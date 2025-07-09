using Amulet.Common;

namespace Amulet.ItemSystem;

public class Item : IPrototype<Item>
{
    public ItemType Type { get; }
    public int Amount { get; private set; }

    public Item(ItemType type, int amount)
    {
        Type = type;
        Amount = amount;
    }

    public void Add(int amount)
    {
        Amount = Math.Clamp(Amount + amount, 0, Constants.ItemAmountCapacity);
    }
    
    public void Remove(int amount)
    {
        Amount = Math.Clamp(Amount - amount, 0, Constants.ItemAmountCapacity);
    }

    public bool Empty() => Amount <= 0;

    public Item Clone() => new(Type, Amount);

    public override string ToString() => $"{Type} - {Amount} шт";
}