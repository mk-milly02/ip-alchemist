// See https://aka.ms/new-console-template for more information
using IPv4.Console;
using Spectre.Console;

Console.Title = "IPv4 Calculator";

AnsiConsole.Write(new FigletText("IPv4 Calculator").Color(Color.Orange3));

AnsiConsole.MarkupLine("[dim italic]Made by mk-milly02[/]");

AnsiConsole.WriteLine("\n");

var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
    .Title("[lime]?[/] What do you [green]want to do[/]?")
    .AddChoices("Network Overview")
    .AddChoiceGroup("IP Subnetting", new[] {"Uniform hosts", "Varied hosts"}));

switch (choice)
{
    case "Network Overview":
        BasicAddressing.Run();
        break;

    case "Varied hosts":
        IPSubnetting.VariedHosts();
        break;

    case "Uniform hosts":
        IPSubnetting.UniformHosts();
        break;
        
    default:
        Environment.Exit(0);
        break;
}

AnsiConsole.MarkupLine("[chartreuse1]Press any key to exit...[/]");
Console.ReadKey();
