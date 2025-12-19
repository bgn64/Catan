namespace Catan.Core;

public class MainGamePhase : IGameSubphase
{ 
    internal void Start()
    {
    }

    internal event EventHandler? Complete;

    public void Accept(IGameSubphaseVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IEnumerable<Command> GetValidCommands()
    {
        return new List<Command>();
    }
}
