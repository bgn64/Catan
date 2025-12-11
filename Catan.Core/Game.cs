namespace Catan.Core;

public class Game : ParentPhase<GameSubphase>
{
	List<Player> _players;
	Dictionary<Player, InitialRoll> _initialRolls;
	Dictionary<Player, int> _unplayedSettlements;
	Dictionary<Player, int> _unplayedRoads;

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

		foreach (Player player in _players)
		{
			_initialRolls.Add(player, new InitialRoll());
		}

		_unplayedSettlements = new Dictionary<Player, int>();

		foreach (Player player in _players)
		{
			_unplayedSettlements.Add(player, 5);
		}

		_unplayedRoads = new Dictionary<Player, int>();

		foreach (Player player in _players)
		{
			_unplayedRoads.Add(player, 13);
		}

		_initialRollPhase = new InitialRollPhase(this);
		_initialPlacementPhase = new InitialPlacementPhase(this);
		_mainGamePhase = new MainGamePhase();
		
		_initialRollPhase.PhaseComplete += InitialRollPhaseComplete;
		_initialPlacementPhase.PhaseComplete += InitialPlacementPhaseComplete;
		_mainGamePhase.PhaseComplete += MainGamePhaseComplete;

		CurrentPhase = _initialRollPhase; 
	}

	public new IEnumerable<Command> GetValidCommands() => base.GetValidCommands();

	public IEnumerable<Player> Players => _players;

	public Player CurrentPlayer { get; internal set; }

	public IReadOnlyDictionary<Player, InitialRoll> InitialRolls => _initialRolls;

	public Board Board { get; private set; }

	public Die RedDie { get; private set; }

	public Die BlueDie { get; private set; }

	public IReadOnlyDictionary<Player, int> UnplayedSettlements => _unplayedSettlements;
	
	public IReadOnlyDictionary<Player, int> UnplayedRoads => _unplayedRoads;

	internal void RollDice()
	{
		RedDie.Roll();
		BlueDie.Roll();
	}

	internal void AddInitialRoll(Player player, Die roll)
	{
		_initialRolls[player].AddRoll(roll);
	}

	bool CanGetUnplayedSettlementCore(Player player)
	{
		return _unplayedSettlements[player] > 0;
	}

	internal bool CanGetUnplayedSettlement(Player player)
	{
		return CanGetUnplayedSettlementCore(player);
	}

	internal bool TryGetUnplayedSettlement(Player player, out Settlement? settlement)
	{
		if (CanGetUnplayedSettlementCore(player))
		{
			_unplayedSettlements[player]--;
			settlement = new Settlement(player);

			return true;
		}
		else
		{
			settlement = null;

			return false;
		}
	}	
	
	bool CanGetUnplayedRoadCore(Player player)
	{
		return _unplayedRoads[player] > 0;
	}

	internal bool CanGetUnplayedRoad(Player player)
	{
		return CanGetUnplayedRoadCore(player);
	}

	internal bool TryGetUnplayedRoad(Player player, out Road? road)
	{
		if (CanGetUnplayedRoadCore(player))
		{
			_unplayedRoads[player]--;
			road = new Road(player);

			return true;
		}
		else
		{
			road = null;

			return false;
		}
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

