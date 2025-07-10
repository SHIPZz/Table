using Amulet.InventorySystem;
using Amulet.ItemSystem;
using Amulet.Logger;

namespace Amulet.Commands;

[Command(CommandNames.TakeFromTable, "Взять предмет со стола")]
public class TakeItemFromTableCommand : ICommand
{
    private readonly IInventory _heroInventory;
    private readonly IInventory _tableInventory;
    private readonly List<ItemType> _itemTypes;
    private readonly ILogger _logger;

    public TakeItemFromTableCommand(IInventory heroInventory, IInventory tableInventory, List<ItemType> itemTypes, ILogger logger)
    {
        _logger = logger;
        _heroInventory = heroInventory;
        _tableInventory = tableInventory;
        _itemTypes = itemTypes;
    }
    
    public void Execute(string? args = null)
    {
        if (CommandParser.TryParseItemEntry(args, _itemTypes, _logger, out ItemEntry entry))
        {
            if (_tableInventory.TryRemoveItem(entry.Type, entry.Amount))
            {
                Item item = new Item(entry.Type, entry.Amount);
                _heroInventory.Add(item);
                _logger.LogInfo($"Герой взял: {entry.Type} - {entry.Amount} шт.");
            }
            else
            {
                _logger.LogError("На столе нет такого количества предметов.");
            }
        }
    }
}