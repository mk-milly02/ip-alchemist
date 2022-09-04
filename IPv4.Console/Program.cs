// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Text;
using IPv4.Console;
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "IPv4 Calculator";

        Console.OutputEncoding = Encoding.UTF8;

        AnsiConsole.Write(new FigletText("IPv4 Calculator").Color(Color.Orange3));

        var version = Assembly.GetExecutingAssembly().GetName().Version;

        AnsiConsole.MarkupLine("[italic]version {0}[/]".PadLeft(110), version);

        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
            .Title("[lime]?[/] [bold]Select from the [blue]menu[/]:[/]")
            .AddChoices("Network Overview")
            .AddChoiceGroup("IP Subnetting", new[] { "Uniform hosts", "Varied hosts" })
            .AddChoices("Exit"));

        switch (choice)
        {
            case "Network Overview":
                NetworkOverview.Run();
                break;

            case "Varied hosts":
                IPSubnetting.VariedHosts();
                break;

            case "Uniform hosts":
                IPSubnetting.UniformHosts();
                break;

            case "Exit":
                Environment.Exit(0);
                break;

            default:
                return;
        }

        AnsiConsole.Markup("\n[chartreuse1]Press [lightslateblue]M[/] for [lightslateblue]menu[/] or [red]E[/] to [red]exit[/]...[/]");

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        switch (keyInfo.Key)
        {
            case ConsoleKey.M:
                Console.Clear();
                Main(null);
            break;

            case ConsoleKey.E:
                Environment.Exit(0);
                break;

            default:
                break;
        }
    }
}