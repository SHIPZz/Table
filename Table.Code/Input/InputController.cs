using Amulet.Commands;
using Amulet.Logger;

namespace Amulet.Input;

public class InputController : IInputController, IDisposable
{
    private readonly IInputService _inputService;
    private readonly ICommandDispatcher _dispatcher;
    private readonly ILogger _logger;

    public InputController(IInputService inputService, ICommandDispatcher dispatcher, ILogger logger)
    {
        _inputService = inputService;
        _dispatcher = dispatcher;
        _logger = logger;
        _inputService.InputReceived += OnInputReceived;
    }

    public void Dispose()
    {
        _inputService.InputReceived -= OnInputReceived;
    }

    private void OnInputReceived(string? input)
    {
        if (TryParseCommandIndexInput(input, out int commandIndex))
            return;

        var commandName = _dispatcher.GetCommandKeyByIndex(commandIndex - 1);

        if (commandName == CommandNames.Empty)
        {
            _logger.LogError("Неверная команда.");
            _dispatcher.PrintMenu();
            _logger.LogInfo("> ");
            return;
        }

        if (TryExitOnExitCommand(commandName))
            return;

        string? args = null;
        
        if (NeedsParameters(commandName)) 
            args = ReadCommandParameters();

        _dispatcher.Execute(commandName, args);
        _dispatcher.PrintMenu();
        _logger.LogInfo("> ");
    }

    private bool TryParseCommandIndexInput(string? input, out int commandIndex)
    {
        if (!int.TryParse(input, out commandIndex))
        {
            _logger.LogError("Неверная команда.");
            _dispatcher.PrintMenu();
            _logger.LogInfo("> ");
            return true;
        }

        return false;
    }

    private bool TryExitOnExitCommand(string commandName)
    {
        if (commandName == CommandNames.Exit)
        {
            Dispose();
            return true;
        }

        return false;
    }

    private bool NeedsParameters(string commandName)
    {
        return commandName is CommandNames.AddToHero or CommandNames.GiveToTable or CommandNames.TakeFromTable;
    }

    private string? ReadCommandParameters()
    {
        _dispatcher.Execute(CommandNames.ShowItemTypes);
        
        _logger.LogInfo("Введите тип предмета:");
        _logger.LogInfo("> ");
        
        if (!int.TryParse(_inputService.ReadLine(), out int itemIndex) || itemIndex < 1)
        {
            _logger.LogError("Неверный тип предмета.");
            return null;
        }

        _logger.LogInfo("Введите количество:");
        _logger.LogInfo("> ");

        if (!int.TryParse(_inputService.ReadLine(), out int amount) || amount <= 0)
        {
            _logger.LogError("Неверное количество.");
            return null;
        }

        return $"{itemIndex} {amount}";
    }
}