namespace Amulet.Input;

public class ConsoleInputService : IInputService
{
    public string? ReadLine() => Console.ReadLine();
    public int ReadInt() => int.Parse(Console.ReadLine() ?? string.Empty);
}