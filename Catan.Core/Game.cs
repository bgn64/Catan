namespace Catan.Core;

public class Game
{
    public GameSubphase CurrentPhase { get; }

    public bool IsGameOver { get; internal set; }
}

