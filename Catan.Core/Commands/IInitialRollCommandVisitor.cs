namespace Catan.Core;

public interface IInitialRollCommandVisitor
{
	void Visit(RollCommand command);
}
