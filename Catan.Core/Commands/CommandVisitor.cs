namespace Catan.Core;

public class CommandVisitor : ICommandVisitor
{
    Action<RollCommand> _rollCommandAction;
    Action<PlaceSettlementCommand> _placeSettlementCommandAction;
    Action<PlaceRoadCommand> _placeRoadCommandAction;
    Action<EndTurnCommand> _endTurnCommandAction;

    public CommandVisitor(Action<RollCommand> rollCommandAction, 
            Action<PlaceSettlementCommand> placeSettlementCommandAction, 
            Action<PlaceRoadCommand> placeRoadCommandAction,
            Action<EndTurnCommand> endTurnCommandAction)
    {
        _rollCommandAction = rollCommandAction;
        _placeSettlementCommandAction = placeSettlementCommandAction;
        _placeRoadCommandAction = placeRoadCommandAction;
        _endTurnCommandAction = endTurnCommandAction;
    }

    public void Visit(RollCommand command)
    {
        _rollCommandAction(command);
    }

    public void Visit(PlaceSettlementCommand command)
    {
        _placeSettlementCommandAction(command);
    }

    public void Visit(PlaceRoadCommand command)
    {
        _placeRoadCommandAction(command);
    }

    public void Visit(EndTurnCommand command)
    {
        _endTurnCommandAction(command);
    }
}
