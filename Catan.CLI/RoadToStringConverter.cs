using Catan.Core;
using System.Text;

namespace Catan.CLI;

public class RoadToStringConverter : IEdgeToStringConverter
{
    Game _game;

    public RoadToStringConverter(Game game)
    {
        _game = game;
    }

    public string FlatEdgeToString(Edge edge, int flatEdgeSize)
    {
        StringBuilder builder = new StringBuilder();
        bool hexEdgeExists = _game.Board.GetAdjacentHexCoordinates(edge).Any(c => _game.Board.TryGetHex(c, out _));
        bool roadExists = _game.Board.TryGetRoad(edge, out _);

        for (int i = 0; i < flatEdgeSize; i++)
        {
            if (roadExists && i == flatEdgeSize / 2)
            {
                builder.Append("R");
            }
            else if (hexEdgeExists)
            {
                builder.Append("-");
            }
            else
            {
                builder.Append(" ");
            }
        }

        return builder.ToString();
    }

    public string SideEdgeToString(Edge edge, int sideEdgeSize, int sideEdgeIndex)
    {
        bool hexEdgeExists = _game.Board.GetAdjacentHexCoordinates(edge).Any(c => _game.Board.TryGetHex(c, out _));
        bool roadExists = _game.Board.TryGetRoad(edge, out _);
        Console.WriteLine($"roadExists for {edge}: {roadExists}");

        if (roadExists && sideEdgeIndex == sideEdgeSize / 2)
        {
            return "R";
        }
        else if (hexEdgeExists)
        {
            if (edge.Coordinate1.IsLeftOf(edge.Coordinate2) && edge.Coordinate1.IsBelow(edge.Coordinate2)
                || edge.Coordinate1.IsRightOf(edge.Coordinate2) && edge.Coordinate1.IsAbove(edge.Coordinate2))
            {
                return "/";
            }
            else
            {
                return "\\";
            }
        }
        else
        {
            return " ";
        }
    }
}
