namespace Catan.Core;

public class InitialRollPhase : IGameSubphase
{
    Game _game;
    List<Player> _playersToRoll;

    internal InitialRollPhase(Game game)
    {
        _game = game;	
        _playersToRoll = new List<Player>();
    }

    internal void OnPhaseStart()
    {
        _playersToRoll = _game.Players.ToList();
        _game.CurrentPlayer = _playersToRoll.First();
    }

    internal event EventHandler? Complete;

    public void Accept(IGameSubphaseVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IEnumerable<Command> GetValidCommands()
    {
        RollCommand command = new RollCommand();
        command.CommandComplete += RollCommandComplete;
        yield return command;
    }

    void RollCommandComplete(object? sender, EventArgs e)
    {
        _game.AddInitialRoll(_game.CurrentPlayer, new Die(_game.RedDie));
        _playersToRoll.Remove(_game.CurrentPlayer);

        if (_playersToRoll.Count != 0)
        {
            _game.CurrentPlayer = _playersToRoll.First();
        }
        else 
        {
            IEnumerable<Player> playersWithUniqueInitialRoll = _game.InitialRolls
                .GroupBy(kvp => kvp.Value)
                .Where(g => g.Count() == 1)
                .Select(g => g.First().Key);
            _playersToRoll = _game.Players.Except(playersWithUniqueInitialRoll).ToList();

            if (_playersToRoll.Count == 0)
            {
                Complete?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _game.CurrentPlayer = _playersToRoll.First();
            }
        }
    }
}
