using ip_alchemist_cli.libs;
using Spectre.Console;

namespace ip_alchemist_cli.modules
{
    public static class IPv4Utils
    {
        static string PromptForIPAddress()
        {
            var ipAddress = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the IP Address you want to work with [dodgerblue2 bold]<eg. [italic]192.168.0.1[/]>[/]: ")
                .PromptStyle(new Style(Color.Aqua))
                .Validate(ip => IPv4Library.ValidateIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IPv4 address.[/]")));

            return ipAddress;
        }

        static void Execute()
        {
            Network = new()
            {
                AvailableAddress = IPAddress.Parse(AskForAvailableIPAddress()),
                NetworkBits = AskForPrefixLength()
            };
            Network.Tabulate();
        }
    }
}