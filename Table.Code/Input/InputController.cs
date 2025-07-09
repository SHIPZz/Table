using Amulet.ItemSystem;
using Amulet.Commands;
using Amulet.Logger;

namespace Amulet.Input;

public class InputController : IInputController, IDisposable
{
    private readonly List<ItemType>? _itemTypes;
    private readonly IInputService _inputService;
    private readonly ICommandDispatcher _dispatcher;
    private readonly ILogger _logger;

    public InputController(List<ItemType> itemTypes, IInputService inputService, ICommandDispatcher dispatcher,
        ILogger logger)
    {
        _itemTypes = itemTypes;
        _inputService = inputService;
        _dispatcher = dispatcher;
        _logger = logger;
        _inputService.InputReceived += OnInputReceived;
    }

    public void Dispose()
    {
        _inputService.InputReceived -= OnInputReceived;
        _inputService.Dispose();
    }

    public void ShowCommandMenuAndExecute()
    {
        _dispatcher.PrintMenu();
        _inputService.StartInputLoop();
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

        if (TryExecuteCommandsWithParameters(commandName))
            return;

        _dispatcher.Execute(commandName);
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

    private bool TryExecuteCommandsWithParameters(string? commandName)
    {
        switch (commandName)
        {
            case CommandNames.AddToHero or CommandNames.GiveToTable or CommandNames.TakeFromTable:
            {
                _dispatcher.Execute(CommandNames.ShowItemTypes);

                if (TryReadItemSelectInput(out ItemEntry entry))
                {
                    _dispatcher.Execute(commandName, entry);
                    _dispatcher.PrintMenu();
                }

                return true;
            }

            default:
                return false;
        }
    }

    private bool TryReadItemSelectInput(out ItemEntry entry)
    {
        entry = new ItemEntry();

        if (!int.TryParse(_inputService.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > _itemTypes.Count)
        {
            _logger.LogError("Неверный тип предмета.");
            return false;
        }

        entry.Type = _itemTypes[itemIndex - 1];

        _logger.LogInfo("Введите количество:");
        _logger.LogInfo("> ");

        if (!int.TryParse(_inputService.ReadLine(), out int amount) || amount <= 0)
        {
            _logger.LogError("Неверное количество.");
            return false;
        }

        entry.Amount = amount;

        return true;
    }
}