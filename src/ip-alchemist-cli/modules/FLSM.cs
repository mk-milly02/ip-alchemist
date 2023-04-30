using ip_alchemist_cli.libs;
using ip_alchemist_cli.models;
using Spectre.Console;

namespace ip_alchemist_cli.modules;

public static class FLSM
{
    public static FBlock? NetworkSegment { get; set; }

    static string PromptForIPAddress()
    {
        var ipAddress = AnsiConsole.Prompt(
            new TextPrompt<string>("[lime]?[/] Enter the IP Address you want to work with [bold]<eg. [italic]x.x.x.x[/]>[/]: ")
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
            .Validate(length => FLSMLibrary.ValidatePrefixLength(length)
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]! The prefix length must be >= 1 <= 30[/]")));

        return int.Parse(length);
    }

    static int PromptForNumberOfSubnets(int prefixLength)
    {
        var numberOfSubnets = AnsiConsole.Prompt(
            new TextPrompt<string>("[lime]?[/] Enter the number of [bold]subnets[/]: ")
            .PromptStyle(new Style(Color.Lime))
            .Validate(subnets => FLSMLibrary.ValidateNumberOfSubnets(subnets, prefixLength)
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]! Invalid number of subnets (must be a power of 2)[/]"))
        );
        return int.Parse(numberOfSubnets);
    }

    public static void Execute()
    {
        NetworkSegment = new(PromptForIPAddress(), PromptForPrefixLength());
        NetworkSegment.NumberOfSubnets = PromptForNumberOfSubnets(NetworkSegment.PrefixLength);
        NetworkSegment.Display();
    }
}