namespace Catan.Core;

public class CommandVisitor : ICommandVisitor
{
    Action<RollCommand> _rollCommandAction;
    Action<PlaceSettlementCommand> _placeSettlementCommandAction;
    Action<PlaceRoadCommand> _placeRoadCommandAction;

    public CommandVisitor(Action<RollCommand> rollCommandAction, Action<PlaceSettlementCommand> placeSettlementCommandAction, Action<PlaceRoadCommand> placeRoadCommandAction)
    {
        _rollCommandAction = rollCommandAction;
        _placeSettlementCommandAction = placeSettlementCommandAction;
        _placeRoadCommandAction = placeRoadCommandAction;
    }

    public void Visit(RollCommand phase)
    {
        _rollCommandAction(phase);
    }

    public void Visit(PlaceSettlementCommand phase)
    {
        _placeSettlementCommandAction(phase);
    }

    public void Visit(PlaceRoadCommand phase)
    {
        _placeRoadCommandAction(phase);
    }
}
