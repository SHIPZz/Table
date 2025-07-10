namespace Amulet.Commands;

public interface ICommandDispatcher
{
    void Execute(string key, string? args = null);
    string GetCommandKeyByIndex(int commandIndex);
    void PrintMenu();
}