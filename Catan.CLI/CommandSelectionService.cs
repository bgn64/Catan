using Catan.Core;
using System;

namespace Catan.CLI;

public class CommandSelectionService : IGameSubphaseVisitor
{
	CommandNameProvider _commandNameProvider;
	ICommand? _selectedCommand;

    public CommandSelectionService()
    {
		_commandNameProvider = new CommandNameProvider();
    }

	public ICommand? SelectCommand(Game game)
	{
		if (game.CurrentPhase == null)
		{
			return null;
		}

		game.CurrentPhase.Accept(this);

		return _selectedCommand;
	}

	public void Visit(InitialRollPhase phase)
	{
		List<IInitialRollCommand> commands = phase.GetValidCommands().ToList();

		if (commands.Count == 0)
		{
			_selectedCommand = null;
			
			return;
		}

		Console.WriteLine("Valid commands:");

		for (int i = 0; i < commands.Count; i++)
		{
			Console.WriteLine($"{i}: {_commandNameProvider.GetCommandName(commands[i])}");
		}

		Console.WriteLine($"Please select a command index: ");
		
		string? input = Console.ReadLine();
		int index = 0;

		while (!int.TryParse(input, out index) || index < 0 || index >= commands.Count)
		{
			Console.WriteLine("Please select a valid command index: ");
			input = Console.ReadLine();
		}

		_selectedCommand = commands[index];
	}

    public void Visit(InitialPlacementPhase phase)
    {
		_selectedCommand = null;
    }

    public void Visit(MainGamePhase phase)
    {
		_selectedCommand = null;
    }
}
