using Catan.Core;
using System.Text;

namespace Catan.CLI;

public class BoardToStringConverter : IBoardToStringConverter
{
    public BoardToStringConverter(IHexToStringConverter hexToStringConverter,
        IVertexToStringConverter vertexToStringConverter,
        IEdgeToStringConverter edgeToStringConverter)
    {
        HexToStringConverter = hexToStringConverter;
        VertexToStringConverter = vertexToStringConverter;
        EdgeToStringConverter = edgeToStringConverter;
    }

    public IHexToStringConverter HexToStringConverter { get; }

    public IVertexToStringConverter VertexToStringConverter { get; }

    public IEdgeToStringConverter EdgeToStringConverter { get; }

    public BoardToStringConverter WithHexToStringConverter(IHexToStringConverter hexToStringConverter)
    {
        return new BoardToStringConverter(hexToStringConverter, VertexToStringConverter, EdgeToStringConverter);
    }

    public BoardToStringConverter WithVertexToStringConverter(IVertexToStringConverter vertexToStringConverter)
    {
        return new BoardToStringConverter(HexToStringConverter, vertexToStringConverter, EdgeToStringConverter);
    }

    public BoardToStringConverter WithEdgeToStringConverter(IEdgeToStringConverter edgeToStringConverter)
    {
        return new BoardToStringConverter(HexToStringConverter, VertexToStringConverter, edgeToStringConverter);
    }

    FlatTopCoordinate GetTopLeft(Board board)
    {
        FlatTopCoordinate topmost = FlatTopCoordinate.GetTopMost(board.GetHexCoordinates());
        FlatTopCoordinate leftmost = FlatTopCoordinate.GetLeftMost(board.GetHexCoordinates());
        FlatTopCoordinate topLeft = topmost;

        while (topLeft.IsRightOf(leftmost))
        {
            topLeft = topLeft.Add180Deg().Add180Deg().Add180Deg();
        }

        return topLeft;
    }

    public string ToString(Board board, int sideEdgeSize, int flatEdgeSize)
    {
        StringBuilder builder = new StringBuilder();
        FlatTopCoordinate topLeftBoardHex = GetTopLeft(board);
        FlatTopCoordinate rightMostBoardHex = FlatTopCoordinate.GetRightMost(board.GetHexCoordinates());
        FlatTopCoordinate bottomMostBoardHex = FlatTopCoordinate.GetBottomMost(board.GetHexCoordinates());
        rightMostBoardHex = rightMostBoardHex.Add0Deg().Add0Deg().Add0Deg();
        bottomMostBoardHex = bottomMostBoardHex.Add300Deg().Add240Deg();
        FlatTopCoordinate rowStartHex = topLeftBoardHex;

        while (!rowStartHex.IsBelow(bottomMostBoardHex))
        {
            FlatTopCoordinate currentHex = rowStartHex;

            while (!currentHex.IsRightOf(rightMostBoardHex))
            {
                FlatTopCoordinate topRightVertex = currentHex.Add60Deg();
                FlatTopCoordinate topLeftVertex = currentHex.Add120Deg();
                FlatTopCoordinate topRightHex = topRightVertex.Add0Deg();
                FlatTopCoordinate topLeftHex = topLeftVertex.Add180Deg();
                Edge.TryGetEdge(board, topLeftVertex, topRightVertex, out Edge? topEdge);

                if (topEdge == null)
                {
                    return "Failed to print board.";
                }

                builder.Append(HexToStringConverter.MiddleRightToString(topLeftHex, sideEdgeSize, board));
                builder.Append(VertexToStringConverter.ToString(topLeftVertex, board));
                builder.Append(EdgeToStringConverter.FlatEdgeToString(topEdge, flatEdgeSize, board));
                builder.Append(VertexToStringConverter.ToString(topRightVertex, board));
                builder.Append(HexToStringConverter.MiddleLeftToString(topRightHex, sideEdgeSize, flatEdgeSize, board));

                currentHex = currentHex.Add0Deg().Add0Deg().Add0Deg();
            }

                  builder.Append("\n");

            for (int sideEdgeIndex = 0; sideEdgeIndex < sideEdgeSize; sideEdgeIndex++)
            {
                currentHex = rowStartHex;

                while (!currentHex.IsRightOf(rightMostBoardHex))
                {
                    FlatTopCoordinate rightVertex = currentHex.Add0Deg();
                    FlatTopCoordinate topRightVertex = currentHex.Add60Deg();
                    FlatTopCoordinate topLeftVertex = currentHex.Add120Deg();
                    FlatTopCoordinate leftVertex = currentHex.Add180Deg();
                    FlatTopCoordinate topRightHex = topRightVertex.Add0Deg();
                    FlatTopCoordinate topLeftHex = topLeftVertex.Add180Deg();
                    Edge.TryGetEdge(board, rightVertex, topRightVertex, out Edge? topRightEdge);
                    Edge.TryGetEdge(board, topLeftVertex, leftVertex, out Edge? topLeftEdge);

                    if (topRightEdge == null || topLeftEdge == null)
                    {
                        return "Failed to print board.";
                    }

                    builder.Append(HexToStringConverter.BottomRightToString(topLeftHex, sideEdgeSize, sideEdgeIndex, board));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(topLeftEdge, sideEdgeSize, sideEdgeIndex, board));
                    builder.Append(HexToStringConverter.TopToString(currentHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex, board));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(topRightEdge, sideEdgeSize, sideEdgeIndex, board));
                    builder.Append(HexToStringConverter.BottomLeftToString(topRightHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex, board));

                    currentHex = currentHex.Add0Deg().Add0Deg().Add0Deg();
                }

                builder.Append("\n");
                  }

            currentHex = rowStartHex;

            while (!currentHex.IsRightOf(rightMostBoardHex))
            {
                FlatTopCoordinate rightVertex = currentHex.Add0Deg();
                FlatTopCoordinate leftVertex = currentHex.Add180Deg();
                FlatTopCoordinate farRightVertex = rightVertex.Add0Deg();
                Edge.TryGetEdge(board, rightVertex, farRightVertex, out Edge? farRightEdge);

                if (farRightEdge == null)
                {
                    return "Failed to print board.";
                }

                builder.Append(VertexToStringConverter.ToString(leftVertex, board));
                builder.Append(HexToStringConverter.MiddleToString(currentHex, sideEdgeSize, flatEdgeSize, board));
                builder.Append(VertexToStringConverter.ToString(rightVertex, board));
                builder.Append(EdgeToStringConverter.FlatEdgeToString(farRightEdge, flatEdgeSize, board));

                currentHex = currentHex.Add0Deg().Add0Deg().Add0Deg();
            }

                  builder.Append("\n");

            for (int sideEdgeIndex = 0; sideEdgeIndex < sideEdgeSize; sideEdgeIndex++)
            {
                currentHex = rowStartHex;

                while (!currentHex.IsRightOf(rightMostBoardHex))
                {
                    FlatTopCoordinate rightVertex = currentHex.Add0Deg();
                    FlatTopCoordinate leftVertex = currentHex.Add180Deg();
                    FlatTopCoordinate bottomLeftVertex = currentHex.Add240Deg();
                    FlatTopCoordinate bottomRightVertex = currentHex.Add300Deg();
                    FlatTopCoordinate bottomLeftHex = bottomLeftVertex.Add180Deg();
                    FlatTopCoordinate bottomRightHex = bottomRightVertex.Add0Deg();
                    Edge.TryGetEdge(board, leftVertex, bottomLeftVertex, out Edge? bottomLeftEdge);
                    Edge.TryGetEdge(board, bottomRightVertex, rightVertex, out Edge? bottomRightEdge);

                    if (bottomLeftEdge == null || bottomRightEdge == null)
                    {
                        return "Failed to print board";
                    }

                    builder.Append(HexToStringConverter.TopRightToString(bottomLeftHex, sideEdgeSize, sideEdgeIndex, board));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(bottomLeftEdge, sideEdgeSize, sideEdgeIndex, board));
                    builder.Append(HexToStringConverter.BottomToString(currentHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex, board));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(bottomRightEdge, sideEdgeSize, sideEdgeIndex, board));
                    builder.Append(HexToStringConverter.TopLeftToString(bottomRightHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex, board));

                    currentHex = currentHex.Add0Deg().Add0Deg().Add0Deg();
                }

                builder.Append("\n");
            }

            rowStartHex = rowStartHex.Add300Deg().Add240Deg();
        }

        return builder.ToString().TrimEnd();
    }
}
