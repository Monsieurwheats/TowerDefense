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
    public AudioClip shootingSound;

    private bool _canBePlaced;
    private Coroutine _shootRoutine;
    
    private static readonly Color GoodPlace = new Color(0.2f, 0.2f, 0.2f, 0.5f);
    private static readonly Color BadPlace = new Color(1, 0, 0, 0.5f);

    [SerializeField] private Collider box = null;
    [SerializeField] private GameObject rangeIndicator = null;

    [SerializeField] private Level level1 = null;
    [SerializeField] private Level level2 = null;
    [SerializeField] private Level level3 = null;
    [SerializeField] private Level level4A = null;
    [SerializeField] private Level level4B = null;

    private Level _level;
    
    private int _shooter = 0;

    protected Transform Shooter
    {
        get
        {
            _shooter += 1;
            _shooter %= CurrLevel.shootPoint.Length;
            return CurrLevel.shootPoint[_shooter];
        }
    }

    public Level CurrLevel
    {
        get => _level;
        set
        {
            _level?.turret.SetActive(false);
            _level = value;
            _level.turret.SetActive(true);
            rangeIndicator.transform.localScale = new Vector3(_level.range, 0.1f, _level.range);
        }
    }

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
        if (!CanBePlaced || Camera.IsPointerOverUIObject())
        {
            Destroy(gameObject);
            return false;
        }
        rangeIndicator.SetActive(false);
        // TODO: Check if round is playing
        StartRound();
        return true;
    }

    public void StartRound()
    {
        _shootRoutine = StartCoroutine(Shoot());
    }

    public void EndRound()
    {
        StopCoroutine(_shootRoutine);
        _shootRoutine = null;
    }
    
    protected abstract IEnumerator Shoot();

    protected Minions[] MinionsInRange()
    {
        var colliders = Physics.OverlapSphere(this.transform.position, CurrLevel.range / 2);
        var minions = colliders
            .Select(c => c.gameObject.GetComponent<Minions>())
            .Where(minion => minion != null)
            .OrderBy(minion => minion.Distance).ToArray();
        return minions;
    }

    private void OnMouseDown()
    {
        if (Camera.IsPointerOverUIObject()) return;
        Game.TowerUI.SelectedTower = this;
    }

    public void RangeShown(bool shown)
    {
        rangeIndicator.SetActive(shown);
    }
    
    private void OnDrawGizmos()
    {
        if (CurrLevel == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CurrLevel.range / 2);
    }


    [Serializable]
    public class Level
    {
        public GameObject turret;
        public GameObject construction;
        public Sprite thumbnail;
        public GameObject bullet;
        public int damage;
        public Transform[] shootPoint;
        public float range;
        public float secPerShot;
        public int price;
        public int sellPrice;

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
