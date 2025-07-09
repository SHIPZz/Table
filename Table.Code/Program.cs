using Amulet.Commands;
using Amulet.Configs;
using Amulet.Input;
using Amulet.InventorySystem;
using Amulet.InventorySystem.Printer;
using Amulet.ItemSystem;

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
        
        _dispatcher = new CommandDispatcher(new Dictionary<string, string>
        {
            { CommandNames.AddToHero, "Добавить предмет герою" },
            { CommandNames.TakeFromTable, "Взять предмет со стола" },
            { CommandNames.GiveToTable, "Положить предмет на стол" },
            { CommandNames.ShowInventories, "Показать инвентари" },
            { CommandNames.Exit, "Выход" },
            { CommandNames.Empty, "Пустая команда" }
        }, _logger);
        
        _configService.LoadTableConfig();
        
        _inputController = new InputController(_configService.AllItemTypes.ToList(), _inputService, _dispatcher, _logger);
    }

    public static void Main()
    {
        new Program().Run();
    }

    public void Run()
    {
        BindCommands();
        
        foreach (var entry in _configService.TableConfig.Items)
        {
            if (entry.Amount > 0)
            {
                _tableInventory.Add(new Item(entry.Type, entry.Amount));
            }
        }

        _inputController.ShowCommandMenuAndExecute();
    }

    private void BindCommands()
    {
        _dispatcher.Register(CommandNames.ShowItemTypes, new ShowItemTypesCommand(_logger,_configService.AllItemTypes.ToList()));
        _dispatcher.Register(CommandNames.AddToHero, new AddItemToInventoryCommand(_heroInventory));
        _dispatcher.Register(CommandNames.TakeFromTable, new TakeItemFromTableCommand(_heroInventory, _tableInventory, _logger));
        _dispatcher.Register(CommandNames.GiveToTable, new GiveToTableCommand(_heroInventory, _tableInventory, _logger));
        _dispatcher.Register(CommandNames.ShowInventories, new ShowInventoriesCommand(_heroInventory, _tableInventory, _printer));
        _dispatcher.Register(CommandNames.Exit, new ExitCommand(_logger));
        _dispatcher.Register(CommandNames.Empty, new EmptyCommand(_logger));
    }
}