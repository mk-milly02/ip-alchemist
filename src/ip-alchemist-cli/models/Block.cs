using System.Net;
using Spectre.Console;

namespace ip_alchemist_cli.models
{
    public class Block : NetworkSegment
    {
        public Block(string ipAddress, int prefixLength)
        {
            Address = IPAddress.Parse(ipAddress);
            PrefixLength = prefixLength;
        }

        public override void Display()
        {
            AnsiConsole.Write("\n");

            Table output = new();
            output.BorderColor(Color.Gold1);
            output.MinimalBorder();
            output.Width(70);
            output.AddColumns($"", "[violet]Network Information[/]");
            output.AddRow("[cyan]Network mask[/]", NetworkMask.decimalMask.ToString());
            output.AddRow("[blueviolet]Binary network mask[/]", NetworkMask.binaryMask);
            output.AddRow("[navy]Wildcard mask[/]", WildcardMask.ToString());
            output.AddRow("Prefix Length", "[red]/[/]" + PrefixLength);
            output.AddRow("[red]Network Address[/]", NetworkAddress.ToString());
            output.AddRow("Broadcast Address", BroadcastAddress.ToString());
            output.AddRow("Network Type", NetworkType);
            output.AddRow("[lime]Addressess (Total)[/]", TotalHosts.ToString());
            output.AddRow("Valid Hosts", TotalValidHosts.ToString());
            output.AddRow("[blue]Range[/]", $"[yellow]{AddressRange}[/]");

            AnsiConsole.Write(output);
        }
    }
}