using Catan.Core;

namespace Catan.CLI;

public interface IEdgeToStringConverter
{
  string FlatEdgeToString(Edge edge, int flatEdgeSize);

  string SideEdgeToString(Edge edge, int sideEdgeSize, int sideEdgeIndex);
}
