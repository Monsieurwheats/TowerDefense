﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Camera : MonoBehaviour
{

    private float _minX, _minZ, _maxX, _maxZ;

    private void Start()
    {
        Game.Cam = GetComponent<UnityEngine.Camera>();
        StartCoroutine(GetBounds());
    }

    private void LateUpdate()
    {
        // TODO: Will have to fix for mobile
        if (Game.ShopUI.IsPlacingTower) return;
        if (!Input.GetMouseButton(0)) return;
        if (IsPointerOverUIObject()) return;

        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        var camPos = transform.position;

        if (camPos.x <= _minX) y = Mathf.Min(y, 0); // Bottom bound
        if (camPos.x >= _maxX) y = Mathf.Max(y, 0); // Top bound
        if (camPos.z <= _minZ) x = Mathf.Max(x, 0); // Left bound
        if (camPos.z >= _maxZ) x = Mathf.Min(x, 0); // Right bound

        transform.Translate(-x, -y, 0);
    }

    private bool IsPointerOverUIObject()
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private IEnumerator GetBounds()
    {
        yield return null;
        float minX = float.MaxValue, minZ = float.MaxValue, maxX = float.MinValue, maxZ = float.MinValue;
        foreach (var pos in Game.Map.Tiles.Select(tile => tile.position))
        {
            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.z < minZ) minZ = pos.z;
            if (pos.z > maxZ) maxZ = pos.z;
        }

        _minX = minX;
        _minZ = minZ;
        _maxX = maxX;
        _maxZ = maxZ;
    }

}
