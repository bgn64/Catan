namespace Catan.Core;

public class MainGamePhase : IGameSubphase
{ 
    Game _game;
    List<Player> _playerOrder;

    internal MainGamePhase(Game game)
    {
        _game = game;
        _playerOrder = new List<Player>();
    }

    public TurnPhase? TurnPhase { get; private set; }

    internal void Start()
    {
        _playerOrder = _game.InitialRolls
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();
        _game.CurrentPlayer = _playerOrder.First();

        TurnPhase = new TurnPhase(_game);
        TurnPhase.Complete += TurnCompleteEventHandler;
        TurnPhase.Start();
    }

    internal event EventHandler? Complete;

    public void Accept(IGameSubphaseVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IEnumerable<Command> GetValidCommands()
    {
        return TurnPhase.GetValidCommands();
    }

    void TurnCompleteEventHandler(object? sender, EventArgs e)
    {
        if (_game.CurrentPlayer == _playerOrder.Last())
        {
            _game.CurrentPlayer = _playerOrder.First();
        }
        else
        {
            _game.CurrentPlayer = _playerOrder[_playerOrder.IndexOf(_game.CurrentPlayer) + 1];
        }

        TurnPhase = new TurnPhase(_game);
        TurnPhase.Complete += TurnCompleteEventHandler;
        TurnPhase.Start();
    }
}
