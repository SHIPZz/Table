namespace Amulet.Logger;

public interface ILogger
{
    void LogError(string message);
    void LogInfo(string message);
    void LogDebug(string message);
} 