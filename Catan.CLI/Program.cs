using Catan.Core;
using System;

namespace Catan.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Catan game!");
        Game game = new Game();
        IHexToStringConverter hexToStringConverter = new ResourceToStringConverter();
        IVertexToStringConverter vertexToStringConverter = new SettlementToStringConverter();
        IEdgeToStringConverter edgeToStringConverter = new RoadToStringConverter();
        BoardToStringConverter boardToStringConverter = new BoardToStringConverter(hexToStringConverter, vertexToStringConverter, edgeToStringConverter);
        GamePrinter gamePrinter = new GamePrinter(boardToStringConverter);
        IVertexProvider vertexProvider = new CLIVertexSelector(boardToStringConverter);
        IEdgeProvider edgeProvider = new CLIEdgeSelector(boardToStringConverter);
        ICommandConfigurationService commandConfigurationService = new CommandConfigurationService(vertexProvider, edgeProvider);
        ICommandProvider commandProvider = new CLICommandSelector(commandConfigurationService);

        gamePrinter.Print(game, 3, 9);

        Command? command = commandProvider.GetCommand(game);

        while (command != null)
        {
            if (!command.TryExecute(game)) 
            {
                Console.WriteLine("Failed to execute command.");
            }

            gamePrinter.Print(game, 3, 9);
            command = commandProvider.GetCommand(game);
        }

        Console.WriteLine("Failed to get command, exiting...");
    }
}
