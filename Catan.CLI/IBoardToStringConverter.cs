using Catan.Core;

namespace Catan.CLI;

public interface IBoardToStringConverter
{
  string ToString(Game game, int sideEdgeSize, int flatEdgeSize);
}
