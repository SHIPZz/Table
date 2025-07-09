using Amulet.InventorySystem;
using Amulet.ItemSystem;

namespace Amulet.Commands;

public class AddItemToInventoryCommand : ICommand<ItemEntry>
{
    private readonly IInventory _heroInventory;
    
    public AddItemToInventoryCommand(IInventory heroInventory)
    {
        _heroInventory = heroInventory;
    }
    
    public void Execute(ItemEntry args)
    {
        _heroInventory.Add(new Item(args.Type, args.Amount));
    }
}