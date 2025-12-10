namespace Catan.Core;

public abstract class GameSubphase : Phase, IGameSubphase
{
	public abstract void Accept(IGameSubphaseVisitor visitor);
}
