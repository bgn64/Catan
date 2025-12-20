using Catan.Core;
using System.Text;

namespace Catan.CLI;

public class SettlementToStringConverter : IVertexToStringConverter
{
    public string ToString(FlatTopCoordinate vertex, Board board)
    {
        if (board.TryGetSettlement(vertex, out Settlement? settlement))
        {
            return "S";
        }
        else if(board.GetAdjacentHexCoordinates(vertex).Any(c => board.TryGetHex(c, out _)))
        {
            return "*";
        }
        else
        {
            return " ";
        }
    }
}
