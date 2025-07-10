using Amulet.Input;
using Amulet.Logger;

namespace Amulet.Commands;

[Command(CommandNames.Exit, "Выход")]
public class ExitCommand : ICommand
{
    private readonly ILogger _logger;
    private readonly IInputService _inputService;

    public ExitCommand(ILogger logger, IInputService inputService)
    {
        _inputService = inputService;
        _logger = logger;
    }

    public void Execute(string? args = null)
    {
        _logger.LogInfo("Выход...");
        
        _inputService.Dispose();
        
        Environment.Exit(0);
    }
}