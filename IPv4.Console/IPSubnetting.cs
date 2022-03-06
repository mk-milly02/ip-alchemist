using Spectre.Console;

namespace IPv4.Console
{
    public class IPSubnetting
    {
        static string AskForAvailableIPAddress()
        {
            var ip = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] What is the available IP Address [blue]eg. 196.128.0.4[/]: ")
                .PromptStyle(new Style(Color.Aqua))
                .Validate(ip => IPv4Extensions.IsIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IP address.[/]")));

            return ip;
        }

        static int AskForNumberOfSubnets()
        {
            var subnets = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter your desired number of subnets: ")
                .PromptStyle(new Style(Color.DarkOrange))
                .Validate(number => int.TryParse(number, out _)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! Invalid number of subnets.[/]"))
            );

            return int.Parse(subnets);
        }
    }
}