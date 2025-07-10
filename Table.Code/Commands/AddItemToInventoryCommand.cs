using Amulet.InventorySystem;
using Amulet.ItemSystem;
using Amulet.Logger;

namespace Amulet.Commands;

[Command(CommandNames.AddToHero, "Добавить предмет герою")]
public class AddItemToInventoryCommand : ICommand
{
    private readonly IInventory _heroInventory;
    private readonly List<ItemType> _itemTypes;
    private readonly ILogger _logger;
    
    public AddItemToInventoryCommand(IInventory heroInventory, List<ItemType> itemTypes, ILogger logger)
    {
        _heroInventory = heroInventory;
        _itemTypes = itemTypes;
        _logger = logger;
    }
    
    public void Execute(string? args = null)
    {
        if (CommandParser.TryParseItemEntry(args, _itemTypes, _logger, out ItemEntry entry))
        {
            _heroInventory.Add(new Item(entry.Type, entry.Amount));
            _logger.LogInfo($"Добавлено {entry.Amount} {entry.Type} в инвентарь героя.");
        }
    }
}