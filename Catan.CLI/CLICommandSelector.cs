using Catan.Core;
using System;

namespace Catan.CLI;

public class CLICommandSelector : ICommandProvider
{
    Game _game;
    ICommandConfigurationService _commandConfigurationService;
    CommandNameProvider _commandNameProvider;

    public CLICommandSelector(Game game, ICommandConfigurationService commandConfigurationService)
    {
        _game = game;
        _commandConfigurationService = commandConfigurationService;
        _commandNameProvider = new CommandNameProvider();
    }

    public Command? GetCommand()
    {
        List<Command> commands = _game.GetValidCommands().ToList();

        if (commands.Count == 0)
        {
            return null;
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

        _commandConfigurationService.ConfigureCommand(commands[index]);

        return commands[index];
    }	
}
