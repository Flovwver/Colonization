using System.Collections.Generic;
using UnityEngine;

public class CoalOccupationRegistry : MonoBehaviour
{
    private readonly HashSet<Coal> _occupiedCoals = new();

    public bool TryOccupy(Coal coal)
    {
        if (_occupiedCoals.Contains(coal))
            return false;

        _occupiedCoals.Add(coal);
        coal.OnRemoved += OnCoalRemoved;
        return true;
    }

    public bool IsOccupied(Coal coal)
    {
        return _occupiedCoals.Contains(coal);
    }

    private void OnCoalRemoved(Coal coal)
    {
        coal.OnRemoved -= OnCoalRemoved;
        _occupiedCoals.Remove(coal);
    }
}
