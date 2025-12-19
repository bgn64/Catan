using Catan.Core;

namespace Catan.CLI;

public class CommandNameProvider : ICommandVisitor
{
    string commandName;

    public CommandNameProvider()
    {
        commandName = String.Empty;
    }

    public string GetCommandName(Command command)
    {
        command.Accept(this);

        return commandName;
    }

    public void Visit(RollCommand command)
    {
        commandName = nameof(RollCommand);
    }

    public void Visit(PlaceSettlementCommand command)
    {
        commandName = nameof(PlaceSettlementCommand);
    }

    public void Visit(PlaceRoadCommand command)
    {
        commandName = nameof(PlaceRoadCommand);
    }
}
