namespace Catan.Core;

public class Board
{
	internal IEnumerable<Vertex> GetAdjacentVertices(Vertex vertex)
	{
		return new List<Vertex>();
	}

	public bool HasSettlement(Vertex vertex)
	{
		return false;
	}
	
	public bool HasSettlement(Vertex vertex, Player player)
	{
		return false;
	}
	
	internal bool CanPlaceSettlement(Vertex vertex, Player player)
	{
		return false;
	}

	internal bool PlaceSettlement(Vertex vertex, Player player)
	{
		return false;
	}
	
	internal bool CanPlaceRoad(Edge edge, Player player)
	{
		return false;
	}

	internal bool PlaceRoad(Edge edge, Player player)
	{
		return false;
	}
}
