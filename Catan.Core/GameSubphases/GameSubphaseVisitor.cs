namespace Catan.Core;

public class GameSubphaseVisitor : IGameSubphaseVisitor
{
	Action<InitialRollPhase> _initialRollPhaseAction;
    Action<InitialPlacementPhase> _initialPlacementPhaseAction;
    Action<MainGamePhase> _mainGamePhaseAction;

    public GameSubphaseVisitor(Action<InitialRollPhase> initialRollPhaseAction, Action<InitialPlacementPhase> initialPlacementPhaseAction, Action<MainGamePhase> mainGamePhaseAction)
    {
		_initialRollPhaseAction = initialRollPhaseAction;
        _initialPlacementPhaseAction = initialPlacementPhaseAction;
        _mainGamePhaseAction = mainGamePhaseAction;
    }

	public void Visit(InitialRollPhase phase)
	{
		_initialRollPhaseAction(phase);
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
