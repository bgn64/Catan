using Catan.Core;
using System;

namespace Catan.CLI;

public class Program
{
    public static void Main(string[] args)
    {
		Console.WriteLine(Guid.NewGuid());
        Console.WriteLine("Starting Catan game!");
        Game game = new Game();
		GamePrinter gamePrinter = new GamePrinter();
		CommandSelectionService commandSelectionService = new CommandSelectionService();
		
		gamePrinter.Print(game);

		ICommand? command = commandSelectionService.SelectCommand(game);

        while (command != null)
        {
			command.Execute(game);
			gamePrinter.Print(game);

			command = commandSelectionService.SelectCommand(game);
        }

		Console.WriteLine("Failed to get command, exiting...");
    }
}
