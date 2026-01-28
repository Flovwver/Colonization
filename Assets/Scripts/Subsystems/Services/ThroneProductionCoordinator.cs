using System.Collections;

public class ThroneProductionCoordinator
{
    private readonly UnitProductionService _unitProductionService;
    private readonly ThroneExpansionService _throneExpansionService;

    private ProductionMode _productionMode;
    private Flag _flag;

    public ThroneProductionCoordinator(
        UnitProductionService unitProductionService,
        ThroneExpansionService throneExpansionService)
    {
        _unitProductionService = unitProductionService;
        _throneExpansionService = throneExpansionService;
        _productionMode = ProductionMode.Units;
    }

    public void RequestThroneExpansion(Flag flag)
    {
        if (_unitProductionService.UnitsCount <= 1)
            return;

        _productionMode = ProductionMode.Expansion;
        _flag = flag;
    }

    public void OnStoredCoalCountChanged(int newCoalCount)
    {
        switch (_productionMode)
        {
            case (ProductionMode.Units):
                _unitProductionService.CreateUnit(newCoalCount);
                break;

            case (ProductionMode.Expansion):
                var result = _throneExpansionService.TrySendUnitToBuildNewThrone(_flag);

                if (result)
                    _productionMode = ProductionMode.Units;

                break;

        }
    }
}

