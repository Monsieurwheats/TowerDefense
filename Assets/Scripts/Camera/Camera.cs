using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Camera : MonoBehaviour
{

    private Vector3 _touchStart;
    private Vector3 _camOffset;
    private float _minX, _minZ, _maxX, _maxZ;

    private void Start()
    {
        Game.Cam = GetComponent<UnityEngine.Camera>();
        StartCoroutine(GetBounds());
        _camOffset = new Vector3(0, 0, transform.position.y);
    }

    private void LateUpdate()
    {
        if (Game.ShopUI.IsPlacingTower) return;
        if (Input.GetMouseButtonDown(0))
        {
            _touchStart = Game.Cam.ScreenToWorldPoint(Input.mousePosition + _camOffset);
        }
        if (!Input.GetMouseButton(0)) return;
        if (IsPointerOverUIObject()) return;

        var touch = Game.Cam.ScreenToWorldPoint(Input.mousePosition + _camOffset);
        var direction = _touchStart - touch;
        
        var x = direction.x;
        var z = direction.z;
        var camPos = transform.position;
        
        if (camPos.x <= _minX) x = Mathf.Max(x, 0); // Bottom bound
        if (camPos.x >= _maxX) x = Mathf.Min(x, 0); // Top bound
        if (camPos.z <= _minZ) z = Mathf.Max(z, 0); // Left bound
        if (camPos.z >= _maxZ) z = Mathf.Min(z, 0); // Right bound

        transform.position += new Vector3(x, 0, z);
    }

    public static bool IsPointerOverUIObject()
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
