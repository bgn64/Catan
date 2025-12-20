using Catan.Core;

namespace Catan.CLI;

public interface ICommandConfigurationService
{
    void ConfigureCommand(Command command, Game game);
}
