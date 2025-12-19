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

    FlatTopCoordinate GetTopLeft(Game game)
    {
        FlatTopCoordinate topmost = FlatTopCoordinate.GetTopMost(game.Board.GetHexCoordinates());
        FlatTopCoordinate leftmost = FlatTopCoordinate.GetLeftMost(game.Board.GetHexCoordinates());
        FlatTopCoordinate topLeft = topmost;

        while (topLeft.IsRightOf(leftmost))
        {
            topLeft = topLeft.Add180Deg().Add180Deg().Add180Deg();
        }

        return topLeft;
    }

    public string ToString(Game game, int sideEdgeSize, int flatEdgeSize)
    {
        StringBuilder builder = new StringBuilder();
        FlatTopCoordinate topLeftBoardHex = GetTopLeft(game);
        FlatTopCoordinate rightMostBoardHex = FlatTopCoordinate.GetRightMost(game.Board.GetHexCoordinates());
        FlatTopCoordinate bottomMostBoardHex = FlatTopCoordinate.GetBottomMost(game.Board.GetHexCoordinates());
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
                Edge.TryGetEdge(game.Board, topLeftVertex, topRightVertex, out Edge? topEdge);

                if (topEdge == null)
                {
                    return "Failed to print board.";
                }

                builder.Append(HexToStringConverter.MiddleRightToString(topLeftHex, sideEdgeSize));
                builder.Append(VertexToStringConverter.ToString(topLeftVertex));
                builder.Append(EdgeToStringConverter.FlatEdgeToString(topEdge, flatEdgeSize));
                builder.Append(VertexToStringConverter.ToString(topRightVertex));
                builder.Append(HexToStringConverter.MiddleLeftToString(topRightHex, sideEdgeSize, flatEdgeSize));

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
                    Edge.TryGetEdge(game.Board, rightVertex, topRightVertex, out Edge? topRightEdge);
                    Edge.TryGetEdge(game.Board, topLeftVertex, leftVertex, out Edge? topLeftEdge);

                    if (topRightEdge == null || topLeftEdge == null)
                    {
                        return "Failed to print board.";
                    }

                    builder.Append(HexToStringConverter.BottomRightToString(topLeftHex, sideEdgeSize, sideEdgeIndex));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(topLeftEdge, sideEdgeSize, sideEdgeIndex));
                    builder.Append(HexToStringConverter.TopToString(currentHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(topRightEdge, sideEdgeSize, sideEdgeIndex));
                    builder.Append(HexToStringConverter.BottomLeftToString(topRightHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex));

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
                Edge.TryGetEdge(game.Board, rightVertex, farRightVertex, out Edge? farRightEdge);

                if (farRightEdge == null)
                {
                    return "Failed to print board.";
                }

                builder.Append(VertexToStringConverter.ToString(leftVertex));
                builder.Append(HexToStringConverter.MiddleToString(currentHex, sideEdgeSize, flatEdgeSize));
                builder.Append(VertexToStringConverter.ToString(rightVertex));
                builder.Append(EdgeToStringConverter.FlatEdgeToString(farRightEdge, flatEdgeSize));

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
                    Edge.TryGetEdge(game.Board, leftVertex, bottomLeftVertex, out Edge? bottomLeftEdge);
                    Edge.TryGetEdge(game.Board, bottomRightVertex, rightVertex, out Edge? bottomRightEdge);

                    if (bottomLeftEdge == null || bottomRightEdge == null)
                    {
                        return "Failed to print board";
                    }

                    builder.Append(HexToStringConverter.TopRightToString(bottomLeftHex, sideEdgeSize, sideEdgeIndex));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(bottomLeftEdge, sideEdgeSize, sideEdgeIndex));
                    builder.Append(HexToStringConverter.BottomToString(currentHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex));
                    builder.Append(EdgeToStringConverter.SideEdgeToString(bottomRightEdge, sideEdgeSize, sideEdgeIndex));
                    builder.Append(HexToStringConverter.TopLeftToString(bottomRightHex, sideEdgeSize, flatEdgeSize, sideEdgeIndex));

                    currentHex = currentHex.Add0Deg().Add0Deg().Add0Deg();
                }

                builder.Append("\n");
            }

            rowStartHex = rowStartHex.Add300Deg().Add240Deg();
        }

        return builder.ToString().TrimEnd();
    }
}
