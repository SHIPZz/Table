namespace Amulet.InventorySystem.Printer;

public interface IInventoryPrinter
{
    void Print(IInventory inventory, string owner);
}