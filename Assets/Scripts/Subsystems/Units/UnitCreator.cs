using UnityEngine;

[RequireComponent(typeof(CoalStorage))]
[RequireComponent(typeof(Throne))]
[RequireComponent(typeof(UnitsCoordinator))]
public class UnitCreator : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private int _unitCost;
    [SerializeField] private Transform _spawnPoint;

    public int UnitCost => _unitCost;

    public Unit CreateUnit(int coalForUnit)
    {
        if (coalForUnit < _unitCost)
            return null;

        Unit unit = Instantiate(_unitPrefab, _spawnPoint.position, Quaternion.identity);

        return unit;
    }
}
