using Catan.Core;
using System.Text;

namespace Catan.CLI;

public class ResourceToStringConverter : IHexToStringConverter
{
    public string TopLeftToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < sideEdgeIndex + 1 + flatEdgeSize; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string TopRightToString(FlatTopCoordinate hex,
            int sideEdgeSize, int sideEdgeIndex, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < 1 + sideEdgeIndex; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string MiddleLeftToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < sideEdgeSize + 1 + flatEdgeSize; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string MiddleRightToString(FlatTopCoordinate coordinate,
            int sideEdgeSize, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < 1 + sideEdgeSize; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string BottomLeftToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < sideEdgeSize - sideEdgeIndex + flatEdgeSize; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string BottomRightToString(FlatTopCoordinate hex,
            int sideEdgeSize, int sideEdgeIndex, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < sideEdgeSize - sideEdgeIndex; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string TopToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < 2 * (sideEdgeIndex + 1) + flatEdgeSize; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string MiddleToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < 2 * (sideEdgeSize + 1) + flatEdgeSize; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    public string BottomToString(FlatTopCoordinate hex,
            int sideEdgeSize, int flatEdgeSize, int sideEdgeIndex, Board board)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < 2 * (sideEdgeSize - sideEdgeIndex) + flatEdgeSize; i++)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }
}
