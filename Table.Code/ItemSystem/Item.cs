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

    public void AddAmount(int delta)
    {
        Amount = Math.Clamp(Amount + delta, 0, int.MaxValue);
    }

    public bool Empty() => Amount <= 0;

    public Item Clone() => new(Type, Amount);

    public override string ToString() => $"{Type} - {Amount} шт";
}