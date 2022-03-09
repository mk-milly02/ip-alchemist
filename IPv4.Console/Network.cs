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

        public List<PowerOfTwo> SubnetHosts { get; set; }

        public List<SubNetwork> Subnets { get; set; }

        public virtual void Tabulate()
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

        public void Subnet()
        {
            Subnets = new();
            SubNetwork sub = new();
            sub.Number = 1;
            sub.AvailableAddress = AvailableAddress;
            sub.DesiredHost = SubnetHosts[0].Power;
            sub.NetworkBits = 32 - SubnetHosts[0].Index;

            Subnets.Add(sub);

            for (int i = 1; i < SubnetHosts.Count; i++)
            {
                SubNetwork sn = new();
                sub.Number = i + 1;
                sn.AvailableAddress = 
                IPv4Extensions.GetNextAvailableIP(Subnets[i - 1].BroadcastAddress);
                sn.DesiredHost = SubnetHosts[i].Power;
                sn.NetworkBits = 32 - SubnetHosts[i].Index;
                Subnets.Add(sn);
            }

            foreach (var item in Subnets)
            {
                item.Tabulate();
            }
        }
    }
}