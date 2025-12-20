namespace Catan.Core;

public class TurnPhase
{ 
    Game _game;

    internal TurnPhase(Game game)
    {
        _game = game;
    }

    internal void Start()
    {
        IsPostRoll = false;
    }

    internal event EventHandler? Complete;

    public IEnumerable<Command> GetValidCommands()
    {
        if (!IsPostRoll)
        {
            RollCommand command = new RollCommand();
            command.CommandComplete += RollCommandComplete;

            yield return command;
        }
        else
        {
            EndTurnCommand command = new EndTurnCommand();
            command.CommandComplete += EndTurnCommandComplete;

            yield return command;
        }
    }

    public bool IsPostRoll { get; private set; }
 
    void RollCommandComplete(object? sender, EventArgs e)
    {
        IsPostRoll = true;
    }

    void EndTurnCommandComplete(object? sender, EventArgs e)
    {
        Complete?.Invoke(this, EventArgs.Empty);
    }
}
