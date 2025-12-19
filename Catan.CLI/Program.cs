using Catan.Core;
using System;

namespace Catan.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Catan game!");
        Game game = new Game();
        IHexToStringConverter hexToStringConverter = new ResourceToStringConverter(game);
        IVertexToStringConverter vertexToStringConverter = new SettlementToStringConverter(game);
        IEdgeToStringConverter edgeToStringConverter = new RoadToStringConverter(game);
        BoardToStringConverter boardToStringConverter = new BoardToStringConverter(hexToStringConverter, vertexToStringConverter, edgeToStringConverter);
        GamePrinter gamePrinter = new GamePrinter(boardToStringConverter);
        IVertexProvider vertexProvider = new CLIVertexSelector(game, boardToStringConverter);
        IEdgeProvider edgeProvider = new CLIEdgeSelector(game, boardToStringConverter);
        ICommandConfigurationService commandConfigurationService = new CommandConfigurationService(game, vertexProvider, edgeProvider);
        ICommandProvider commandProvider = new CLICommandSelector(game, commandConfigurationService);

        gamePrinter.Print(game);

        Command? command = commandProvider.GetCommand();

        while (command != null)
        {
            if (!command.TryExecute(game)) 
            {
                Console.WriteLine("Failed to execute command.");
            }

            gamePrinter.Print(game);
            command = commandProvider.GetCommand();
        }

        Console.WriteLine("Failed to get command, exiting...");
    }
}
