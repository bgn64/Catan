using Catan.Core;
using System.Text;

namespace Catan.CLI;

public class RoadToStringConverter : IEdgeToStringConverter
{
    public string FlatEdgeToString(Edge edge, int flatEdgeSize, Board board)
    {
        StringBuilder builder = new StringBuilder();
        bool hexEdgeExists = board.GetAdjacentHexCoordinates(edge).Any(c => board.TryGetHex(c, out _));
        bool roadExists = board.TryGetRoad(edge, out _);

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

    public string SideEdgeToString(Edge edge, int sideEdgeSize, int sideEdgeIndex, Board board)
    {
        bool hexEdgeExists = board.GetAdjacentHexCoordinates(edge).Any(c => board.TryGetHex(c, out _));
        bool roadExists = board.TryGetRoad(edge, out _);
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
