namespace Catan.Core;

public interface IGameSubphaseVisitor
{
	void Visit(InitialRollPhase phase);
	void Visit(InitialPlacementPhase phase);
	void Visit(MainGamePhase phase);
}
