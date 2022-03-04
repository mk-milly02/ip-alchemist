using Spectre.Console;

namespace IPv4.Console
{
    public static class BasicAddressing
    {
        private static BasicNetwork? Network { get; set; }

        static string AskForAvailableIPAddress()
        {
            var ip = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] What is the available IP Address [blue]eg. 196.128.0.4[/]: ")
                .Validate(ip => IPv4Extensions.IsIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IP address.[/]")));

            return ip;
        }

        static int AskForNetworkMask()
        {
            var mask = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the network mask: ")
                .Validate(mask => IPv4Extensions.ValidateNetworkMask(mask)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! A prefix length must be between 1 and 32")));

            return int.Parse(mask);
        }

    }
}