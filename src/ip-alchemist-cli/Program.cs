// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Text;
using Spectre.Console;

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

        break;

    default:
        return;
}

Console.ReadLine();
