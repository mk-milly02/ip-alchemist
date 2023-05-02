using Spectre.Console;

namespace ip_alchemist_cli.models;

public class VSubnet : Subnet
{
    public long DesiredHosts { get; set; }

    public override void Display()
    {
        AnsiConsole.Write("\n");

        Rule rule = new($"[gold1]Subnet {Number}[/]");
        rule.Justify(Justify.Left);
        AnsiConsole.Write(rule);

        Table output = new();
        output.Border(TableBorder.None);
        output.Width(85);
        output.AddColumns("", "");
        output.AddRow("[cyan]Network mask[/]", NetworkMask.decimalMask.ToString());
        output.AddRow("[cyan]Binary mask[/]", NetworkMask.binaryMask.ToString());
        output.AddRow("Network Bits", "[red]/[/]" + PrefixLength.ToString());
        output.AddRow("[red]Network Address[/]", NetworkAddress.ToString());
        output.AddRow("[red]Broadcast Address[/]", BroadcastAddress.ToString());
        output.AddRow("[lime]Total Hosts[/]", TotalHosts.ToString());
        output.AddRow("[lime]Desired Hosts[/]", DesiredHosts.ToString());
        output.AddRow("[lime]Unused Hosts[/]", (TotalHosts - DesiredHosts).ToString());
        output.AddRow("[lime]Valid Hosts[/]", TotalValidHosts.ToString());
        output.AddRow("[blue]Range[/]", $"[bold italic]{AddressRange}[/]");

        AnsiConsole.Write("\n");
        AnsiConsole.Write(output);
    }
}