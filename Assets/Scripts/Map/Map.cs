using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject terrain;

    private readonly List<Transform> _walkable = new List<Transform>();
    private readonly List<Transform> _placeable = new List<Transform>();

    public IEnumerable<Transform> Tiles => _walkable.Concat(_placeable).ToList();
    
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
}
