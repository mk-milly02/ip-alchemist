using System.Net;
using Spectre.Console;

namespace IPv4.Console
{
    public class Network
    {
        public IPAddress AvailableAddress { get; set; }

        public IPAddress NetworkMask => IPv4Extensions.GenerateNetworkMask(NetworkBits);

        public int NetworkBits { get; set; }

        public IPAddress NetworkAddress => IPv4Extensions.GetNetworkAddress(AvailableAddress, NetworkMask);

        public IPAddress BroadcastAddress => IPv4Extensions.GetBroadcastAddress(NetworkAddress, NetworkMask);

        public string Range => IPv4Extensions.GetRange(NetworkAddress, BroadcastAddress);

        public int TotalHosts => IPv4Extensions.TotalNumberOfAddresses(NetworkBits);

        public int TotalValidHosts => TotalHosts - 2;

        public void Tabulate()
        {
            AnsiConsole.Write("\n");

            Table output = new();
            output.Border(TableBorder.Rounded);
            output.BorderColor(Color.Gold1);
            output.MinimalBorder();
            output.Width(70);
            output.AddColumns($"{Emoji.Known.Rocket}", "[violet]Network Credentials[/]");
            output.AddRow("[cyan]Network mask[/]", NetworkMask.ToString());
            output.AddRow("Network Bits", "[red]/[/]" + NetworkBits.ToString());
            output.AddRow("[red]Network Address[/]", NetworkAddress.ToString());
            output.AddRow("Broadcast Address", BroadcastAddress.ToString());
            output.AddRow("[lime]Addressess(Total)[/]", TotalHosts.ToString());
            output.AddRow("Valid Host", TotalValidHosts.ToString());
            output.AddRow("[blue]Range[/]", $"[yellow]{Range}[/]");

            AnsiConsole.Write(output);
        }
    }
}