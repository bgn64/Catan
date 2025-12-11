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
			game.CanGetUnplayedRoad(game.CurrentPlayer) &&
			game.Board.CanPlaceRoad(Edge!);
	}

	protected override bool TryExecuteCore(Game game)
	{
		return CanExecuteCore(game) &&
			game.TryGetUnplayedRoad(game.CurrentPlayer, out Road? road) &&
			game.Board.TryPlaceRoad(Edge!, road!);
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
