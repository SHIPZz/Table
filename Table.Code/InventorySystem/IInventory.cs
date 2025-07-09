using Amulet.ItemSystem;

namespace Amulet.InventorySystem;

public interface IInventory
{
    void Add(Item item);
    bool TryRemoveItem(ItemType type, int amount, out Item? removedItem);
    IReadOnlyCollection<Item> Items { get; }
}