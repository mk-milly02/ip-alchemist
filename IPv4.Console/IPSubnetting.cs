using Spectre.Console;
using System.Net;

namespace IPv4.Console
{
    public static class IPSubnetting
    {
        private static Network Network { get; set; }

        static IPAddress AskForAvailableIPAddress()
        {
            var ip = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter any IP Address in the network [blue]eg. 196.128.0.4[/]: ")
                .PromptStyle(new Style(Color.Aqua))
                .Validate(ip => IPv4Extensions.IsAnIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IP address.[/]")));

            return IPAddress.Parse(ip);
        }

        static int AskForPrefixLength()
        {
            var length = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the prefix length [blue]/network mask[/]: ")
                .PromptStyle(new Style(Color.Chartreuse3))
                .Validate(length => IPv4Extensions.ValidatePrefixLength(length)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! A prefix length must be between 1 and 32[/]")));

            return int.Parse(length);
        }

        static int AskForNumberOfSubnets()
        {
            var subnets = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter the number of subnets: ")
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
                    .PromptStyle(new Style(Color.Gold3_1))
                    .Validate(number => IPv4Extensions.ValidateNumberOfHosts(number)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]! Invalid number of hosts.[/]")));

                output.Add(int.Parse(hosts));
            }

            return output;
        }

        static readonly Action<Network> GetUniformHosts = (network) =>
        {
            int numberOfSubnets = AskForNumberOfSubnets();
            int uniformHosts = (int)(network.TotalHosts / numberOfSubnets);
            List<int> output = new();

            for (int i = 0; i < numberOfSubnets; i++)
            {
                output.Add(uniformHosts);
            }

            network.ActualHosts = output;
        };

        public static void VariedHosts()
        {
            Network = new()
            {
                AvailableAddress = AskForAvailableIPAddress(),
                NetworkBits = AskForPrefixLength(),
                ActualHosts = AskForNumberOfHosts()
            };
            Network.Tabulate();
            Network.Subnet();
        }

        public static void UniformHosts()
        {
            Network = new()
            {
                AvailableAddress = AskForAvailableIPAddress(),
                NetworkBits = AskForPrefixLength()
            };
            GetUniformHosts(Network);
            Network.Tabulate();
            Network.Subnet();
        }
    }
}