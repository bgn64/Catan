using Catan.Core;

namespace Catan.CLI;

public class CommandConfigurationService : ICommandConfigurationService
{
    IVertexProvider _vertexProvider;
    IEdgeProvider _edgeProvider;

    public CommandConfigurationService(IVertexProvider vertexProvider, IEdgeProvider edgeProvider)
    {
        _vertexProvider = vertexProvider;
        _edgeProvider = edgeProvider;
    }

    public void ConfigureCommand(Command command, Game game)
    {
        command.Accept(new CommandVisitor(rollCommand => {},
                    placeSettlementCommand => placeSettlementCommand.Coordinate = _vertexProvider.GetVertex(game.Board),
                    placeRoadCommand => placeRoadCommand.Edge = _edgeProvider.GetEdge(game.Board)));
    }
}
