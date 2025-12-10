namespace Catan.Core;

public class InitialPlacementSubphaseVisitor : IInitialPlacementSubphaseVisitor
{
    Action<PlaceSettlementPhase> _placeSettlementPhaseAction;
    Action<PlaceRoadPhase> _placeRoadPhaseAction;

    public InitialPlacementSubphaseVisitor(Action<PlaceSettlementPhase> placeSettlementPhaseAction, Action<PlaceRoadPhase> placeRoadPhaseAction)
    {
        _placeSettlementPhaseAction = placeSettlementPhaseAction;
        _placeRoadPhaseAction = placeRoadPhaseAction;
    }

    public void Visit(PlaceSettlementPhase subphase)
    {
        _placeSettlementPhaseAction(subphase);
    }

    public void Visit(PlaceRoadPhase subphase)
    {
        _placeRoadPhaseAction(subphase);
    }
}