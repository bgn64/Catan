namespace Catan.Core;

public interface ICommandVisitor
{
    void Visit(RollCommand command);

    void Visit(PlaceSettlementCommand command);

    void Visit(PlaceRoadCommand command);

    void Visit(EndTurnCommand command);
}
