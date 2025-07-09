namespace Amulet.Input;

public class ConsoleInputService : IInputService
{
    public event Action<string?>? InputReceived;
    
    private bool _isRunning;
    
    public string? ReadLine()
    {
        var input = Console.ReadLine();
        return input;
    }
    
    public void StartInputLoop()
    {
        _isRunning = true;
        
        while (_isRunning)
        {
            var input = Console.ReadLine();
            InputReceived?.Invoke(input);
        }
    }
    
    public void Dispose()
    {
        _isRunning = false;
        InputReceived = null;
    }
}