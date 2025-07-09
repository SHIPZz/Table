using Amulet.Configs;

namespace Amulet.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<string, ICommand> _commands = new();
    private readonly Dictionary<string, object> _paramCommands = new();
    private readonly List<(string Key, string Description)> _menu = new();
    private readonly ILogger _logger;

    public CommandDispatcher(Dictionary<string, string> descriptions, ILogger logger)
    {
        _logger = logger;

        foreach (var pair in descriptions)
        {
            if (pair.Key == CommandNames.Empty)
                continue;
            
            _menu.Add((pair.Key, pair.Value));
        }
    }

    public void Register(string key, ICommand command)
    {
        _commands[key] = command;
    }

    public void Register<TArgs>(string key, ICommand<TArgs> command)
    {
        _paramCommands[key] = command;
    }

    public void Execute(string key)
    {
        if (_commands.TryGetValue(key, out var cmd))
            cmd.Execute();
        else
            _logger.LogError("Неизвестная команда.");
    }

    public void Execute<TArgs>(string key, TArgs args)
    {
        if (_paramCommands.TryGetValue(key, out var obj) && obj is ICommand<TArgs> cmd)
            cmd.Execute(args);
        else
            _logger.LogError("Неизвестная команда.");
    }

    public string GetCommandKeyByIndex(int commandIndex)
    {
        if (commandIndex >= 0 && commandIndex < _menu.Count)
            return _menu[commandIndex].Key;
        
        return CommandNames.Empty;
    }

    public void PrintMenu()
    {
        _logger.LogInfo("\nВыберите команду:");
        for (int i = 0; i < _menu.Count; i++)
        {
            _logger.LogInfo($"{i + 1}. {_menu[i].Description}");
        }
    }
}
