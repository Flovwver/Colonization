using UnityEngine;
using System.Collections;

public class ResourceSearchService 
{
    private readonly CoalSearcher _coalSearcher;
    private readonly UnitsCoordinator _unitsCoordinator;
    private readonly float _searchDelay;

    private bool _isSearch = true;

    public ResourceSearchService(
        CoalSearcher coalSearcher,
        UnitsCoordinator unitsCoordinator,
        float searchDelay)
    {
        _coalSearcher = coalSearcher;
        _unitsCoordinator = unitsCoordinator;
        _searchDelay = searchDelay; 
    }


    public IEnumerator RunSearch()
    {
        var wait = new WaitForSeconds(_searchDelay);

        while (_isSearch)
        {
            yield return wait;

            var findedCoals = _coalSearcher.FindAllCoals();

            _unitsCoordinator.SendUnitsTo(findedCoals);
        }
    }
}
