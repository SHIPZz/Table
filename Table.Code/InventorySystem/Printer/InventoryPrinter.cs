using Amulet.Configs;
using Amulet.ItemSystem;

namespace Amulet.InventorySystem.Printer;

public class InventoryPrinter : IInventoryPrinter
{
    private readonly ILogger _logger;
    
    public InventoryPrinter(ILogger logger)
    {
        _logger = logger;
    }

    public void Print(IInventory inventory, string owner)
    {
        _logger.LogInfo($"--- {owner} Inventory ---");
        IEnumerable<Item> items = inventory.Items;
        
        if (items.Count() <= 0)
        {
            _logger.LogInfo("Пусто");
            return;
        }
        
        foreach (var item in items)
        {
            _logger.LogInfo($"{item.Type}: {item.Amount}");
        }
    }
}