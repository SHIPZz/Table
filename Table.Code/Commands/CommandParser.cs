using Amulet.ItemSystem;
using Amulet.Logger;

namespace Amulet.Commands;

public static class CommandParser
{
    public static bool TryParseItemEntry(string? args, List<ItemType> itemTypes, ILogger logger, out ItemEntry entry)
    {
        entry = new ItemEntry();

        if (string.IsNullOrWhiteSpace(args))
        {
            logger.LogError("Не указаны параметры команды.");
            return false;
        }

        var parts = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length != 2)
        {
            logger.LogError("Неверный формат параметров. Ожидается: <тип_предмета> <количество>");
            return false;
        }

        if (!int.TryParse(parts[0], out int itemIndex) || itemIndex < 1 || itemIndex > itemTypes.Count)
        {
            logger.LogError("Неверный тип предмета.");
            return false;
        }

        if (!int.TryParse(parts[1], out int amount) || amount <= 0)
        {
            logger.LogError("Неверное количество.");
            return false;
        }

        entry.Type = itemTypes[itemIndex - 1];
        entry.Amount = amount;

        return true;
    }
} 