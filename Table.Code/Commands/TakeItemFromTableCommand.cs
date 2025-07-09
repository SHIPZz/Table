using Amulet.Configs;
using Amulet.InventorySystem;
using Amulet.ItemSystem;

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
        if (_tableInventory.TryRemoveItem(args.Type, args.Amount, out Item? taken))
        {
            _heroInventory.Add(taken!);
            _logger.LogDebug($"Герой взял: {taken}");
        }
        else
        {
            _logger.LogError("На столе нет такого количества предметов.");
        }
    }
}