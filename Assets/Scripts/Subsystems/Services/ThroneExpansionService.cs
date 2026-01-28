public class ThroneExpansionService
{
    private readonly CoalStorage _coalStorage;
    private readonly UnitsCoordinator _unitsCoordinator;
    private readonly CoalSpawner _coalSpawner;

    public ThroneExpansionService(
        CoalStorage coalStorage,
        UnitsCoordinator unitsCoordinator,
        CoalSpawner coalSpawner)
    {
        _coalStorage = coalStorage;
        _unitsCoordinator = unitsCoordinator;
        _coalSpawner = coalSpawner;
    }

    public bool TrySendUnitToBuildNewThrone(Flag flag)
    {
        var unit = _unitsCoordinator.SendUnitTo(flag);

        if (unit == null)
            return false;

        var throneCost = unit.ThroneCost;
        var takenCoalCount = _coalStorage.GiveCoal(throneCost);

        if (takenCoalCount != throneCost)
        {
            unit.GoHome();
            return false;
        }

        for ( var i = 0; i < takenCoalCount; i++)
        {
            var coal = _coalSpawner.Spawn();
            unit.Take(coal);
        }

        _unitsCoordinator.UnregisterUnit(unit);
        unit.SetCoalOccupationRegistry(_unitsCoordinator.GetCoalOccupationRegistry());

        return true;
    }
}
