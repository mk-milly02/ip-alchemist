using System.Net;
using Spectre.Console;

namespace IPv4.Console
{
    public class SubNetwork : Network
    {
        public int DesiredHost { get; set; } 

        public int SpareHostAddresses  => TotalHosts - DesiredHost;

        public IPAddress NextAvailableIP { get; set; }

        public int Number { get; set; }

        public override void Tabulate()
        {
            AnsiConsole.Write("\n");
            Rule rule = new($"[gold1]Subnet {Number}[/]");
            rule.Alignment(Justify.Left);
            AnsiConsole.Write(rule);

            Table output = new();
            output.Border(TableBorder.None);
            output.Width(70);
            output.AddColumns($"{Emoji.Known.Rocket}", "[violet]Network Credentials[/]");
            output.AddRow("[cyan]Network mask[/]", NetworkMask.ToString());
            output.AddRow("Network Bits", "[red]/[/]" + NetworkBits.ToString());
            output.AddRow("[red]Network Address[/]", NetworkAddress.ToString());
            output.AddRow("Broadcast Address", BroadcastAddress.ToString());
            output.AddRow("[lime]Addressess(Total)[/]", TotalHosts.ToString());
            output.AddRow("[lime]Desired(Total)[/]", DesiredHost.ToString());
            output.AddRow("Valid Host", TotalValidHosts.ToString());
            output.AddRow("[blue]Range[/]", $"[yellow]{Range}[/]");
            output.AddRow("[orange3]Unused Addresses[/]", $"[blueviolet]{SpareHostAddresses}[/]");

            AnsiConsole.Write("\n");
            AnsiConsole.Write(output);
        }
    }
}