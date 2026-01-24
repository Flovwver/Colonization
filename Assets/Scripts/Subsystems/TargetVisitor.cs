using UnityEngine;

[RequireComponent(typeof(CoalInteractor))]
public class TargetVisitor : MonoBehaviour
{
    private CoalInteractor _interactor;

    private void Awake()
    {
        _interactor = GetComponent<CoalInteractor>();
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
        
    }
}
