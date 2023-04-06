using Spectre.Console;

namespace ip_alchemist_cli.models
{
    public class Subnet : NetworkSegment
    {
        public int Number { get; set; }
        public long Hosts { get; set; }

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
            output.AddRow("[bold]Binary mask[/]", NetworkMask.binaryMask.ToString());
            output.AddRow("Network Bits", "[red]/[/]" + PrefixLength.ToString());
            output.AddRow("[red]Network Address[/]", NetworkAddress.ToString());
            output.AddRow("Broadcast Address", BroadcastAddress.ToString());
            output.AddRow("[lime]Addressess(Total)[/]", TotalHosts.ToString());
            output.AddRow("Valid Host", TotalValidHosts.ToString());
            output.AddRow("[blue]Range[/]", $"[yellow]{AddressRange}[/]");

            AnsiConsole.Write("\n");
            AnsiConsole.Write(output);
        }

        public override string ToString() => $"{Number}, {NetworkMask.decimalMask}, {NetworkMask.binaryMask}, {PrefixLength}, {NetworkAddress}, {BroadcastAddress}, {TotalHosts}, {TotalValidHosts}, {AddressRange}";
    }
}