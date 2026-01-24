using System.Collections.Generic;
using UnityEngine;

public class CoalSearcher : MonoBehaviour
{
    private readonly Collider[] _hits = new Collider[32];

    [SerializeField] private float _searchRadius = 10f;
    [SerializeField] private LayerMask _coalLayerMask;
    [SerializeField] private CoalOccupationRegistry _coalOccupationRegistry;

    public List<Coal> FindAllCoals()
    {
        int hitsCount = Physics.OverlapSphereNonAlloc(transform.position, _searchRadius, _hits, _coalLayerMask);

        List<Coal> coals = new();

        for (int i = 0; i < hitsCount; i++)
        {
            if (_hits[i].TryGetComponent(out Coal coal) &&
                _coalOccupationRegistry.IsOccupied(coal) == false)
            {
                coals.Add(coal);
            }
        }

        coals.Sort((a, b) =>
            Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position))
        );

        return coals;
    }
}
