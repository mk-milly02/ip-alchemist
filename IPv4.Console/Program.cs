// See https://aka.ms/new-console-template for more information
using IPv4.Console;
using Spectre.Console;

AnsiConsole.Write(new FigletText("IPv4 Addressing").Color(Color.Orange3));
AnsiConsole.MarkupLine("[dim italic]Made by Jonas[/]");
AnsiConsole.WriteLine("\n");

var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
    .Title("[lime]?[/] What do you [green]want to do[/]?")
    .AddChoices("Network Overview", "IP Subnetting"));

switch (choice)
{
    case "Network Overview":
        BasicAddressing.Run();
        break;

    case "IP Subnetting":
        IPSubnetting.Run();
        break;

    default:
        Environment.Exit(0);
        break;
}

Console.ReadKey();
