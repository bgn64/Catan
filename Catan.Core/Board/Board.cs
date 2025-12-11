namespace Catan.Core;

public class Board
{
	Dictionary<Vertex, Settlement> _settlements;
	Dictionary<Edge, Road> _roads;

	internal Board()
	{
		_settlements = new Dictionary<Vertex, Settlement>();
		_roads = new Dictionary<Edge, Road>();
	}
		
	internal IEnumerable<Vertex> GetAdjacentVertices(Vertex vertex)
	{
		return new List<Vertex>();
	}

	public bool HasSettlement(Vertex vertex)
	{
		return _settlements.ContainsKey(vertex);
	}
	
	public bool HasSettlement(Vertex vertex, Player player)
	{
		return _settlements.TryGetValue(vertex, out Settlement? settlement) && settlement.Player == player;
	}
	
	internal bool CanPlaceSettlement(Vertex vertex)
	{
		return !_settlements.ContainsKey(vertex);
	}

	internal bool TryPlaceSettlement(Vertex vertex, Settlement settlement)
	{
		return _settlements.TryAdd(vertex, settlement);
	}
	
	internal bool CanPlaceRoad(Edge edge)
	{
		return !_roads.ContainsKey(edge);
	}

	internal bool TryPlaceRoad(Edge edge, Road road)
	{
		return _roads.TryAdd(edge, road);
	}
}
