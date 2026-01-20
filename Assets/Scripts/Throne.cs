using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CoalSearcher))]
[RequireComponent(typeof(CoalStorage))]
[RequireComponent(typeof(UnitsCoordinator))]
public class Throne : VisitableTarget
{
    [SerializeField] private bool _isSearch = true;
    [SerializeField] private float _searchDelay = 3.0f;

    private CoalSearcher _coalSearcher;
    private CoalStorage _storage;
    private UnitsCoordinator _unitsCoordinator;

    private void Awake()
    {
        _coalSearcher = GetComponent<CoalSearcher>();
        _storage = GetComponent<CoalStorage>();
        _unitsCoordinator = GetComponent<UnitsCoordinator>();
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
