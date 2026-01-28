using UnityEngine;

[RequireComponent(typeof(Unit))]
public class ThroneBuilder : MonoBehaviour
{
    [SerializeField] private Throne _prefab;
    [SerializeField] private int _cost;

    private CoalOccupationRegistry _occupationRegistry;
    private Unit _owner;

    public int Cost => _cost;

    private void Awake()
    {
        _owner = GetComponent<Unit>();
    }

    public void SetCoalOccupationRegistry(CoalOccupationRegistry occupationRegistry)
    {
        _occupationRegistry = occupationRegistry;
    }

    public bool TryBuildThrone(Flag flag, int coalCountOnBuilding)
    {
        if (coalCountOnBuilding < _cost)
            return false;

        Vector3 newThronePosition = flag.transform.position;
        newThronePosition.y = _prefab.transform.position.y;

        Throne throne = Instantiate(_prefab, newThronePosition, Quaternion.identity);

        _owner.SetThrone(throne);
        throne.Register(_owner);
        throne.SetCoalOccupationRegistry(_occupationRegistry);

        Destroy(flag.gameObject);
        return true;
    }
}
