using Amulet.Commands;
using Amulet.Configs;
using Amulet.Input;
using Amulet.InventorySystem;
using Amulet.InventorySystem.Printer;
using Amulet.ItemSystem;
using Amulet.Logger;

namespace Amulet;

public class Program
{
    private readonly IConfigService _configService;
    private readonly IInventory _heroInventory;
    private readonly IInventory _tableInventory;
    private readonly IInventoryPrinter _printer;
    private readonly IInputController _inputController;
    private readonly ILogger _logger;
    private readonly IInputService _inputService;
    private readonly ICommandDispatcher _dispatcher;

    public Program()
    {
        _logger = new ConsoleLogger();
        _configService = new ConfigService(_logger);
        _heroInventory = new Inventory();
        _tableInventory = new Inventory();
        _printer = new InventoryPrinter(_logger);
        _inputService = new ConsoleInputService();
        
        _configService.LoadTableConfig();
        
        var commandFactory = new CommandFactory(
            _heroInventory, 
            _tableInventory, 
            _configService.AllItemTypes.ToList(), 
            _logger, 
            _printer, 
            _inputService);
        
        _dispatcher = new CommandDispatcher(_logger, commandFactory);
        
        _inputController = new InputController(_inputService, _dispatcher, _logger);
    }

    public static void Main()
    {
        new Program().Run();
    }

    public void Run()
    {
        foreach (var entry in _configService.TableConfig.Items)
        {
            if (entry.Amount > 0)
            {
                _tableInventory.Add(new Item(entry.Type, entry.Amount));
            }
        }

        _inputController.Initialize();
        _dispatcher.PrintMenu();
        _inputService.StartInputLoop();
    }
}