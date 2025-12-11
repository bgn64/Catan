namespace Catan.Core;

public class PlaceSettlementCommand : Command
{
	public Vertex? Vertex { get; set; }
	
	bool CanExecuteCore(Game game)
	{
		if (Vertex == null || game.UnplayedSettlements[game.CurrentPlayer] == 0)
		{
			return false;
		}

		foreach (Vertex vertex in game.Board.GetAdjacentVertices(Vertex))
		{
			if (game.Board.HasSettlement(vertex))
			{
				return false;
			}
		}	

		return true;
	}

	public override bool CanExecute(Game game)
	{	
		return CanExecuteCore(game) && 
			game.CanGetUnplayedSettlement(game.CurrentPlayer) &&
			game.Board.CanPlaceSettlement(Vertex!);
	}

	protected override bool TryExecuteCore(Game game)
	{
		return CanExecuteCore(game) &&
			game.TryGetUnplayedSettlement(game.CurrentPlayer, out Settlement? settlement) &&
			game.Board.TryPlaceSettlement(Vertex!, settlement!);
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
