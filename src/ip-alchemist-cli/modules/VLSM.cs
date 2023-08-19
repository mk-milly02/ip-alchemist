using ip_alchemist.core;
using Spectre.Console;

namespace ip_alchemist_cli.modules;

public static class VLSM
{
    private static VBlock? Network { get; set; }

    static string PromptForIPAddress()
    {
        string ipAddress = AnsiConsole.Prompt(
            new TextPrompt<string>("[lime]?[/] Enter the IP Address you want to work with [bold]<eg. [italic]x.x.x.x[/]>[/]: ")
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
            .Validate(length => Subnetting.ValidatePrefixLength(length)
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]! The prefix length must be >= 1 <= 30[/]")));

        return int.Parse(length);
    }

    static int PromptForNumberOfSubnets(int prefixLength)
    {
        string numberOfSubnets = AnsiConsole.Prompt(
            new TextPrompt<string>("[lime]?[/] Enter the number of [bold]subnets[/]: ")
            .PromptStyle(new Style(Color.Lime))
            .Validate(subnets => Subnetting.ValidateNumberOfSubnets(subnets, prefixLength)
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]! Invalid number of subnets (must be a power of 2)[/]"))
        );
        return int.Parse(numberOfSubnets);
    }

    static readonly Action<VBlock> PromptForDesiredNumberOfHostsPerSubnet = (network) =>
    {
        network.Subnets = new();

        for (int i = 0; i < network.NumberOfSubnets; i++)
        {
            string numberOfHosts = AnsiConsole.Prompt(
                new TextPrompt<string>($"[lime]?[/] Input the number of hosts in [bold]subnet {i + 1}[/]: ")
                .PromptStyle(new Style(Color.Orange1, decoration: Decoration.Italic))
                .Validate(hosts => Subnetting.ValidateNumberOfHostsPerSubnet(hosts, network.PrefixLength)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! Invalid number of hosts[/]"))
            );

            PowerOfTwo powerOfTwo = Subnetting.GetActualNumberOfHosts(int.Parse(numberOfHosts));

            VSubnet subnet = new()
            {
                Number = i + 1,
                DesiredHosts = long.Parse(numberOfHosts),
                Hosts = powerOfTwo.Power,
                PrefixLength = 32 - powerOfTwo.Index
            };
            network.Subnets.Add(subnet);
        }
    };

    public static void Execute()
    {
        Network = new(PromptForIPAddress(), PromptForPrefixLength());
        Network.NumberOfSubnets = PromptForNumberOfSubnets(Network.PrefixLength);
        PromptForDesiredNumberOfHostsPerSubnet(Network);
        Network.WriteToConsole();
        Network.GenerateSubnets();
    }
}