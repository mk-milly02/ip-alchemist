// See https://aka.ms/new-console-template for more information
using IPv4.Console;
using Spectre.Console;

AnsiConsole.Write(new FigletText("IPv4 Addressing").Color(Color.Orange3));

var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
    .Title("[lime]?[/] What do you [green]want to do[/]?")
    .AddChoices("Basic Addressing", "IP Subnetting"));

switch (choice)
{
    case "Basic Addressing":
        BasicAddressing.Run();
    break;

    case "IP Subnetting":
    break;

    default:
    Environment.Exit(0);
    break;
}

Console.ReadKey();
