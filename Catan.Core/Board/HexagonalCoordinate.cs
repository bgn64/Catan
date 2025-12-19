namespace Catan.Core;

public abstract class HexagonalCoordinate<T> where T : HexagonalCoordinate<T>, IEquatable<T>, IComparable<T>
{
    public abstract IEnumerable<T> GetAdjacentCoordinates(); 

    public abstract bool IsRightOf(T other);

    public abstract bool IsLeftOf(T other);

    public abstract bool IsAbove(T other);

    public abstract bool IsBelow(T other);

    public bool IsAdjacent(T other)
    {
        foreach (T coordinate in GetAdjacentCoordinates()) 
        {
            if (other.Equals(coordinate))
            {
                return true;
            }
        }

        return false;
    }

    public static T GetRightMost(IEnumerable<T> coordinates)
    {
        return GetMost(coordinates, (left, right) => left.IsRightOf(right));
    }

    public static T GetLeftMost(IEnumerable<T> coordinates)
    {
        return GetMost(coordinates, (left, right) => left.IsLeftOf(right));
    }

    public static T GetTopMost(IEnumerable<T> coordinates)
    {
        return GetMost(coordinates, (left, right) => left.IsAbove(right));
    }

    public static T GetBottomMost(IEnumerable<T> coordinates)
    {
        return GetMost(coordinates, (left, right) => left.IsBelow(right));
    }

    static T GetMost(IEnumerable<T> coordinates, Func<T, T, bool> more)
    {
        T most = coordinates.First();

        foreach (T coordinate in coordinates)
        {
            if (more(coordinate, most))
            {
                most = coordinate;
            }
        }

        return most;
    }

    protected static bool Equals(T? left, T? right)
    {
        if (left == null && right == null)
        {
            return true;
        }
        else if (left != null && right != null)
        {
            return !left.IsRightOf(right) &&
                !left.IsLeftOf(right) &&
                !left.IsAbove(right) &&
                !left.IsBelow(right);
        }
        else 
        {
            return false;
        }
    }

    protected static int CompareTo(T? left, T? right)
    {
        if (left == null && right == null)
        {
            return 0;
        }
        else if (left == null && right != null)
        {
            return -1;
        }
        else if (left != null && right == null)
        {
            return 1;
        }
        else if (left!.IsBelow(right!))
        {
            return 1;
        }
        else if (left!.IsAbove(right!))
        {
            return -1;
        }
        else if (left!.IsRightOf(left!))
        {
            return 1;
        }
        else if (left!.IsLeftOf(right!))
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
