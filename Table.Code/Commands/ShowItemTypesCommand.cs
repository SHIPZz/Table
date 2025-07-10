using Amulet.ItemSystem;
using Amulet.Logger;

namespace Amulet.Commands;

[Command(CommandNames.ShowItemTypes, "Показать типы предметов", 20)]
public class ShowItemTypesCommand : ICommand
{
    private readonly ILogger _logger;
    private readonly List<ItemType> _itemTypes;

    public ShowItemTypesCommand(ILogger logger, List<ItemType> itemTypes)
    {
        _itemTypes = itemTypes;
        _logger = logger;
    }

    public void Execute(string? args = null)
    {
        _logger.LogInfo("Выберите тип предмета:");
        
        for (int i = 0; i < _itemTypes.Count; i++)
        {
            _logger.LogInfo($"{i + 1}. {_itemTypes[i]}");
        }
    }
}