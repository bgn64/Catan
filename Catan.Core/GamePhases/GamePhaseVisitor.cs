namespace Catan.Core;

public class GameSubphaseVisitor : IGameSubphaseVisitor
{
    Action<InitialPlacementPhase> _initialPlacementPhaseAction;
    Action<MainGamePhase> _mainGamePhaseAction;

    public GameSubphaseVisitor(Action<InitialPlacementPhase> initialPlacementPhaseAction, Action<MainGamePhase> mainGamePhaseAction)
    {
        _initialPlacementPhaseAction = initialPlacementPhaseAction;
        _mainGamePhaseAction = mainGamePhaseAction;
    }

    public void Visit(InitialPlacementPhase phase)
    {
        _initialPlacementPhaseAction(phase);
    }

    public void Visit(MainGamePhase phase)
    {
        _mainGamePhaseAction(phase);
    }
}
