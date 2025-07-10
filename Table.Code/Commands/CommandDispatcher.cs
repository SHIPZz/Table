using Amulet.Logger;
using System.Reflection;

namespace Amulet.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<string, ICommand> _commands = new();
    private readonly Dictionary<string, string> _descriptions = new();
    private readonly Dictionary<string, int> _priorities = new();
    private readonly ILogger _logger;
    private readonly ICommandFactory _commandFactory;

    public CommandDispatcher(ILogger logger, ICommandFactory commandFactory)
    {
        _logger = logger;
        _commandFactory = commandFactory;
        RegisterCommandsFromAssembly();
    }

    public void Register(string key, ICommand command, string description = "", int priority = 0)
    {
        _commands[key] = command;
        _priorities[key] = priority;

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
        if (commandIndex < 0)
            return CommandNames.Empty;
            
        try
        {
            return GetSortedCommands().ElementAt(commandIndex);
        }
        catch (ArgumentOutOfRangeException)
        {
            return CommandNames.Empty;
        }
    }

    public void PrintMenu()
    {
        _logger.LogInfo("\nВыберите команду:");
        
        var sortedCommands = GetSortedCommands();
        int i = 1;
        
        foreach (var commandName in sortedCommands)
        {
            if (commandName == CommandNames.Empty)
                continue;
            
            if (_descriptions.TryGetValue(commandName, out var description))
            {
                _logger.LogInfo($"{i} - {description}");
                i++;
            }
        }
    }

    private IEnumerable<string> GetSortedCommands()
    {
        return _descriptions.Keys
            .OrderBy(key => _priorities.GetValueOrDefault(key, 0));
    }

    private void RegisterCommandsFromAssembly()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var commandTypes = assembly.GetTypes()
            .Where(t => typeof(ICommand).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Where(t => t.GetCustomAttribute<CommandAttribute>() != null);

        foreach (var commandType in commandTypes)
        {
            var attribute = commandType.GetCustomAttribute<CommandAttribute>();
            if (attribute == null) continue;

            try
            {
                var command = _commandFactory.CreateCommand(commandType);
                Register(attribute.Name, command, attribute.Description, attribute.Priority);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при создании команды {commandType.Name}: {ex.Message}");
            }
        }
    }
}
