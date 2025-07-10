namespace Amulet.Logger;

public class ConsoleLogger : ILogger
{
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        ShowMessage(message);
    }

    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        ShowMessage(message);
    }

    public void LogDebug(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        ShowMessage(message);
    }

    private static void ShowMessage(string message)
    {
        Console.WriteLine($"[INFO] {message}");
        Console.ResetColor();
    }
} 