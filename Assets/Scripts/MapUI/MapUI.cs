﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    
    [SerializeField] private RectTransform arrow;
    [SerializeField] private RectTransform corners;
    [SerializeField] private Camera cam;
    
    private void Start()
    {
        Game.MapUI = this;
        Clear();
    }

    public IEnumerator TowerPlacement()
    {
        do
        {
            yield return null;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hitInfo)) continue;
            if (!Game.Map.IsPlaceable(hitInfo.transform)) continue;
            var tilePos = hitInfo.transform.position;
            var dir = hitInfo.point - tilePos;
            SetUI(tilePos, dir);
        } while (Game.Map.IsPlacingTower); // TODO: Placeholder condition
        Clear();
        yield return null;
    }

    private void SetUI(Vector3 tilePos, Vector3 dir)
    {
        Vector3 other;
        float rot;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            other = (dir.x > 0 ? Vector3.right : Vector3.left) * Game.TileSize;
            rot = dir.x > 0 ? 90 : -90;
        }
        else
        {
            other = (dir.z > 0 ? Vector3.forward : Vector3.back) * Game.TileSize;
            rot = dir.z > 0 ? 180 : 0;
        }
        if (!Physics.Raycast(
            cam.transform.position,
            -cam.transform.position + tilePos + other,
            out var hit)) return;
        if (!Game.Map.IsPath(hit.transform)) return;
        Activate();
        corners.position = cam.WorldToScreenPoint(tilePos);
        arrow.position = cam.WorldToScreenPoint(tilePos + other / 2);
        arrow.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
    }

    private void Activate()
    {
        arrow.gameObject.SetActive(true);
        corners.gameObject.SetActive(true);
    }
    
    private void Clear()
    {
        arrow.gameObject.SetActive(false);
        corners.gameObject.SetActive(false);
    }
    
}
