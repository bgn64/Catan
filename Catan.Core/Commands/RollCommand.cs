namespace Catan.Core;

public class RollCommand : Command
{
	public override bool CanExecute(Game game)
	{
		return true;
	}

	protected override bool TryExecuteCore(Game game)
	{
		game.RollDice();

		return true;
	}

	public override void Accept(ICommandVisitor visitor)
	{
		visitor.Visit(this);
	}
}
