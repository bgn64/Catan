namespace Catan.Core;

public interface IGameSubphaseVisitor
{
    void Visit(InitialPlacementPhase phase);
    void Visit(MainGamePhase phase);
}
