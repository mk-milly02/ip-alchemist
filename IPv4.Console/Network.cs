using Spectre.Console;
using System.Net;

namespace IPv4.Console
{
    public class Network
    {
        public IPAddress AvailableAddress { get; set; }

        public IPAddress NetworkMask => IPv4Extensions.GenerateNetworkMask(NetworkBits).decimalMask;

        public string BinaryNetworkMask => IPv4Extensions.GenerateNetworkMask(NetworkBits).binaryMask;

        public int NetworkBits { get; set; }

        public IPAddress NetworkAddress => IPv4Extensions.GetNetworkAddress(AvailableAddress, NetworkMask);

        public IPAddress BroadcastAddress => IPv4Extensions.GetBroadcastAddress(NetworkAddress, NetworkMask);

        public string Range => IPv4Extensions.GetRange(NetworkAddress, BroadcastAddress);

        public int TotalHosts => IPv4Extensions.TotalNumberOfAddresses(NetworkBits);

        public int TotalValidHosts => TotalHosts - 2;

        public List<int> ActualHosts { get; set; }

        public List<PowerOfTwo> SubnetHosts => IPv4Extensions.FindPowersOfTwo(ActualHosts);

        public List<SubNetwork> Subnets { get; set; }

        public virtual void Tabulate()
        {
            AnsiConsole.Write("\n");

            Table output = new();
            output.BorderColor(Color.Gold1);
            output.MinimalBorder();
            output.Width(70);
            output.AddColumns($"", "[violet]Network Information[/]");
            output.AddRow("[cyan]Network mask[/]", NetworkMask.ToString());
            output.AddRow("[blueviolet]Binary network mask[/]", BinaryNetworkMask);
            output.AddRow("Network Bits", "[red]/[/]" + NetworkBits.ToString());
            output.AddRow("[red]Network Address[/]", NetworkAddress.ToString());
            output.AddRow("Broadcast Address", BroadcastAddress.ToString());
            output.AddRow("[lime]Addressess(Total)[/]", TotalHosts.ToString());
            output.AddRow("Valid Hosts", TotalValidHosts.ToString());
            output.AddRow("[blue]Range[/]", $"[yellow]{Range}[/]");

            AnsiConsole.Write(output);
        }

        public void Subnet()
        {
            Subnets = new();
            SubNetwork sub = new()
            {
                Number = 1,
                AvailableAddress = AvailableAddress,
                DesiredHost = ActualHosts[0],
                NetworkBits = 32 - SubnetHosts[0].Index
            };

            Subnets.Add(sub);

            for (int i = 1; i < SubnetHosts.Count; i++)
            {
                SubNetwork sn = new()
                {
                    Number = i + 1,
                    AvailableAddress = IPv4Extensions.GetNextAvailableIP(Subnets[i - 1].BroadcastAddress),
                    DesiredHost = ActualHosts[i],
                    NetworkBits = 32 - SubnetHosts[i].Index
                };
                Subnets.Add(sn);
            }

            foreach (var item in Subnets)
            {
                item.Tabulate();
            }
        }
    }
}