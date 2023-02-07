using MainApplication.PluginBase.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

// Normal Application boot
Console.WriteLine("This is a Plugin demonstrator");
Console.WriteLine("To create plugins:");
Console.WriteLine("> Create a new ClassLibrary project");
Console.WriteLine("> Inherit the IPlugin interface");
Console.WriteLine("> ... if it will do Math, implement IMath intrface");
Console.WriteLine("> Place the compiled DLL in the 'plugins' folder");
Console.WriteLine();
var config = new MainApplication.PluginBase.Models.InitParams()
{
    // all my stuff and references
    // like DI, Logs, DAOs, etc
};

// Read plugins Folders
Console.WriteLine("Reading plugins on: /plugins");
if (!Directory.Exists("plugins")) Directory.CreateDirectory("plugins");
var dlls = Directory.GetFiles("plugins", "*.dll");
var asm = dlls.Select(f => Assembly.LoadFrom(f));

var allTypes = asm.SelectMany(asm => asm.GetTypes())
                  .Where(t => t.IsClass && !t.IsInterface)
                  .Where(t => typeof(IPlugin).IsAssignableFrom(t));

var instances = allTypes.Select(t => (IPlugin)(Activator.CreateInstance(t) ?? throw new Exception($"Constructor not supported for {t.Name}")))
                        .Where(obj => obj is not null)
                        .ToArray(); // easier to debug
Console.WriteLine($"{instances.Length} Available plugins");

// Start all plugins
var activePlugins = instances.Where(obj => obj.Initialize(config))
                             .ToArray(); // Make the plugins list a non IEnumerable
Console.WriteLine($"{activePlugins.Length} Active (loaded) plugins");
Console.WriteLine("");

if(activePlugins.Length == 0)
{
    Console.WriteLine("No available plugins =(");
    return;
}

// Application calls plugins
while (true)
{
    Console.WriteLine("What to do?");
    Console.WriteLine("1. Execute Math");
    Console.WriteLine("ESC. Exit");

    var key = Console.ReadKey(true);
    if (key.Key == ConsoleKey.Escape) break;

    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1) executeMath();
    Console.WriteLine();
}

void executeMath()
{
    var mathCapable = activePlugins.Where(p => p.CanDoMath)
                                   .ToArray();

    if (mathCapable.Length == 0)
    {
        Console.WriteLine("There is not MathCapable plugins");
        return;
    }

    Console.Clear();
    Console.WriteLine("Lets do Math");
    Console.WriteLine("Who you want to use?");
    for (int i = 0; i < mathCapable.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {mathCapable[i].PluginName}: {mathCapable[i].PluginDescription}");
    }
    string line = Console.ReadLine() ?? "";

    if (!int.TryParse(line, out int sel))
    {
        Console.WriteLine("Invalid input");
        return;
    }

    sel--; // Adjust to array position
    if (sel < 0 || sel >= mathCapable.Length)
    {
        Console.WriteLine("Invalid option");
        return;
    }

    // Here we are trusting that, if it said it can do Math, it can
    // additional checks go here to validate that claims
    var math = (IMath)mathCapable[sel];
    math.ExecuteTaks();
}