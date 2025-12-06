namespace Catan.Core;

public abstract class GameSubphase
{
    public abstract void Accept(IGameSubphaseVisitor visitor);
}
