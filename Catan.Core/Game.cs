namespace Catan.Core;

public class Game : ParentPhase<GameSubphase>
{
	List<Player> _players;
	Dictionary<Player, InitialRoll> _initialRolls;
	InitialRollPhase _initialRollPhase;
	InitialPlacementPhase _initialPlacementPhase;
	MainGamePhase _mainGamePhase;

	public Game()
	{
		_players = new List<Player>();
		_players.Add(new Player());
		_players.Add(new Player());
		_players.Add(new Player());
		_players.Add(new Player());
		
		CurrentPlayer = _players.First();

		_initialRolls = new Dictionary<Player, InitialRoll>();

		Board = new Board();
		RedDie = new Die();
		BlueDie = new Die();

		_initialRollPhase = new InitialRollPhase(this);

		foreach (Player player in _players)
		{
			_initialRolls.Add(player, new InitialRoll());
		}

		_initialPlacementPhase = new InitialPlacementPhase();
		_mainGamePhase = new MainGamePhase();
		
		_initialRollPhase.PhaseComplete += InitialRollPhaseComplete;
		_initialPlacementPhase.PhaseComplete += InitialPlacementPhaseComplete;
		_mainGamePhase.PhaseComplete += MainGamePhaseComplete;

		CurrentPhase = _initialRollPhase; 
	}

	public IEnumerable<Player> Players => _players;

	public Player CurrentPlayer { get; internal set; }

	public IReadOnlyDictionary<Player, InitialRoll> InitialRolls => _initialRolls;

	public Board Board { get; private set; }

	public Die RedDie { get; private set; }

	public Die BlueDie { get; private set; }

	internal void RollDice()
	{
		RedDie.Roll();
		BlueDie.Roll();
	}

	internal void AddInitialRoll(Player player, Die roll)
	{
		_initialRolls[player].AddRoll(roll);
	}

	void InitialRollPhaseComplete(object? sender, EventArgs e)
	{
		CurrentPhase = _initialPlacementPhase;
	}

	void InitialPlacementPhaseComplete(object? sender, EventArgs e)
	{
		CurrentPhase = _mainGamePhase;
	}

	void MainGamePhaseComplete(object? sender, EventArgs e)
	{
		CurrentPhase = null;
	}
}

