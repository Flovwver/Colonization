using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(TargetVisitor))]
[RequireComponent(typeof(ThroneBuilder))]
[RequireComponent(typeof(CoalInteractor))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget = 0.1f;
    [SerializeField] private Throne _throne;

    [SerializeField] private VisitableTarget _target;

    private Mover _mover;
    private TargetVisitor _targetVisitor;
    private ThroneBuilder _throneBuilder;
    private CoalInteractor _interactor;

    public UnitStatuses Status => _target != null ? UnitStatuses.Busy : UnitStatuses.Idle;
    public int ThroneCost => _throneBuilder.Cost;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _targetVisitor = GetComponent<TargetVisitor>();
        _throneBuilder = GetComponent<ThroneBuilder>();
        _interactor = GetComponent<CoalInteractor>();
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

    public void SetThrone(Throne throne)
    {
        _throne = throne;
    }

    public void GoHome()
    {
        SetTarget(_throne);
    }

    public void Take(Coal coal)
    {
        _interactor.Take(coal);
    }

    public void SetCoalOccupationRegistry(CoalOccupationRegistry occupationRegistry)
    {
        _throneBuilder.SetCoalOccupationRegistry(occupationRegistry);
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
