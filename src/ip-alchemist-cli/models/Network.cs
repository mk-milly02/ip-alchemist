using System.Net;
using ip_alchemist_cli.libs;
using Spectre.Console;

namespace ip_alchemist_cli.models
{
    public class Network
    {
        public Network(string ipAddress, int prefixLength)
        {
            Address = IPAddress.Parse(ipAddress);
            PrefixLength = prefixLength;
        }

        public IPAddress? Address { get; set; }
        public int PrefixLength { get; set; }
        public (IPAddress decimalMask, string binaryMask) NetworkMask => IPv4Library.GenerateNetworkMask(PrefixLength);
        public IPAddress WildcardMask => IPv4Library.GenerateWildcardMask(NetworkMask.decimalMask);
        public IPAddress NetworkAddress => IPv4Library.GenerateNetworkAddress(Address!, NetworkMask.decimalMask);
        public IPAddress BroadcastAddress => IPv4Library.GenerateBroadcastAddress(NetworkAddress, NetworkMask.decimalMask);
        public long TotalHosts => IPv4Library.TotalNumberOfAddresses(PrefixLength);
        public long TotalValidHosts => (TotalHosts - 2) < 1 ? 0 : TotalHosts - 2;
        public string AddressRange => IPv4Library.GenerateAddressRange(NetworkAddress, BroadcastAddress, TotalValidHosts);
        public string AddressType => IPv4Library.GetAddressType(Address!);

        #region Methods
        public virtual void Display()
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
            output.AddRow("Network Bits (CIDR)", "[red]/[/]" + PrefixLength);
            output.AddRow("[red]Network Address[/]", NetworkAddress.ToString());
            output.AddRow("Broadcast Address", BroadcastAddress.ToString());
            output.AddRow("Address Type", AddressType);
            output.AddRow("[lime]Addressess (Total)[/]", TotalHosts.ToString());
            output.AddRow("Valid Hosts", TotalValidHosts.ToString());
            output.AddRow("[blue]Range[/]", $"[yellow]{AddressRange}[/]");

            AnsiConsole.Write(output);
        }
        #endregion
    }
}