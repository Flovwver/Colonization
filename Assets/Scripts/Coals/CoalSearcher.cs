using System.Collections.Generic;
using UnityEngine;

public class CoalSearcher : MonoBehaviour
{
    private readonly Collider[] _hits = new Collider[32];

    [SerializeField] private float _searchRadius = 10f;
    [SerializeField] private LayerMask _coalLayerMask;

    public List<Coal> FindAllCoals()
    {
        int hitsCount = Physics.OverlapSphereNonAlloc(transform.position, _searchRadius, _hits, _coalLayerMask);

        List<Coal> coals = new();

        for (int i = 0; i < hitsCount; i++)
        {
            if (_hits[i].TryGetComponent(out Coal coal))
            {
                coals.Add(coal);
            }
        }

        return coals;
    }
}
