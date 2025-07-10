namespace Amulet.Commands;

public interface ICommand
{
    void Execute(string? args = null);
}