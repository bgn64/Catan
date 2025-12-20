namespace Catan.Core;

public class EndTurnCommand : Command
{
    public override bool CanExecute(Game game)
    {
        return true;
    }

    protected override bool TryExecuteCore(Game game)
    {
        return true;
    }

    public override void Accept(ICommandVisitor visitor)
    {
        visitor.Visit(this);
    }
}
