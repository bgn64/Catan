namespace Catan.Core;

public class Edge : IEquatable<Edge>
{
    public FlatTopCoordinate Coordinate1 { get; }
    public FlatTopCoordinate Coordinate2 { get; }

    Edge(FlatTopCoordinate coordinate1, FlatTopCoordinate coordinate2)
    {
        Coordinate1 = coordinate1;
        Coordinate2 = coordinate2;
    }

    public static bool TryGetEdge(Board board, FlatTopCoordinate c1, FlatTopCoordinate c2, out Edge? edge)
    {
        if (c1.IsAdjacent(c2) && board.IsVertexCoordinate(c1) && board.IsVertexCoordinate(c2))
        {
            edge = new Edge(c1, c2);

            return true;
        }
        else
        {
            edge = null;

            return false;
        }
    } 

    public bool Equals(Edge? other) => other is not null && 
        ((Coordinate1 == other.Coordinate1 && Coordinate2 == other.Coordinate2) || (Coordinate2 == other.Coordinate1 && Coordinate1 == other.Coordinate2));

    public override bool Equals(object? obj) => Equals(obj as Edge);

    public override int GetHashCode()
    {
        FlatTopCoordinate lesser = Coordinate1 < Coordinate2 ? Coordinate1 : Coordinate2;
        FlatTopCoordinate greater = lesser == Coordinate2 ? Coordinate1 : Coordinate2;

        return  HashCode.Combine(lesser, greater);
    }

    public static bool operator ==(Edge? left, Edge? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Edge? left, Edge? right)
    {
        if (left is null)
        {
            return right is not null;
        }

        return !left.Equals(right);
    }

    public override string ToString()
    {
        return $"({Coordinate1}, {Coordinate2})";
    }
}
