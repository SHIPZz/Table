using Amulet.ItemSystem;
using Amulet.Commands;
using Amulet.Configs;

namespace Amulet.Input;

public class InputController : IInputController
{
    private readonly List<ItemType>? _itemTypes;
    private readonly IInputService _inputService;
    private readonly CommandDispatcher _dispatcher;
    private readonly ILogger _logger;
    
    public InputController(List<ItemType> itemTypes, IInputService inputService, CommandDispatcher dispatcher, ILogger logger)
    {
        _itemTypes = itemTypes;
        _inputService = inputService;
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public void ShowItemTypeByNumber()
    {
        _logger.LogInfo("Выберите тип предмета:");
        
        for (int i = 0; i < _itemTypes.Count; i++)
        {
            _logger.LogInfo($"{i + 1}. {_itemTypes[i]}");
        }
    }

    public void ShowCommandMenuAndExecute()
    {
        while (true)
        {
            _dispatcher.PrintMenu();
            Console.Write("> ");

            if (!int.TryParse(_inputService.ReadLine(), out int commandIndex))
            {
                _logger.LogError("Неверная команда.");
                continue;
            }

            var commandName = _dispatcher.GetCommandKeyByIndex(commandIndex - 1);

            if (commandName == CommandNames.Empty)
            {
                _logger.LogError("Неверная команда.");
                continue;
            }

            if (commandName == CommandNames.Exit)
                break;

            if (TryExecuteCommandsWithParameters(commandName)) 
                continue;

            _dispatcher.Execute(commandName);
        }
    }

    private bool TryExecuteCommandsWithParameters(string? commandName)
    {
        switch (commandName)
        {
            case CommandNames.AddToHero or CommandNames.GiveToTable or CommandNames.TakeFromTable:
            {
                ShowItemTypeByNumber();
                
                if (TryReadItemSelectInput(out ItemEntry entry))
                {
                    _dispatcher.Execute(commandName, entry);
                }
                
                return true;
            }
            
            default:
                return false;
        }
    }

    public bool TryReadItemSelectInput(out ItemEntry entry)
    {
        entry = new ItemEntry();
        
        if (!int.TryParse(_inputService.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > _itemTypes.Count)
        {
            _logger.LogError("Неверный тип предмета.");
            return false;
        }
        
        entry.Type = _itemTypes[itemIndex - 1];
        Console.WriteLine("Введите количество:");
        Console.Write("> ");
        
        if (!int.TryParse(_inputService.ReadLine(), out int amount) || amount <= 0)
        {
            _logger.LogError("Неверное количество.");
            return false;
        }
        entry.Amount = amount;
        
        return true;
    }
} 