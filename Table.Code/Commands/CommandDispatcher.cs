using Amulet.Logger;
using System.Reflection;

namespace Amulet.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<string, ICommand> _commands = new();
    private readonly Dictionary<string, string> _descriptions = new();
    private readonly ILogger _logger;
    private readonly ICommandFactory _commandFactory;

    public CommandDispatcher(ILogger logger, ICommandFactory commandFactory)
    {
        _logger = logger;
        _commandFactory = commandFactory;
        RegisterCommandsFromAssembly();
    }

    public void Register(string key, ICommand command, string description = "")
    {
        _commands[key] = command;

        if (!string.IsNullOrEmpty(description))
            _descriptions[key] = description;
    }

    public void Execute(string key, string? args = null)
    {
        if (_commands.TryGetValue(key, out var command))
        {
            try
            {
                command.Execute(args);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка выполнения команды {key}: {ex.Message}");
            }
        }
        else
        {
            _logger.LogError("Неизвестная команда.");
        }
    }

    public string GetCommandKeyByIndex(int commandIndex)
    {
        var commands = _descriptions.Keys;
        
        if (commandIndex >= 0 && commandIndex < commands.Count)
            return commands.ElementAt(commandIndex);
        
        return CommandNames.Empty;
    }

    public void PrintMenu()
    {
        _logger.LogInfo("\nВыберите команду:");
        
        Dictionary<string, string> commands = _descriptions;
        
        for (int i = 0; i < commands.Values.Count; i++) 
            _logger.LogInfo($"{i + 1}. {commands.ElementAt(i)}");
    }

    private void RegisterCommandsFromAssembly()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var commandTypes = assembly.GetTypes()
            .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Where(t => t.GetCustomAttribute<CommandAttribute>() != null);

        foreach (var commandType in commandTypes)
        {
            var attribute = commandType.GetCustomAttribute<CommandAttribute>();
            if (attribute == null) continue;

            try
            {
                var command = _commandFactory.CreateCommand(commandType);
                Register(attribute.Name, command, attribute.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при создании команды {commandType.Name}: {ex.Message}");
            }
        }
    }
}
