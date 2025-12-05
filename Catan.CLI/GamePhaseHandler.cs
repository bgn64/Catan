using Catan.Core;
using System;

namespace Catan.CLI;

public class GamePhaseHandler : IGamePhaseVisitor
{
    InitialPlacementSubphaseHandler _initialPlacementSubphaseVisitor;

    public GamePhaseHandler()
    {
        _initialPlacementSubphaseVisitor = new InitialPlacementSubphaseHandler();
        IsComplete = false;
    }

    public bool IsComplete { get; private set; }

    public void Visit(InitialPlacementPhase phase)
    {
        phase.CurrentPhase.Accept(_initialPlacementSubphaseVisitor);
    }

    public void Visit(MainGamePhase phase)
    {
        Console.WriteLine("Main game phase implemenation is not complete yet. Terminating the game phase visitor.");
        IsComplete = true;
    }
}