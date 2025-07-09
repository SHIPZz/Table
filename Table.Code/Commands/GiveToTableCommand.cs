using Amulet.InventorySystem;
using Amulet.ItemSystem;
using System;
using Amulet.Commands;
using Amulet.Configs;
using Amulet.Logger;

namespace Amulet.Commands;

public class GiveToTableCommand : ICommand<ItemEntry>
{
    private readonly IInventory _heroInventory;
    private readonly IInventory _tableInventory;
    private readonly ILogger _logger;

    public GiveToTableCommand(IInventory heroInventory, IInventory tableInventory, ILogger logger)
    {
        _logger = logger;
        _heroInventory = heroInventory;
        _tableInventory = tableInventory;
    }
    
    public void Execute(ItemEntry args)
    {
        if (_heroInventory.TryRemoveItem(args.Type, args.Amount))
        {
            _tableInventory.Add(new Item(args.Type, args.Amount));
            _logger.LogInfo($"Герой положил на стол: {args.Type} - {args.Amount} шт.");
            return;
        }
        
        _logger.LogError("У героя нет такого количества предметов.");
    }
}