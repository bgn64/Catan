namespace Catan.Core;

public class RollCommand : Command, IInitialRollCommand
{
	internal static bool CouldExecute(Game game)
	{
		if (game.CurrentPhase == null)
		{
			return false;
		}

		bool ret = false;

		game.CurrentPhase.Accept(new GameSubphaseVisitor(
			initialRollPhase => ret = true,
            initialPlacementPhase => ret = false, 
            mainGamePhase => ret = false 
        ));

		return ret;
	}

	public override bool CanExecute(Game game)
	{
		return CouldExecute(game);
	}

	protected override void ExecuteCore(Game game)
	{
		game.RollDice();
	}

	public void Accept(IInitialRollCommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
