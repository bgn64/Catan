namespace Catan.Core;

public class PlaceSettlementCommand : Command
{
	Vertex? _vertex;

	internal static bool CouldExecute(Game game)
	{
		if (game.CurrentPhase == null)
		{
			return false;
		}

		bool ret = false;
	
		game.CurrentPhase.Accept(new GameSubphaseVisitor(
			initialRollPhase => ret = false,
			initialPlacementPhase => ret = !initialPlacementPhase.HasPlacedSettlement,
			mainGamePhase => ret = false));

		return ret;
	}

	public override bool CanExecute(Game game)
	{
		if (!CouldExecute(game) || vertex == null)
		{
			return false;
		}

		foreach (Vertex vertex in game.Board.GetAdjacentVertices(vertex))
		{
			if (game.Board.HasSettlement(vertex))
			{
				return false;
			}
		}

		return true;
	}

	protected override void ExecuteCore(Game game)
	{
		game.Board.PlaceSettlement(_vertex, game.CurrentPlayer);
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
