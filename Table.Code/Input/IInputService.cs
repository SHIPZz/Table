using Amulet.ItemSystem;

namespace Amulet.Input;

public interface IInputService
{
    string? ReadLine();
    int ReadInt();
}