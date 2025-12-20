using Catan.Core;

namespace Catan.CLI;

public interface ICommandProvider
{
    Command? GetCommand(Game game);
}
