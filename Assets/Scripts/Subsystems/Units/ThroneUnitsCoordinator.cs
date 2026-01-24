using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThroneUnitsCoordinator : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private CoalOccupationRegistry _coalOccupationRegistry;

    public void RegisterUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void SendUnitsTo(List<Coal> targets)
    {
        foreach (Unit unit in _units.Where(unit => unit.Status == UnitStatuses.Idle))
        {
            Coal freeTarget = FindFreeTarget(targets);

            if (freeTarget == null)
                break;

            if (_coalOccupationRegistry.TryOccupy(freeTarget) == false)
                continue;

            unit.SetTarget(freeTarget);
        }
    }

    public bool TrySendUnitTo(Flag target)
    {
        var unit = _units.FirstOrDefault(u => u.Status == UnitStatuses.Idle);

        if (unit == null)
            return false;

        unit.SetTarget(target);
        return true;
    }

    private Coal FindFreeTarget(List<Coal> targets)
    {
        foreach (Coal target in targets)
        {
            if (_coalOccupationRegistry.IsOccupied(target) == false)
                return target;
        }

        return null;
    }
}
