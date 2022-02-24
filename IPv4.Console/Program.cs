// See https://aka.ms/new-console-template for more information
using IPv4.Console;
using Spectre.Console;

AnsiConsole.Write(new FigletText("IPv4 Addressing").Color(Color.Red));

var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().PageSize(10)
    .Title("[lime]?[/] What do you [green]want to do[/]?")
    .AddChoices("IP Basics", "IP Subnetting"));

switch (choice)
{
    case "IP Basics":

        var ipWithMask = AnsiConsole.Prompt(
            new TextPrompt<string>("[lime]?[/] Enter the available IP address with mask [blue]eg. 196.128.0.4/24[/]: ")
            .PromptStyle("green")
            .ValidationErrorMessage("[red]> The mask is invalid.[/]")
            .Validate(address => IPv4Extensions.ValidateMask(address)));

        var ip = ipWithMask.Remove(ipWithMask.IndexOf('/'));
        var networkBits = Convert.ToInt32(ipWithMask[(ipWithMask.IndexOf('/') + 1)..]);

        if (IPv4Extensions.IsIPValid(ip))
        {
            string mask = IPv4Extensions.GenerateMask(networkBits);

            AnsiConsole.MarkupLine($"[violet]{mask}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]> This IP address is invalid[/]");
        }

    break;

    default:
    break;
}

Console.ReadKey();
