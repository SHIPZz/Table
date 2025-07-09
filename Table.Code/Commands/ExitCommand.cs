using Amulet.Configs;

namespace Amulet.Commands;

public class ExitCommand : ICommand
{
    private readonly ILogger _logger;
    
    public ExitCommand(ILogger logger)
    {
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInfo("Выход...");
        Environment.Exit(0);
    }
}