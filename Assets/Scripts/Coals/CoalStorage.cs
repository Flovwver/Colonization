using UnityEngine;

public class CoalStorage : MonoBehaviour
{
    [SerializeField] private CoalStorageUI _coalStorageUI;

    public int StoredCoalCount { get; private set; } = 0;

    public void StoreCoal(Coal coal)
    {
        StoredCoalCount++;
        coal.Remove();

        _coalStorageUI?.SetCount(StoredCoalCount);
    }
}
