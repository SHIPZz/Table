using Amulet.Common;
using Amulet.ItemSystem;

namespace Amulet.InventorySystem;

public class Inventory : IInventory
{
    private readonly Dictionary<ItemType, Item> _items = new(Constants.InventoryCapacity);

    public IReadOnlyCollection<Item> Items => _items.Values;

    public void Add(Item item)
    {
        if (_items.TryGetValue(item.Type, out var targetItem))
            targetItem.Add(item.Amount);
        else
            _items[item.Type] = item.Clone();
    }

    public bool TryRemoveItem(ItemType type, int amount)
    {
        if (!_items.TryGetValue(type, out var targetItem))
            return false;

        if (targetItem.Amount < amount)
            return false;
        
        targetItem.Remove(amount);

        if (targetItem.Empty())
            _items.Remove(type);

        return true;

    }
}