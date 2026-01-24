using UnityEngine;
using UnityEngine.Pool;

public class CoalSpawner : MonoBehaviour
{
    [SerializeField] private Coal _prefab;
    [SerializeField] private int _maxCoalCount = 3;

    private ObjectPool<Coal> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coal>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: coal => coal.gameObject.SetActive(true),
            actionOnRelease: coal => coal.gameObject.SetActive(false),
            actionOnDestroy: coal => Destroy(coal.gameObject),
            collectionCheck: false,
            defaultCapacity: _maxCoalCount,
            maxSize: _maxCoalCount
        );
    }

    public void Spawn()
    {
        if (CanSpawn() == false)
            return;

        var coal = _pool.Get();
        coal.transform.position = transform.position;
        coal.TurnOffKinematic();
        coal.OnRemoved += RemoveCoal;
    }

    public bool CanSpawn()
    {
        return _pool.CountActive < _maxCoalCount;
    }

    private void RemoveCoal(Coal coal)
    {
        coal.OnRemoved -= RemoveCoal;

        _pool.Release(coal);
    }
}
