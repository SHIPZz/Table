namespace Amulet.Commands;

public interface ICommandFactory
{
    ICommand CreateCommand(Type commandType);
} 