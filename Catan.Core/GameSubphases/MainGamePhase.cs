namespace Catan.Core;

public class MainGamePhase : GameSubphase
{
	public override void Accept(IGameSubphaseVisitor visitor)
	{
		visitor.Visit(this);
	}
}
