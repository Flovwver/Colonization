using UnityEngine;

[RequireComponent(typeof(ThroneCoalStorage))]
[RequireComponent(typeof(Throne))]
[RequireComponent(typeof(ThroneUnitsCoordinator))]
public class ThroneUnitCreator : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private int _unitCost;
    [SerializeField] private Transform _spawnPoint;

    private ThroneCoalStorage _throneCoalStorage;
    private Throne _throne;
    private ThroneUnitsCoordinator _throneUnitsCoordinator;

    private void Awake()
    {
        _throneCoalStorage = GetComponent<ThroneCoalStorage>();
        _throne = GetComponent<Throne>();
        _throneUnitsCoordinator = GetComponent<ThroneUnitsCoordinator>();
    }

    private void OnEnable()
    {
        _throneCoalStorage.StoredCoalCountChanged += OnStoredCoalCountChanged;
    }

    private void OnDisable()
    {
        _throneCoalStorage.StoredCoalCountChanged -= OnStoredCoalCountChanged;
    }

    private void OnStoredCoalCountChanged(int newCoalCount)
    {
        if (newCoalCount > _unitCost)
        {
            CreateUnit();
        }
    }

    private void CreateUnit()
    {
        var takedCoalCount = _throneCoalStorage.GiveCoal(_unitCost);

        if (takedCoalCount < _unitCost)
            return;

        Unit unit = Instantiate(_unitPrefab, _spawnPoint.position, Quaternion.identity);
        unit.SetThrone(_throne);
        _throneUnitsCoordinator.RegisterUnit(unit);
    }
}
