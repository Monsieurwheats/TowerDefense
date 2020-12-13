using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public bool CanBePlaced
    {
        get => _canBePlaced;
        set
        {
            if (_canBePlaced == value) return;
            rangeIndicator.GetComponent<MeshRenderer>().material.color = value ? GoodPlace : BadPlace;
            _canBePlaced = value;
        }
    }
    
    public int Price => level1.price;

    private bool _canBePlaced;
    private static readonly Color GoodPlace = new Color(0.2f, 0.2f, 0.2f, 0.5f);
    private static readonly Color BadPlace = new Color(1, 0, 0, 0.5f);

    [SerializeField] private Collider box = null;
    [SerializeField] private GameObject rangeIndicator = null;

    [SerializeField] private Level level1 = null;
    [SerializeField] private Level level2 = null;
    [SerializeField] private Level level3 = null;
    [SerializeField] private Level level4A = null;
    [SerializeField] private Level level4B = null;

    protected Level CurrLevel;

    private void Start()
    {
        CurrLevel = level1;
        level1.SetNext(level2);
        level2.SetNext(level3);
        level3.SetNext(level4A).SetNext(level4B);
    }


    public void TryPlace(RaycastHit hit)
    {
        transform.position = hit.point;
        rangeIndicator.transform.localScale = new Vector3(level1.range, 0.1f, level1.range);
        var bounds = box.bounds;
        var hitColliders = Physics.OverlapBox(bounds.center, bounds.extents / 2);
        if (hitColliders.Length != 0)
        {
            if (hitColliders.Select(col => col.gameObject.GetComponent<Tower>()).Any(other => other != this && other != null))
            {
                CanBePlaced = false;
                return;
            }
        }

        if (!Game.Map.IsPlaceable(hit.collider.transform))
        {
            CanBePlaced = false;
            return;
        }
        CanBePlaced = true;
    }

    public bool Place()
    {
        if (!CanBePlaced)
        {
            Destroy(gameObject);
            return false;
        }
        rangeIndicator.SetActive(false);
        return true;
    }

    protected abstract IEnumerator Shoot();

    private void OnMouseDown()
    {
        //TODO: Open upgrade ui
        print("Open ui for " + this);
    }


    [Serializable]
    protected class Level
    {
        public GameObject turret;
        public GameObject construction;
        public GameObject bullet;
        public Transform[] shootPoint;
        public float range;
        public float secPerShot;
        public int price;
        
        [NonSerialized] private List<Level> _next;

        public Level()
        {
            _next = new List<Level>();
        }

        public Level SetNext(Level l)
        {
            _next.Add(l);
            return this;
        }

        public List<Level> GetNext()
        {
            return _next;
        }
    }

}
