using System.Net;
using Spectre.Console;

namespace IPv4.Console
{
    public class IPSubnetting
    {
        private static Network Network { get; set; }

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

        static int AskForPrefixLength()
        {
            var length = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the network mask: ")
                .PromptStyle(new Style(Color.Chartreuse3))
                .Validate(length => IPv4Extensions.ValidatePrefixLength(length)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! A prefix length must be between 1 and 32[/]")));

            return int.Parse(length);
        }

        static int AskForNumberOfSubnets()
        {
            var subnets = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter your desired number of subnets: ")
                .PromptStyle(new Style(Color.DarkOrange))
                .Validate(number => IPv4Extensions.ValidateNumberOfSubnets(number)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! Invalid number of subnets.[/]"))
            );

            return int.Parse(subnets);
        }

        static List<int> AskForNumberOfHosts()
        {
            int numberOfSubnets = AskForNumberOfSubnets();
            List<int> output = new();

            for (int i = 0; i < numberOfSubnets; i++)
            {
                var hosts = AnsiConsole.Prompt(
                    new TextPrompt<string>($"[lime]?[/] Enter number of hosts in subnet {i + 1}: ")
                    .PromptStyle(new Style(Color.Purple4))
                    .Validate(number => IPv4Extensions.ValidateNumberOfHosts(number)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]! Invalid number of hosts.[/]")));

                output.Add(int.Parse(hosts));
            }

            return output;
        }

        public static void Run()
        {
            Network = new();
            Network.AvailableAddress = IPAddress.Parse(AskForAvailableIPAddress());
            Network.NetworkBits = AskForPrefixLength();
            Network.ActualHosts = AskForNumberOfHosts();
            Network.Tabulate();
            Network.Subnet();
        }
    }
}