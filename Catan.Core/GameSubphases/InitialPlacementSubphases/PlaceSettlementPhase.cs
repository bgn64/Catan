namespace Catan.Core;

public class PlaceSettlementPhase : InitialPlacementSubphase
{
	public override void Accept(IInitialPlacementSubphaseVisitor visitor)
	{
		visitor.Visit(this);
	}
}
