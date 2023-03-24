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

        AnsiConsole.Write(new FigletText("ip-alchemist-cli").Color(Color.Lime));
        AnsiConsole.MarkupLine("[bold]-- A cli toolkit for perfroming various IP address operations.[/]");

        var version = Assembly.GetExecutingAssembly().GetName().Version;
        AnsiConsole.MarkupLine($"[bold]-- Developed by [link=https://github.com/mk-milly02]@mk-milly02[/] | Version {version}.[/]");

        AnsiConsole.MarkupLine("\n");

        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
            .Title("[lime]?[/] [bold]Select from the [blue]menu[/]:[/]")
            .AddChoices("- ipv4-utils")
            .AddChoiceGroup("- subnetting", new[] { "fixed length subnet mask (FLSM)", "variable length subnet mask (VLSM)" })
            .AddChoices("- exit"));

        switch (choice)
        {
            case "- ipv4-utils":
                IPv4Utils.Execute();
                break;

            case "fixed length subnet mask (FLSM)":
                FLSM.Execute();
                break;

            case "- exit":
                Console.Clear();
                Environment.Exit(0);
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
                Console.Clear();
                Environment.Exit(0);
                break;

            default:
                break;
        }

        Console.ReadLine();
    }
}