namespace Catan.Core;

public interface IInitialPlacementSubphaseVisitor
{
    void Visit(PlaceSettlementPhase subphase);
    void Visit(PlaceRoadPhase subphase);
}