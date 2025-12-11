namespace Catan.Core;

public class InitialPlacementPhase : GameSubphase
{
	Game _game;
	List<Player> _playerOrder;

	internal InitialPlacementPhase(Game game)
	{
		_game = game;
		_playerOrder = new List<Player>();
		HasPlacedSettlement = false;
		IsRoundOne = true;	
	}

	internal override void OnPhaseStart()
	{
		_playerOrder = _game.InitialRolls
			.OrderByDescending(kvp => kvp.Value)
			.Select(kvp => kvp.Key)
			.ToList();
		_game.CurrentPlayer = _playerOrder.First();
	}
	
	public bool IsRoundOne { get; private set; }

	public bool HasPlacedSettlement { get; private set; }

	internal override IEnumerable<Command> GetValidCommands()
	{
		if (!HasPlacedSettlement)
		{
			PlaceSettlementCommand command = new PlaceSettlementCommand();
			command.CommandComplete += PlaceSettlementCommandComplete;
			yield return command;
		}
		
		if (HasPlacedSettlement)
		{
			PlaceRoadCommand command = new PlaceRoadCommand();
			command.CommandComplete += PlaceRoadCommandComplete;
			yield return command;
		}
	}

	void PlaceSettlementCommandComplete(object? sender, EventArgs e)
	{
		HasPlacedSettlement = true;
	}
	
	void PlaceRoadCommandComplete(object? sender, EventArgs e)
	{
		HasPlacedSettlement = false;

		if (IsRoundOne && _game.CurrentPlayer != _playerOrder.Last())
		{
			_game.CurrentPlayer = _playerOrder[_playerOrder.IndexOf(_game.CurrentPlayer) + 1];
		}
		else if (IsRoundOne && _game.CurrentPlayer == _playerOrder.Last())
		{
			IsRoundOne = false;
		}
		else if (!IsRoundOne && _game.CurrentPlayer != _playerOrder.First())
		{
			_game.CurrentPlayer = _playerOrder[_playerOrder.IndexOf(_game.CurrentPlayer) - 1];		
		}
		else if (!IsRoundOne && _game.CurrentPlayer == _playerOrder.First())
		{
			OnPhaseComplete(EventArgs.Empty);
		}
	}

	public override void Accept(IGameSubphaseVisitor visitor)
    {
        visitor.Visit(this);
    }
}
