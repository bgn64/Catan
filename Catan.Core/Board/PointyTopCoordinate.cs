namespace Catan.Core;

public class PointyTopCoordinate : HexagonalCoordinate<PointyTopCoordinate>, IEquatable<PointyTopCoordinate>, IComparable<PointyTopCoordinate>
{
    internal int Q { get; }
    internal int R { get; }
    internal int S { get; }

    internal PointyTopCoordinate(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    } 

    public static PointyTopCoordinate Origin => new PointyTopCoordinate(0, 0, 0);

    public PointyTopCoordinate Add30Deg() => this + new PointyTopCoordinate(1, -1, 0);

    public PointyTopCoordinate Add90Deg() => this + new PointyTopCoordinate(0, -1, 1);

    public PointyTopCoordinate Add150Deg() => this + new PointyTopCoordinate(-1, 0, 1);

    public PointyTopCoordinate Add210Deg() => this + new PointyTopCoordinate(-1, 1, 0);

    public PointyTopCoordinate Add270Deg() => this + new PointyTopCoordinate(0, 1, -1);

    public PointyTopCoordinate Add330Deg() => this + new PointyTopCoordinate(1, 0, -1);

    public FlatTopCoordinate RotateClockwise()
    {
        return new FlatTopCoordinate(-R, -S, -Q);
    }

    public FlatTopCoordinate RotateCounterClockwise()
    {
        return new FlatTopCoordinate(Q, R, S);
    }	

    public override IEnumerable<PointyTopCoordinate> GetAdjacentCoordinates()
    {
        yield return Add30Deg();
        yield return Add90Deg();;
        yield return Add150Deg();
        yield return Add210Deg();
        yield return Add270Deg();
        yield return Add330Deg();
    }

    public override bool IsRightOf(PointyTopCoordinate other)
    {
        return Q > other.Q;
    }

    public override bool IsLeftOf(PointyTopCoordinate other)
    {
        return Q < other.Q;
    }

    public override bool IsAbove(PointyTopCoordinate other)
    {
        return S > other.S;
    }

    public override bool IsBelow(PointyTopCoordinate other)
    {
        return S < other.S;
    }

    public bool Equals(PointyTopCoordinate? other) => Equals(this, other);

    public override bool Equals(object? obj) => Equals(this, obj as PointyTopCoordinate);

    public override int GetHashCode() => HashCode.Combine(Q, R, S); 

    public int CompareTo(PointyTopCoordinate? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (IsBelow(other))
        {
            return 1;
        }
        else if (IsAbove(other))
        {
            return -1;
        }
        else if (IsLeftOf(other))
        {
            return -1;
        }
        else if (IsRightOf(other))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static bool operator ==(PointyTopCoordinate? left, PointyTopCoordinate? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(PointyTopCoordinate? left, PointyTopCoordinate? right)
    {
        return !Equals(left, right);
    }

    public static bool operator <(PointyTopCoordinate? left, PointyTopCoordinate? right)
    {
        return CompareTo(left, right) < 0;
    }

    public static bool operator >(PointyTopCoordinate? left, PointyTopCoordinate? right)
    {
        return CompareTo(left, right) > 0;
    }

    public static bool operator <=(PointyTopCoordinate? left, PointyTopCoordinate? right)
    {
        return CompareTo(left, right) <= 0;
    }

    public static bool operator >=(PointyTopCoordinate? left, PointyTopCoordinate? right)
    {
        return CompareTo(left, right) >= 0;
    }

    public static PointyTopCoordinate operator +(PointyTopCoordinate left, PointyTopCoordinate right)
    {
        return new PointyTopCoordinate(left.Q + right.Q, left.R + right.R, left.S + right.S);
    }

    public static PointyTopCoordinate operator -(PointyTopCoordinate left, PointyTopCoordinate right)
    {
        return new PointyTopCoordinate(left.Q - right.Q, left.R - right.R, left.S - right.S);
    }
}
