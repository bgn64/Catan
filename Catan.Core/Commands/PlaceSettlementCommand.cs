namespace Catan.Core;

public class PlaceSettlementCommand : Command
{
	public FlatTopCoordinate? Coordinate { get; set; }
	
	bool CanExecuteCore(Game game)
	{
		if (Coordinate == null)
		{
			return false;
		}

		foreach (FlatTopCoordinate coordinate in Coordinate.GetAdjacentCoordinates())
		{
			if (game.Board.TryGetSettlement(coordinate, out _))
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
			game.Board.CanPlaceSettlement(Coordinate!);
	}

	protected override bool TryExecuteCore(Game game)
	{
		return CanExecuteCore(game) &&
			game.TryGetUnplayedSettlement(game.CurrentPlayer, out Settlement? settlement) &&
			game.Board.TryPlaceSettlement(Coordinate!, settlement!);
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
