namespace Catan.Core;

public class PlaceRoadCommand : Command
{
	public Edge? Edge { get; set; }	

	bool CanExecuteCore(Game game)
	{
		if (Edge == null)
		{
			return false;
		}

		foreach (Vertex vertex in Edge.Vertices)
		{
			if (game.Board.HasSettlement(vertex, game.CurrentPlayer))
			{
				return true;
			}
		}

		return false;
	}

	public override bool CanExecute(Game game)
	{
		return CanExecuteCore(game) &&
			game.Board.CanPlaceRoad(Edge!, game.CurrentPlayer);
	}

	protected override bool ExecuteCore(Game game)
	{
		return CanExecuteCore(game) &&
			game.Board.PlaceRoad(Edge!, game.CurrentPlayer);
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
