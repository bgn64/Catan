using Catan.Core;
using System;

namespace Catan.CLI;

public class GamePhaseHandler : IGameSubphaseVisitor
{
    InitialPlacementSubphaseHandler _initialPlacementSubphaseHandler;

    public GamePhaseHandler()
    {
        _initialPlacementSubphaseHandler = new InitialPlacementSubphaseHandler();
        IsComplete = false;
    }

    public bool IsComplete { get; private set; }

    public void Visit(InitialPlacementPhase phase)
    {
        phase.CurrentPhase.Accept(_initialPlacementSubphaseHandler);
    }

    public void Visit(MainGamePhase phase)
    {
        Console.WriteLine("Main game phase implemenation is not complete yet. Terminating the game phase visitor.");
        IsComplete = true;
    }
}
