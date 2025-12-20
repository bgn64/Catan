using Catan.Core;

namespace Catan.CLI;

public interface IBoardToStringConverter
{
    string ToString(Board board, int sideEdgeSize, int flatEdgeSize);
}
