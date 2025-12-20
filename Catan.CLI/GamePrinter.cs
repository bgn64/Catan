using Catan.Core;
using System;

namespace Catan.CLI;

public class GamePrinter
{
    IBoardToStringConverter _boardToStringConverter;
    GameSubphasePrinter _gameSubphasePrinter;

    public GamePrinter(IBoardToStringConverter boardToStringConverter)
    {
        _boardToStringConverter = boardToStringConverter;
        _gameSubphasePrinter = new GameSubphasePrinter(1);
    }

    public void Print(Game game, int sideEdgeSize, int flatEdgeSize)
    {
        Console.WriteLine($"Players.Count: {game.Players.Count()}");
        Console.WriteLine($"CurrentPlayer.Id: {game.CurrentPlayer.Id}");
        Console.WriteLine($"RedDie.Value: {game.RedDie.Value}");
        Console.WriteLine($"BlueDie.Value: {game.BlueDie.Value}");

        PrintInitialRolls(game);

        _gameSubphasePrinter.Print(game.CurrentPhase);

        Console.WriteLine(_boardToStringConverter.ToString(game.Board, sideEdgeSize, flatEdgeSize));
    }

    void PrintInitialRolls(Game game)
    {
        foreach (Player player in game.Players)
        {
            Console.WriteLine($"Player Id: {player.Id}");
            InitialRoll initialRoll = game.InitialRolls[player];
            Console.Write($"\tInitialRoll: ");
            foreach (Die roll in initialRoll.Rolls)
            {
                Console.Write(roll.Value);
            }
            Console.WriteLine();
        }
    }

    class GameSubphasePrinter : IGameSubphaseVisitor
    {
        int _indent;

        public GameSubphasePrinter(int indent)
        {
            _indent = indent;
        }

        public void Print(IGameSubphase? phase)
        {
            if (phase == null)
            {
                PrintIndent();
                Console.WriteLine("null");

                return;
            }

            phase.Accept(this);
        }

        public void Visit(InitialRollPhase phase)
        {
            PrintIndent();
            Console.WriteLine(nameof(InitialRollPhase));
        }

        public void Visit(InitialPlacementPhase phase)
        {
            Console.WriteLine(nameof(InitialPlacementPhase));
        }

        public void Visit(MainGamePhase phase)
        {
            Console.WriteLine(nameof(MainGamePhase));
        }

        void PrintIndent()
        {
            for (int i = 0; i < _indent; i++)
            {
                Console.Write("\t");
            }
        }
    }
}
