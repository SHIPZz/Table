using Amulet.ItemSystem;

namespace Amulet.Configs;

public interface IConfigService
{
    ItemsConfig TableConfig { get; }
    IReadOnlyList<ItemType> AllItemTypes { get; }
    void LoadTableConfig();
}