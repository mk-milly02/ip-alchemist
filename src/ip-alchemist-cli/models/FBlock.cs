using System.Net;
using ip_alchemist_cli.libs;
using Spectre.Console;

namespace ip_alchemist_cli.models
{
    public class FBlock : NetworkSegment
    {
        public FBlock(string ipAddress, int prefixLength)
        {
            Address = IPAddress.Parse(ipAddress);
            PrefixLength = prefixLength;
        }

        public int NumberOfSubnets { get; set; }
        public int HostsPerSubnet => (int)(TotalHosts / NumberOfSubnets);
        public List<Subnet>? Subnets { get; set; }

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

            GetSubnets();
        }

        public void GetSubnets()
        {
            if (NumberOfSubnets == 1)
            {
                return;
            }

            Subnets = new();

            //create first subnet
            Subnet subnet = new()
            {
                Number = 1,
                Address = Address,
                Hosts = HostsPerSubnet,
                PrefixLength = 32 - (int)Math.Log2(HostsPerSubnet)
            };

            Subnets.Add(subnet);

            for (int i = 1; i < NumberOfSubnets; i++)
            {
                Subnet subnet1 = new()
                {
                    Number = i + 1,
                    Address = FLSMLibrary.GetNextAvailableIPAddress(Subnets[i - 1].BroadcastAddress),
                    Hosts = NumberOfSubnets,
                    PrefixLength = 32 - (int)Math.Log2(HostsPerSubnet)
                };
                Subnets.Add(subnet1);
            }

            if (NumberOfSubnets <= 32)
            {
                foreach (var sn in Subnets)
                {
                    sn.Display();
                }
            }
            else
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                            + $"\\{Address!}-{PrefixLength}-{NumberOfSubnets}.csv";

                FileOperations.WriteToCSV(path, Subnets);
            }
        }
    }
}