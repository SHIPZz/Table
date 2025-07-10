using Amulet.InventorySystem;
using Amulet.InventorySystem.Printer;

namespace Amulet.Commands;

[Command(CommandNames.ShowInventories, "Показать инвентари", 10)]
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

    public void Execute(string? args = null)
    {
        _printer.Print(_heroInventory, "Hero");
        _printer.Print(_tableInventory, "Table");
    }
}