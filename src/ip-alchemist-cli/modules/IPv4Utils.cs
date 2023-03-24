using ip_alchemist_cli.libs;
using ip_alchemist_cli.models;
using Spectre.Console;

namespace ip_alchemist_cli.modules
{
    public static class IPv4Utils
    {
        public static Network? Network { get; set; }

        static string PromptForIPAddress()
        {
            var ipAddress = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the IP Address you want to work with [bold]<eg. x.x.x.x>[/]: ")
                .PromptStyle(new Style(Color.Lime))
                .Validate(ip => IPv4Library.ValidateIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IPv4 address.[/]")));

            return ipAddress;
        }

        static int PromptForPrefixLength()
        {
            var length = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the prefix length [bold]/network bits[/]: ")
                .PromptStyle(new Style(Color.Lime))
                .Validate(length => IPv4Library.ValidatePrefixLength(length)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! The prefix length must be >= 0 < 33[/]")));

            return int.Parse(length);
        }

        public static void Execute()
        {
            Network = new(PromptForIPAddress(), PromptForPrefixLength());
            Network.Display();
        }
    }
}