using System.Collections.Generic;
using UnityEngine;

public class UnitsCoordinator : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    public void SendUnitsTo(List<Coal> targets)
    {
        int targetIndex = 0;

        foreach (Unit unit in _units)
        {
            if (unit.Status != UnitStatuses.Idle)
                continue;

            if (targetIndex >= targets.Count)
                break;

            unit.SetTarget(targets[targetIndex].transform);
            targetIndex++;
        }
    }
}
