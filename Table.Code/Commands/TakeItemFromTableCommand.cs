using Amulet.InventorySystem;
using Amulet.ItemSystem;
using Amulet.Logger;

namespace Amulet.Commands;

public class TakeItemFromTableCommand : ICommand<ItemEntry>
{
    private readonly IInventory _heroInventory;
    private readonly IInventory _tableInventory;
    private readonly ILogger _logger;

    public TakeItemFromTableCommand(IInventory heroInventory, IInventory tableInventory, ILogger logger)
    {
        _logger = logger;
        _heroInventory = heroInventory;
        _tableInventory = tableInventory;
    }
    
    public void Execute(ItemEntry args)
    {
        if (_tableInventory.TryRemoveItem(args.Type, args.Amount))
        {
            Item item = new Item(args.Type, args.Amount);
            _heroInventory.Add(item);
            _logger.LogDebug($"Герой взял: {item}");
        }
        else
        {
            _logger.LogError("На столе нет такого количества предметов.");
        }
    }
}