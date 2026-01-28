using System;
using UnityEngine;

[RequireComponent(typeof(CoalSearcher))]
[RequireComponent(typeof(CoalStorage))]
[RequireComponent(typeof(UnitsCoordinator))]
[RequireComponent(typeof(UnitCreator))]
[RequireComponent(typeof(CoalSpawner))]
public class Throne : VisitableTarget
{
    [SerializeField] private float _searchDelay = 3.0f;
    
    private bool _isSelected = false;

    private CoalSearcher _coalSearcher;
    private CoalStorage _storage;
    private UnitsCoordinator _unitsCoordinator;
    private UnitCreator _unitCreator;
    private CoalSpawner _coalSpawner;

    private UnitProductionService _productionService;
    private ThroneExpansionService _expansionService;
    private ResourceSearchService _searchService;
    private ThroneProductionCoordinator _throneProductionCoordinator;

    public event Action<bool> OnSelected;

    private void Awake()
    {
        _coalSearcher = GetComponent<CoalSearcher>();
        _storage = GetComponent<CoalStorage>();
        _unitsCoordinator = GetComponent<UnitsCoordinator>();
        _unitCreator = GetComponent<UnitCreator>();
        _coalSpawner = GetComponent<CoalSpawner>();

        _productionService = new (
            _storage,
            _unitCreator,
            _unitsCoordinator,
            this
        );

        _expansionService = new (
            _storage,
            _unitsCoordinator,
            _coalSpawner
        );

        _searchService = new (
            _coalSearcher,
            _unitsCoordinator,
            _searchDelay
        );

        _throneProductionCoordinator = new (
            _productionService, 
            _expansionService
        );
    }

    private void Start()
    {
        StartCoroutine(_searchService.RunSearch());
    }

    private void OnEnable()
    {
        _storage.StoredCoalCountChanged += _throneProductionCoordinator.OnStoredCoalCountChanged;
    }

    private void OnDisable()
    {
        _storage.StoredCoalCountChanged -= _throneProductionCoordinator.OnStoredCoalCountChanged;
    }

    public override void Accept(TargetVisitor visitor) => visitor.Visit(this);

    public void TakeCoal(Coal coal)
    {
        _storage.StoreCoal(coal);
    }

    public void SendUnitToBuild(Flag flag)
    {
        _throneProductionCoordinator.RequestThroneExpansion(flag);
    }

    public void Register(Unit unit)
    {
        _unitsCoordinator.RegisterUnit(unit);
    }

    public void SetSelected(bool isSelected)
    { 
        _isSelected = isSelected;
        OnSelected?.Invoke(_isSelected);
    }

    public void SetCoalOccupationRegistry(CoalOccupationRegistry coalOccupationRegistry)
    {
        _unitsCoordinator.SetCoalOccupationRegistry(coalOccupationRegistry);
        _coalSearcher.SetCoalOccupationRegistry(coalOccupationRegistry);
    }
}
