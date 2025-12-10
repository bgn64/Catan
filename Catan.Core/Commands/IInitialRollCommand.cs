namespace Catan.Core;

public interface IInitialRollCommand : ICommand
{
	void Accept(IInitialRollCommandVisitor visitor);
}
