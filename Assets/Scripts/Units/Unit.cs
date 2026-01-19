using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(CoalSearcher))]
[RequireComponent(typeof(CoalInteractor))]
public class Unit : MonoBehaviour
{
    [SerializeField] private UnitStatuses _status = UnitStatuses.Idle;
    [SerializeField] private float _distanceToTarget = 0.1f;
    [SerializeField] private Throne _throne;

    private Transform _target;

    private Mover _mover;
    private CoalSearcher _searcher;
    private CoalInteractor _interactor;

    public UnitStatuses Status => _status;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _searcher = GetComponent<CoalSearcher>();
        _interactor = GetComponent<CoalInteractor>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            _status = UnitStatuses.Busy;
            _mover.GoToSpawner(_target.position);

            if(Vector3.Distance(transform.position, _target.position) < _distanceToTarget)
            {
                if (_interactor.HasCoal == false)
                {
                    var coal = _searcher.FindCoal();

                    if (coal != null)
                    {
                        _interactor.Take(coal);

                        _target = _throne.transform;
                    }
                    else
                    {
                        _target = null;
                        _status = UnitStatuses.Idle;
                    }
                }
                else 
                {
                    _interactor.GiveCoalTo(_throne);
                    _status = UnitStatuses.Idle;
                    _target = null;
                }
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
