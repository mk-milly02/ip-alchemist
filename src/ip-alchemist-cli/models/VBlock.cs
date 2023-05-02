using System.Net;
using ip_alchemist_cli.libs;
using Spectre.Console;

namespace ip_alchemist_cli.models;

public class VBlock : NetworkSegment
{
    public VBlock(string ipAddress, int prefixLength)
    {
        Address = IPAddress.Parse(ipAddress);
        PrefixLength = prefixLength;
    }

    public int NumberOfSubnets { get; set; }
    public List<VSubnet>? Subnets { get; set; }
    public long TotalNumberOfDesiredHosts => Subnetting.GetTotalNumberOfDesiredHosts(Subnets!);
    public bool CanBeSubnetted => TotalHosts > TotalNumberOfDesiredHosts;

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

    public void GenerateSubnets()
    {
        if (NumberOfSubnets != 1 && CanBeSubnetted)
        {
            Subnets!.First().Address = Address;

            for (int i = 1; i < NumberOfSubnets; i++)
            {
                Subnets![i].Address = Subnetting.GetNextAvailableIPAddress(Subnets![i - 1].BroadcastAddress);
            }

            if (NumberOfSubnets <= 8)
            {
                foreach (var subnet in Subnets!)
                {
                    subnet.Display();
                }
            }
            else
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                            + $"\\{Address!}-{PrefixLength}-{NumberOfSubnets}.csv";

                FileOperations.WriteToCSV(path, Subnets!);
            }
        }
    }
}