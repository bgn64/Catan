namespace Catan.Core;

public class FlatTopCoordinate : HexagonalCoordinate<FlatTopCoordinate>, IEquatable<FlatTopCoordinate>, IComparable<FlatTopCoordinate>
{
    internal int Q { get; }
    internal int R { get; }
    internal int S { get; }

    internal FlatTopCoordinate(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    } 

    public static FlatTopCoordinate Origin => new FlatTopCoordinate(0, 0, 0);

    public FlatTopCoordinate Add0Deg() => this + new FlatTopCoordinate(1, 0, -1);

    public FlatTopCoordinate Add60Deg() => this + new FlatTopCoordinate(1, -1, 0);

    public FlatTopCoordinate Add120Deg() => this + new FlatTopCoordinate(0, -1, 1);

    public FlatTopCoordinate Add180Deg() => this + new FlatTopCoordinate(-1, 0, 1);

    public FlatTopCoordinate Add240Deg() => this + new FlatTopCoordinate(-1, 1, 0);

    public FlatTopCoordinate Add300Deg() => this + new FlatTopCoordinate(0, 1, -1);

    public PointyTopCoordinate RotateClockwise()
    {
        return new PointyTopCoordinate(Q, R, S);
    }

    public PointyTopCoordinate RotateCounterClockwise()
    {
        return new PointyTopCoordinate(-S, -Q, -R);
    }

    public override IEnumerable<FlatTopCoordinate> GetAdjacentCoordinates()
    {
        yield return Add0Deg();
        yield return Add60Deg();
        yield return Add120Deg();
        yield return Add180Deg();
        yield return Add240Deg();
        yield return Add300Deg();
    }

    public override bool IsRightOf(FlatTopCoordinate other)
    {
        return Q - S > other.Q - other.S;
    }

    public override bool IsLeftOf(FlatTopCoordinate other)
    {
        return Q - S < other.Q - other.S;
    }

    public override bool IsAbove(FlatTopCoordinate other)
    {
        return R < other.R;
    }

    public override bool IsBelow(FlatTopCoordinate other)
    {
        return R > other.R;
    }

    public bool Equals(FlatTopCoordinate? other) => Equals(this, other);
 
    public override bool Equals(object? obj) => Equals(this, obj as FlatTopCoordinate);

    public override int GetHashCode() => HashCode.Combine(Q, R, S);

    public int CompareTo(FlatTopCoordinate? other)
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

    public static bool operator ==(FlatTopCoordinate? left, FlatTopCoordinate? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(FlatTopCoordinate? left, FlatTopCoordinate? right)
    {
        return !Equals(left, right);
    }

    public static bool operator <(FlatTopCoordinate? left, FlatTopCoordinate? right)
    {
        return CompareTo(left, right) < 0;
    }

    public static bool operator >(FlatTopCoordinate? left, FlatTopCoordinate? right)
    {
        return CompareTo(left, right) > 0;
    }

    public static bool operator <=(FlatTopCoordinate? left, FlatTopCoordinate? right)
    {
        return CompareTo(left, right) <= 0;
    }

    public static bool operator >=(FlatTopCoordinate? left, FlatTopCoordinate? right)
    {
        return CompareTo(left, right) >= 0;
    }

    public static FlatTopCoordinate operator +(FlatTopCoordinate left, FlatTopCoordinate right)
    {
        return new FlatTopCoordinate(left.Q + right.Q, left.R + right.R, left.S + right.S);
    }

    public static FlatTopCoordinate operator -(FlatTopCoordinate left, FlatTopCoordinate right)
    {
        return new FlatTopCoordinate(left.Q - right.Q, left.R - right.R, left.S - right.S);
    }

    public override string ToString()
    {
        return $"({Q}, {R}, {S})";
    }
}
