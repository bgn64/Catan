using Catan.Core;

namespace Catan.CLI;

public interface IHexToStringConverter
{
  string TopLeftToString(FlatTopCoordinate hex,
      int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex);

  string TopRightToString(FlatTopCoordinate hex,
      int sideEdgeSize, int sideEdgeIndex);
 
  string MiddleLeftToString(FlatTopCoordinate hex,
      int sideEdgeSize, int flatEdgeSize);

  string MiddleRightToString(FlatTopCoordinate coordinate,
      int sideEdgeSize);

  string BottomLeftToString(FlatTopCoordinate hex,
      int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex);

  string BottomRightToString(FlatTopCoordinate hex,
      int sideEdgeSize, int sideEdgeIndex);

  string TopToString(FlatTopCoordinate hex,
      int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex);
  
  string MiddleToString(FlatTopCoordinate hex,
      int sideEdgeSize, int flatEdgeSize);

  abstract string BottomToString(FlatTopCoordinate hex,
      int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex);
}
