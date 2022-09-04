using Spectre.Console;
using System.Net;

namespace IPv4.Console
{
    public static class NetworkOverview
    {
        private static Network Network { get; set; }

        static string AskForAvailableIPAddress()
        {
            var ip = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the IP Address [dodgerblue2 bold]<eg. [italic]196.128.0.4[/]>[/]: ")
                .PromptStyle(new Style(Color.Aqua))
                .Validate(ip => IPv4Extensions.IsAnIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IPv4 address.[/]")));

            return ip;
        }

        static int AskForPrefixLength()
        {
            var length = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the prefix length [blue]/network bits[/]: ")
                .PromptStyle(new Style(Color.Chartreuse3))
                .Validate(length => IPv4Extensions.ValidatePrefixLength(length)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! The prefix length must be between 0 and 33[/]")));

            return int.Parse(length);
        }

        public static void Run()
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