// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Text;
using ip_alchemist_cli.modules;
using Spectre.Console;

internal class Program
{
    private static void Main(string[]? args)
    {
        Console.Title = "ip-alchemist-cli";
        Console.OutputEncoding = Encoding.UTF8;

        AnsiConsole.Write(new FigletText("ip-alchemist-cli").Color(Color.DarkSlateGray2));

        var version = Assembly.GetExecutingAssembly().GetName().Version;
        AnsiConsole.MarkupLine($"[italic]version {version}[/]".PadLeft(115));

        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
            .Title("[lime]?[/] [bold]Select from the [blue]menu[/]:[/]")
            .AddChoices("ipv4-utils"));

        switch (choice)
        {
            case "ipv4-utils":
                IPv4Utils.Execute();
                break;

            default:
                return;
        }

        AnsiConsole.Markup("\n[chartreuse1]Press [lightslateblue]m[/] to return to the [lightslateblue]main menu[/] or [red]e[/] to [red]exit[/]...[/]");

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

        Console.ReadLine();
    }
}