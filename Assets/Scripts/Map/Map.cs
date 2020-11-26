using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject terrain;

    private readonly List<Transform> _walkable = new List<Transform>();
    private readonly List<Transform> _placeable = new List<Transform>();

    public bool IsPlacingTower;
    
    public bool IsPath(Transform item)
    {
        return _walkable.Contains(item);
    }

    public bool IsPlaceable(Transform item)
    {
        return _placeable.Contains(item);
    }

    private void Start()
    {
        Game.Map = this;

        foreach (Transform o in path.transform)
        {
            _walkable.Add(o);
        }
        foreach (Transform o in terrain.transform)
        {
            _placeable.Add(o);
        }
    }

    private void Update()
    {
        // TODO: Placeholder way of calling routine
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPlacingTower)
            {
                IsPlacingTower = true;
                StartCoroutine(Game.MapUI.TowerPlacement());
                
            }
            else
            {
                IsPlacingTower = false;
            }
        }
    }
}
