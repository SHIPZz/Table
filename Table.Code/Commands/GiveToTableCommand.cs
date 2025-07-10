using Amulet.InventorySystem;
using Amulet.ItemSystem;
using System;
using Amulet.Commands;
using Amulet.Configs;
using Amulet.Logger;
using System.Collections.Generic;

namespace Amulet.Commands;

[Command(CommandNames.GiveToTable, "Положить предмет на стол")]
public class GiveToTableCommand : ICommand
{
    private readonly IInventory _heroInventory;
    private readonly IInventory _tableInventory;
    private readonly List<ItemType> _itemTypes;
    private readonly ILogger _logger;

    public GiveToTableCommand(IInventory heroInventory, IInventory tableInventory, List<ItemType> itemTypes, ILogger logger)
    {
        _logger = logger;
        _heroInventory = heroInventory;
        _tableInventory = tableInventory;
        _itemTypes = itemTypes;
    }
    
    public void Execute(string? args = null)
    {
        if (CommandParser.TryParseItemEntry(args, _itemTypes, _logger, out ItemEntry entry))
        {
            if (_heroInventory.TryRemoveItem(entry.Type, entry.Amount))
            {
                _tableInventory.Add(new Item(entry.Type, entry.Amount));
                _logger.LogInfo($"Герой положил на стол: {entry.Type} - {entry.Amount} шт.");
                return;
            }
            
            _logger.LogError("У героя нет такого количества предметов.");
        }
    }
}