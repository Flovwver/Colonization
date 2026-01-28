public class UnitProductionService
{
    private readonly CoalStorage _coalStorage;
    private readonly UnitCreator _unitCreator;
    private readonly UnitsCoordinator _unitsCoordinator;
    private readonly Throne _throne;

    public UnitProductionService(
        CoalStorage coalStorage, 
        UnitCreator unitCreator, 
        UnitsCoordinator unitsCoordinator, 
        Throne throne)
    {
        _coalStorage = coalStorage;
        _unitCreator = unitCreator;
        _unitsCoordinator = unitsCoordinator;
        _throne = throne;
    }

    public int UnitsCount => _unitsCoordinator.UnitsCount;

    public void CreateUnit(int coalCountForUnit)
    {
        if (coalCountForUnit > _unitCreator.UnitCost)
        {
            var takedCoalCount = _coalStorage.GiveCoal(_unitCreator.UnitCost);

            Unit unit = _unitCreator.CreateUnit(takedCoalCount);

            if (unit == null)
                return;

            unit.SetThrone(_throne);
            _unitsCoordinator.RegisterUnit(unit);
        }
    }
}
