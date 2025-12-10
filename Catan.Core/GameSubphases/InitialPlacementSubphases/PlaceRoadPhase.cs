namespace Catan.Core;

public class PlaceRoadPhase : InitialPlacementSubphase
{
	public override void Accept(IInitialPlacementSubphaseVisitor visitor)
	{
		visitor.Visit(this);
	}
}
