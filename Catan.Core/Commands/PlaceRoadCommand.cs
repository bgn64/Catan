namespace Catan.Core;

public class PlaceRoadCommand : Command
{
	Edge _edge;

	public void SetEdge(Edge edge)
	{
		_edge = edge;
	}

	internal static bool CouldExecute(Game game)
	{
		if (game.CurrentPhase == null)
		{
			return false;
		}

		bool ret = false;

		game.CurrentPhase.Accept(new GameSubphaseVisitor(
			initialRollPhase => ret = false,
			initialPlacementPhase => ret = initialPlacementPhase.HasPlacedSettlement,
			mainGamePhase => ret = false));

		return ret;
	}

	public override bool CanExecute(Game game)
	{
		return CouldExecute(game);
	}

	protected override void ExecuteCore(Game game)
	{
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
