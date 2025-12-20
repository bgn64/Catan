namespace Catan.Core;

public class Board
{
    Dictionary<FlatTopCoordinate, Settlement> _settlements;
    Dictionary<Edge, Road> _roads;
    Dictionary<FlatTopCoordinate, Hex> _hexes;

    internal Board()
    {
        _settlements = new Dictionary<FlatTopCoordinate, Settlement>();
        _roads = new Dictionary<Edge, Road>();
        _hexes = new Dictionary<FlatTopCoordinate, Hex>();

        List<FlatTopCoordinate> hexesToAdd = new List<FlatTopCoordinate>();
        hexesToAdd.Add(FlatTopCoordinate.Origin);

        for (int i = 0; i < 3; i++)
        {
            List<FlatTopCoordinate> newHexesToAdd = new List<FlatTopCoordinate>();

            foreach (FlatTopCoordinate hex in hexesToAdd)
            {
                _hexes.Add(hex, new Hex());
            }

            foreach (FlatTopCoordinate hex in hexesToAdd)
            {
                foreach (FlatTopCoordinate adjacentCoordinate in GetAdjacentHexCoordinates(hex))
                {
                    if (!_hexes.ContainsKey(adjacentCoordinate) &&
                            !newHexesToAdd.Contains(adjacentCoordinate))
                    {
                        newHexesToAdd.Add(adjacentCoordinate);
                    }
                }
            }

            hexesToAdd = newHexesToAdd;
        }
    }

    public IEnumerable<FlatTopCoordinate> GetHexCoordinates()
    {
        return _hexes.Keys;
    }

    public IEnumerable<FlatTopCoordinate> GetHexVertexCoordinates()
    {
        HashSet<FlatTopCoordinate> coordinates = new HashSet<FlatTopCoordinate>();

        foreach (FlatTopCoordinate hex in _hexes.Keys)
        {
            coordinates.UnionWith(hex.GetAdjacentCoordinates());
        }

        return coordinates;
    }

    public bool TryGetHex(FlatTopCoordinate coordinate, out Hex? hex)
    {
        hex = null;

        return IsHexCoordinate(coordinate) &&
            _hexes.TryGetValue(coordinate, out hex);
    }

    public bool TryGetSettlement(FlatTopCoordinate coordinate, out Settlement? settlement)
    {
        settlement = null;

        return IsVertexCoordinate(coordinate) &&
            _settlements.TryGetValue(coordinate, out settlement);
    }	

    internal bool CanPlaceSettlement(FlatTopCoordinate coordinate)
    {
        return IsVertexCoordinate(coordinate) &&
            !_settlements.ContainsKey(coordinate);
    }

    internal bool TryPlaceSettlement(FlatTopCoordinate coordinate, Settlement settlement)
    {
        return IsVertexCoordinate(coordinate) &&
            _settlements.TryAdd(coordinate, settlement);
    }

    public bool TryGetRoad(Edge edge, out Road? road)
    {
        road = null;

        return _roads.TryGetValue(edge, out road);
    }	

    internal bool CanPlaceRoad(Edge edge)
    {
        return !_roads.ContainsKey(edge);
    }

    internal bool TryPlaceRoad(Edge edge, Road road)
    {
        return _roads.TryAdd(edge, road);
    }

    public bool IsHexCoordinate(FlatTopCoordinate coordinate)
    {
        int columnIndex = coordinate.Q - coordinate.S;

        if (columnIndex % 3 != 0)
        {
            return false;
        }

        if (columnIndex % 6 == 0)
        {
            return coordinate.R % 2 == 0;
        }
        else
        { 
            return coordinate.R % 2 != 0;
        }
    }

    public bool IsVertexCoordinate(FlatTopCoordinate coordinate)
    {
        return !IsHexCoordinate(coordinate);
    }

    public IEnumerable<FlatTopCoordinate> GetAdjacentHexCoordinates(FlatTopCoordinate coordinate)
    {
        if (IsHexCoordinate(coordinate))
        {
            yield return coordinate.Add0Deg().Add60Deg();
            yield return coordinate.Add60Deg().Add120Deg();
            yield return coordinate.Add120Deg().Add180Deg();
            yield return coordinate.Add180Deg().Add240Deg();
            yield return coordinate.Add240Deg().Add300Deg();
            yield return coordinate.Add300Deg().Add0Deg();
        }
        else
        {
            foreach (FlatTopCoordinate adjacentCoordinate in coordinate.GetAdjacentCoordinates())
            {
                if (IsHexCoordinate(adjacentCoordinate))
                {
                    yield return adjacentCoordinate;
                }
            }
        }
    }	

    public IEnumerable<FlatTopCoordinate> GetAdjacentHexCoordinates(Edge edge)
    {
        IEnumerable<FlatTopCoordinate> set1 = GetAdjacentHexCoordinates(edge.Coordinate2);
        IEnumerable<FlatTopCoordinate> set2 = GetAdjacentHexCoordinates(edge.Coordinate1);

        return set1.Intersect(set2);
    }
}
