using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(TargetVisitor))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget = 0.1f;
    [SerializeField] private Throne _throne;

    private VisitableTarget _target;

    private Mover _mover;
    private TargetVisitor _targetVisitor;

    public UnitStatuses Status => _target != null ? UnitStatuses.Busy : UnitStatuses.Idle;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _targetVisitor = GetComponent<TargetVisitor>();
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        _mover.MoveTo(_target.transform.position);

        if (IsCloseToTarget() == false)
            return;

        _target.Accept(_targetVisitor);

        ResolveNextTarget();

    }

    public void SetTarget(VisitableTarget target)
    {
        _target = target;
    }

    private bool IsCloseToTarget()
    {
        if (_target == null)
            return false;

        return Vector3.Distance(transform.position, _target.transform.position) < _distanceToTarget;
    }

    private void ResolveNextTarget()
    {
        if (_target is Coal)
        {
            SetTarget(_throne);
        }
        else
        {
            SetTarget(null);
        }
    }
}
