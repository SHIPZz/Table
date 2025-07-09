using System.Text.Json;
using Amulet.ItemSystem;
using Amulet.Logger;

namespace Amulet.Configs;

public class ConfigService : IConfigService
{
    private readonly string _tableConfigPath;
    private readonly ILogger _logger;
    
    private List<ItemType> _allItemTypes = new();
    
    public ItemsConfig TableConfig { get; private set; }
    public IReadOnlyList<ItemType> AllItemTypes => _allItemTypes;

    public ConfigService(ILogger logger, string tableConfigPath = "Configs/items_config.json")
    {
        _logger = logger;
        _tableConfigPath = tableConfigPath;
    }

    public void LoadTableConfig()
    {
        if (!File.Exists(_tableConfigPath))
        {
            _logger.LogError($"No table config file found at {_tableConfigPath}");
            TableConfig = new ItemsConfig();
            _allItemTypes = new List<ItemType>();
            return;
        }
        try
        {
            var json = File.ReadAllText(_tableConfigPath);
            TableConfig = JsonSerializer.Deserialize<ItemsConfig>(json) ?? new ItemsConfig();
            _allItemTypes = TableConfig.Items
                .Select(i => Enum.TryParse(i.Type.ToString(), true, out ItemType t) ? t : (ItemType?)null)
                .Where(t => t.HasValue)
                .Select(t => t.Value)
                .Distinct()
                .ToList();
            _logger.LogInfo("Table config loaded successfully.");
        }
        catch
        {
            _logger.LogError("Ошибка чтения или парсинга конфигурации предметов.");
            TableConfig = new ItemsConfig();
            _allItemTypes = new List<ItemType>();
        }
    }
}