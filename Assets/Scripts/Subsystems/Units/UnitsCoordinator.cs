using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsCoordinator : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private CoalOccupationRegistry _coalOccupationRegistry;
    [SerializeField] private int _minUnitsCount = 1;

    public int UnitsCount => _units.Count;

    public CoalOccupationRegistry GetCoalOccupationRegistry() => _coalOccupationRegistry;

    public void SetCoalOccupationRegistry(CoalOccupationRegistry coalOccupationRegistry)
    {
        _coalOccupationRegistry = coalOccupationRegistry;
    }

    public void RegisterUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void UnregisterUnit(Unit unit)
    {
        _units.Remove(unit);
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

    public Unit SendUnitTo(Flag target)
    {
        if (_units.Count <= _minUnitsCount)
            return null;

        var unit = _units.FirstOrDefault(u => u.Status == UnitStatuses.Idle);

        if (unit != null)
            unit.SetTarget(target);

        return unit;
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
