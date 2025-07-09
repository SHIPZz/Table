namespace Amulet.ItemSystem;

[Serializable]
public class ItemsConfig
{
    public List<ItemEntry> Items { get; init; } = new();
}