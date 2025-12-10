namespace Catan.Core;

public class InitialPlacementPhase : GameSubphase
{
	public override void Accept(IGameSubphaseVisitor visitor)
    {
        visitor.Visit(this);
    }
}
