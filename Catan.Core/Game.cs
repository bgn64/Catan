namespace Catan.Core;

public class Game
{
	public Game()
	{
		Board = new Board();
		CurrentPhase = new InitialPlacementPhase();
	}

	public Board Board { get; private set; }

    public GameSubphase CurrentPhase { get; private set; }
}

