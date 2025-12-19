using Catan.Core;

namespace Catan.CLI;

public class CommandConfigurationService : ICommandConfigurationService, ICommandVisitor
{
    Game _game;
    IVertexProvider _vertexProvider;
    IEdgeProvider _edgeProvider;

    public CommandConfigurationService(Game game, IVertexProvider vertexProvider, IEdgeProvider edgeProvider)
    {
        _game = game;
        _vertexProvider = vertexProvider;
        _edgeProvider = edgeProvider;
    }

    public void ConfigureCommand(Command command)
    {
        command.Accept(this);
    }

    public void Visit(RollCommand command)
    {
    }

    public void Visit(PlaceSettlementCommand command)
    {
        command.Coordinate = _vertexProvider.GetVertex();
    }

    public void Visit(PlaceRoadCommand command)
    {
        command.Edge = _edgeProvider.GetEdge();
    }
}
