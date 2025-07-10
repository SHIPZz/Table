using Amulet.Logger;

namespace Amulet.Commands;

[Command(CommandNames.Empty, "Пустая команда")]
public class EmptyCommand : ICommand
{
    private readonly ILogger _logger;
    
    public EmptyCommand(ILogger logger)
    {
        _logger = logger;
    }

    public void Execute(string? args = null)
    {
        _logger.LogInfo("Эта команда не выполняет никаких действий.");
    }
}