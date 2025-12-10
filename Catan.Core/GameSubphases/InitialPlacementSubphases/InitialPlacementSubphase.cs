namespace Catan.Core;

public abstract class InitialPlacementSubphase
{
    public abstract void Accept(IInitialPlacementSubphaseVisitor visitor);
}