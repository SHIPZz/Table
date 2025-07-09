using Amulet.InventorySystem;
using Amulet.InventorySystem.Printer;

namespace Amulet.Commands;

public class ShowInventoriesCommand : ICommand
{
    private readonly IInventory _heroInventory;
    private readonly IInventory _tableInventory;
    private readonly IInventoryPrinter _printer;

    public ShowInventoriesCommand(IInventory heroInventory, IInventory tableInventory, IInventoryPrinter printer)
    {
        _heroInventory = heroInventory;
        _tableInventory = tableInventory;
        _printer = printer;
    }

    public void Execute()
    {
        _printer.Print(_heroInventory, "Hero");
        _printer.Print(_tableInventory, "Table");
    }
}