using System.Collections.Generic;
using UnityEngine;

public class CoalInteractor : MonoBehaviour
{
    [SerializeField] private Vector3 _holdPoint;
    [SerializeField] private int _coalsCapacity = 5;
    [SerializeField] private List<Coal> _takedCoals = new();

    public bool HasCoal => _takedCoals.Count > 0;

    public int CoalCount => _takedCoals.Count;

    public void Take(Coal coal)
    {
        if (coal == null || _takedCoals.Count >= _coalsCapacity) 
            return;

        coal.TurnOnKinematic();
        coal.transform.SetParent(transform, true);
        coal.transform.localPosition = _holdPoint;
        _takedCoals.Add(coal);
    }

    public void GiveAllCoalsTo(Throne throne)
    {
        if (HasCoal == false)
            return;
        
        foreach (var coal in _takedCoals)
        {
            coal.transform.SetParent(null, true);
            throne.TakeCoal(coal);
        }

        _takedCoals.Clear();
    }

    public void RemoveAllCoals()
    {
        foreach (var coal in _takedCoals)
            coal.Remove();

        _takedCoals.Clear();
    }
}
