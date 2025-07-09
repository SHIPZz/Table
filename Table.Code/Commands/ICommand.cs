namespace Amulet.Commands;

public interface ICommand
{
    void Execute();
}

public interface ICommand<TArgs>
{
    void Execute(TArgs args);
}