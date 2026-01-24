using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CoalSearcher))]
[RequireComponent(typeof(ThroneCoalStorage))]
[RequireComponent(typeof(ThroneUnitsCoordinator))]
public class Throne : VisitableTarget
{
    [SerializeField] private bool _isSearch = true;
    [SerializeField] private float _searchDelay = 3.0f;

    private CoalSearcher _coalSearcher;
    private ThroneCoalStorage _storage;
    private ThroneUnitsCoordinator _unitsCoordinator;

    private void Awake()
    {
        _coalSearcher = GetComponent<CoalSearcher>();
        _storage = GetComponent<ThroneCoalStorage>();
        _unitsCoordinator = GetComponent<ThroneUnitsCoordinator>();
    }

    private void Start()
    {
        StartCoroutine(RunSearch());
    }

    public override void Accept(TargetVisitor visitor) => visitor.Visit(this);

    public void TakeCoal(Coal coal)
    {
        _storage.StoreCoal(coal);
    }

    public void SendUnitToBuild(Flag flag)
    {
        
    }

    private IEnumerator RunSearch()
    {
        var wait = new WaitForSeconds(_searchDelay);

        while (_isSearch)
        {
            yield return wait;

            var findedCoals = _coalSearcher.FindAllCoals();

            _unitsCoordinator.SendUnitsTo(findedCoals);
        }
    }
}
