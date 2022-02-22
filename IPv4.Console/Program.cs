// See https://aka.ms/new-console-template for more information
using Spectre.Console;

AnsiConsole.Write(new FigletText("IPv4 Addressing").Color(Color.Red));

var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
    .Title("[lightgreen]?[/] What do you [green]want to do[/]?")
    .AddChoices("IP Basics", "IP Subnetting"));
