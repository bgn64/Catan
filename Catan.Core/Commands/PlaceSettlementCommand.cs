namespace Catan.Core;

public class PlaceSettlementCommand : Command
{
	public Vertex? Vertex { get; set; }
	
	bool CanExecuteCore(Game game)
	{
		if (Vertex == null)
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
			game.Board.CanPlaceSettlement(Vertex!, game.CurrentPlayer);
	}

	protected override bool ExecuteCore(Game game)
	{
		return CanExecuteCore(game) &&
			game.Board.PlaceSettlement(Vertex!, game.CurrentPlayer);
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
