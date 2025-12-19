using Catan.Core;
using System.Text;

namespace Catan.CLI;

public class SettlementToStringConverter : IVertexToStringConverter
{
    Game _game;

    public SettlementToStringConverter(Game game)
    {
        _game = game;
    }

    public string ToString(FlatTopCoordinate vertex)
    {
        if (_game.Board.TryGetSettlement(vertex, out Settlement? settlement))
        {
            return "S";
        }
        else if(_game.Board.GetAdjacentHexCoordinates(vertex).Any(c => _game.Board.TryGetHex(c, out _)))
        {
            return "*";
        }
        else
        {
            return " ";
        }
    }
}
