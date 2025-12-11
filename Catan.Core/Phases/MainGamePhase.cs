namespace Catan.Core;

public class MainGamePhase : GameSubphase
{
	internal override IEnumerable<Command> GetValidCommands()
	{
		return new List<Command>();
	}

	public override void Accept(IGameSubphaseVisitor visitor)
	{
		visitor.Visit(this);
	}
}
