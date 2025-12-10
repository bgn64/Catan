namespace Catan.Core;

public interface IInitialPlacementSubphaseVisitor
{
	void Visit(PlaceSettlementPhase phase);
	void Visit(PlaceRoadPhase phase);
}
