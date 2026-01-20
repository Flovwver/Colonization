using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsCoordinator : MonoBehaviour
{
    private readonly HashSet<Coal> _occupiedTargets = new();

    [SerializeField] private List<Unit> _units;

    public void SendUnitsTo(List<Coal> targets)
    {
        foreach (Unit freeUnit in _units.Where(unit => unit.Status == UnitStatuses.Idle))
        {
            var freeTarget = targets.FirstOrDefault(target => _occupiedTargets.Contains(target) == false);

            if (freeTarget == null) 
                break;

            freeUnit.SetTarget(freeTarget);
            _occupiedTargets.Add(freeTarget);
            freeTarget.OnRemoved += OnTargetRemoved;
        }
    }

    private void OnTargetRemoved(Coal target)
    {
        target.OnRemoved -= OnTargetRemoved;
        _occupiedTargets.Remove(target);
    }
}
