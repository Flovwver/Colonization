using UnityEngine;

public class CoalInteractor : MonoBehaviour
{
    [SerializeField] private Vector3 _holdPoint;
    [SerializeField] private LayerMask _layerMask;

    private Coal _takedCoal = null;

    public bool HasCoal => _takedCoal != null;

    public void Take(Coal coal)
    {
        if (coal == null || coal.IsTook) 
            return;

        coal.OnTook();
        coal.transform.SetParent(transform, true);
        coal.transform.localPosition = _holdPoint;
        _takedCoal = coal;
    }

    public void GiveCoalTo(Throne throne)
    {
        if (_takedCoal == null)
            return;
                
        _takedCoal.transform.SetParent(null, true);
        throne.TakeCoal(_takedCoal);
        _takedCoal = null;        
    }
}
