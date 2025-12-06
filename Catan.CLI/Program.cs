using Catan.Core;
using System;

namespace Catan.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var game = new Game();
        bool isVisitorComplete = false;

        while (!isVisitorComplete)
        {
            game.CurrentPhase.Accept(new GameSubphaseVisitor(
                initialPlacementPhase => {
                    initialPlacementPhase.CurrentPhase.Accept(new InitialPlacementSubphaseVisitor(
                        placeSettlementPhase => {
                            // Implement the logic for the place settlement phase
                        },
                        placeRoadPhase => {
                            // Implement the logic for the place road phase
                        }
                    ));
                },
                mainGamePhase => {
                    Console.WriteLine("Main game phase implemenation is not complete yet. Terminating the game phase visitor.");
                    isVisitorComplete = true;
                }
            ));
        }
    }
}
