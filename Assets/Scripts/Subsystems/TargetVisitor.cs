using UnityEngine;

[RequireComponent(typeof(CoalInteractor))]
[RequireComponent(typeof(ThroneBuilder))]
[RequireComponent(typeof(Unit))]
public class TargetVisitor : MonoBehaviour
{
    private CoalInteractor _interactor;
    private ThroneBuilder _throneBuilder;

    private Unit _unit;

    private void Awake()
    {
        _interactor = GetComponent<CoalInteractor>();
        _throneBuilder = GetComponent<ThroneBuilder>();
        _unit = GetComponent<Unit>();
    }

    public void Visit(Coal coal) 
    {
        _interactor.Take(coal);
    }

    public void Visit(Throne throne)
    {
        _interactor.GiveAllCoalsTo(throne);
    }

    public void Visit(Flag flag)
    {
        bool result = _throneBuilder.TryBuildThrone(flag, _interactor.CoalCount);

        if (result)
        {
            _interactor.RemoveAllCoals();
        }
        else
        {
            Destroy(flag.gameObject);
            _unit.GoHome();
        }
    }
}
