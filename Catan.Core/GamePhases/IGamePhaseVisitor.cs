namespace Catan.Core;

public interface IGamePhaseVisitor
{
    void Visit(InitialPlacementPhase phase);
    void Visit(MainGamePhase phase);
}