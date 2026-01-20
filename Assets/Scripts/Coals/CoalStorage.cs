using System;
using UnityEngine;

public class CoalStorage : MonoBehaviour
{
    public int StoredCoalCount { get; private set; } = 0;

    public event Action<int> StoredCoalCountChanged;

    public void StoreCoal(Coal coal)
    {
        StoredCoalCount++;
        coal.Remove();

        StoredCoalCountChanged?.Invoke(StoredCoalCount);
    }
}
