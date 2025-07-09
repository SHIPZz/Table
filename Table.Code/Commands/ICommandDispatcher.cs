namespace Amulet.Commands;

public interface ICommandDispatcher
{
    void Register(string key, ICommand command);
    void Register<TArgs>(string key, ICommand<TArgs> command);
    void Execute(string key);
    void Execute<TArgs>(string key, TArgs args);
    string GetCommandKeyByIndex(int commandIndex);
    void PrintMenu();
}