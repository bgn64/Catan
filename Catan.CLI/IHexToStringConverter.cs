using Catan.Core;

namespace Catan.CLI;

public interface IHexToStringConverter
{
    string TopLeftToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board);

    string TopRightToString(FlatTopCoordinate hex,
            int sideEdgeSize, int sideEdgeIndex, Board board);

    string MiddleLeftToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, Board board);

    string MiddleRightToString(FlatTopCoordinate coordinate,
            int sideEdgeSize, Board board);

    string BottomLeftToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board);

    string BottomRightToString(FlatTopCoordinate hex,
            int sideEdgeSize, int sideEdgeIndex, Board board);

    string TopToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board);

    string MiddleToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, Board board);

    abstract string BottomToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board);
}
