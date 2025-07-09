namespace Amulet.Input;

public interface IInputService : IDisposable
{
    string? ReadLine();
    event Action<string?>? InputReceived;
    void StartInputLoop();
}