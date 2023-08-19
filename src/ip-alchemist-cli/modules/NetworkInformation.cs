using ip_alchemist.core;
using Spectre.Console;

namespace ip_alchemist_cli.modules
{
    public static class NetworkInformation
    {
        public static Block? NetworkSegment { get; set; }

        static string PromptForIPAddress()
        {
            string ipAddress = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the IP Address you want to work with [bold]<eg. x.x.x.x>[/]: ")
                .PromptStyle(new Style(Color.Lime))
                .Validate(ip => IPv4Library.ValidateIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IPv4 address.[/]")));

            return ipAddress;
        }

        static int PromptForPrefixLength()
        {
            string length = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the prefix length [bold]/network bits[/]: ")
                .PromptStyle(new Style(Color.Lime))
                .Validate(length => IPv4Library.ValidatePrefixLength(length)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! The prefix length must be >= 0 < 33[/]")));

            return int.Parse(length);
        }

        public static void Execute()
        {
            NetworkSegment = new(PromptForIPAddress(), PromptForPrefixLength());
            NetworkSegment.WriteToConsole();
        }
    }
}