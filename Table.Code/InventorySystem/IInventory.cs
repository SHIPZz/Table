using Amulet.ItemSystem;

namespace Amulet.InventorySystem;

public interface IInventory
{
    IReadOnlyCollection<Item> Items { get; }
    void Add(Item item);
    bool TryRemoveItem(ItemType type, int amount);
}