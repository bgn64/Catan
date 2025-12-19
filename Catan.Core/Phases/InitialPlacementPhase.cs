namespace Catan.Core;

public class InitialPlacementPhase : IGameSubphase
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

    internal void Start()
    {
        _playerOrder = _game.InitialRolls
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();
        _game.CurrentPlayer = _playerOrder.First();
    }

    internal event EventHandler? Complete;

    public void Accept(IGameSubphaseVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IEnumerable<Command> GetValidCommands()
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

    public bool IsRoundOne { get; private set; }

    public bool HasPlacedSettlement { get; private set; }

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
            Complete?.Invoke(this, EventArgs.Empty);
        }
    }
}
