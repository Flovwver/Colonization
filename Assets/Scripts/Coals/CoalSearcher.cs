using System.Collections.Generic;
using UnityEngine;

public class CoalSearcher : MonoBehaviour
{
    [SerializeField] private float _searchRadius = 10f;

    [SerializeField] private LayerMask _coalLayerMask;

    public List<Coal> FindAllCoals()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius, _coalLayerMask);

        List<Coal> coals = new();

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Coal coal) && coal.IsTook == false)
            {
                coals.Add(coal);
            }
        }

        return coals;
    }

    public Coal FindCoal()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius, _coalLayerMask);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Coal coal) && coal.IsTook == false)
                return coal;
        }

        return null;
    }
}
