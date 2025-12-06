namespace Catan.Core;

public class InitialPlacementPhase : GameSubphase
{
	public InitialPlacementPhase()
	{
		CurrentPhase = new PlaceRoadPhase();
	}

    public InitialPlacementSubphase CurrentPhase { get; private set; }

	public override void Accept(IGameSubphaseVisitor visitor)
	{
		visitor.Visit(this);
	}
}
