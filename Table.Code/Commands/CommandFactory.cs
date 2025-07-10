using Amulet.InventorySystem;
using Amulet.InventorySystem.Printer;
using Amulet.ItemSystem;
using Amulet.Input;
using Amulet.Logger;

namespace Amulet.Commands;

public class CommandFactory : ICommandFactory
{
    private readonly IInventory _heroInventory;
    private readonly IInventory _tableInventory;
    private readonly List<ItemType> _itemTypes;
    private readonly ILogger _logger;
    private readonly IInventoryPrinter _printer;
    private readonly IInputService _inputService;

    public CommandFactory(
        IInventory heroInventory,
        IInventory tableInventory,
        List<ItemType> itemTypes,
        ILogger logger,
        IInventoryPrinter printer,
        IInputService inputService)
    {
        _heroInventory = heroInventory;
        _tableInventory = tableInventory;
        _itemTypes = itemTypes;
        _logger = logger;
        _printer = printer;
        _inputService = inputService;
    }

    public ICommand CreateCommand(Type commandType)
    {
        return commandType.Name switch
        {
            nameof(ShowItemTypesCommand) => new ShowItemTypesCommand(_logger, _itemTypes),
            nameof(AddItemToInventoryCommand) => new AddItemToInventoryCommand(_heroInventory, _itemTypes, _logger),
            nameof(GiveToTableCommand) => new GiveToTableCommand(_heroInventory, _tableInventory, _itemTypes, _logger),
            nameof(TakeItemFromTableCommand) => new TakeItemFromTableCommand(_heroInventory, _tableInventory, _itemTypes, _logger),
            nameof(ShowInventoriesCommand) => new ShowInventoriesCommand(_heroInventory, _tableInventory, _printer),
            nameof(ExitCommand) => new ExitCommand(_logger, _inputService),
            nameof(EmptyCommand) => new EmptyCommand(_logger),
            _ => throw new ArgumentException($"Неизвестный тип команды: {commandType.Name}")
        };
    }
} 