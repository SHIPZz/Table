using Amulet.Logger;

namespace Amulet.Commands;

public class EmptyCommand : ICommand
{
    private readonly ILogger _logger;
    
    public EmptyCommand(ILogger logger)
    {
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInfo("Эта команда не выполняет никаких действий.");
    }
}