using Catan.Core;

namespace Catan.CLI;

public class CommandNameProvider : IInitialRollCommandVisitor
{
	string commandName;

	public CommandNameProvider()
	{
		commandName = String.Empty;
	}

	public string GetCommandName(IInitialRollCommand command)
	{
		command.Accept(this);

		return commandName;
	}

	public void Visit(RollCommand command)
	{
		commandName = nameof(RollCommand);
	}
}
