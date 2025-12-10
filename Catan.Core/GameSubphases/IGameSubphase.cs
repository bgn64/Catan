namespace Catan.Core;

public interface IGameSubphase
{
    void Accept(IGameSubphaseVisitor visitor);
}
