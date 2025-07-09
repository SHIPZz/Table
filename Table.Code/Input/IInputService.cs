namespace Amulet.Input;

public interface IInputService : IDisposable
{
    event Action<string?>? InputReceived;
    string? ReadLine();
    void StartInputLoop();
}