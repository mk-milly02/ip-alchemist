using ip_alchemist.core;
using Spectre.Console;

namespace ip_alchemist_cli;

public static class Extensions
{
    public static void WriteToConsole(this Block Block)
    {
        AnsiConsole.Write("\n");

        Table output = new();
        output.BorderColor(Color.Gold1);
        output.MinimalBorder();
        output.Width(70);
        output.AddColumns($"", "[violet]Network Information[/]");
        output.AddRow("[cyan]Network mask[/]", Block.NetworkMask.decimalMask.ToString());
        output.AddRow("[blueviolet]Binary network mask[/]", Block.NetworkMask.binaryMask);
        output.AddRow("[navy]Wildcard mask[/]", Block.WildcardMask.ToString());
        output.AddRow("Prefix Length", "[red]/[/]" + Block.PrefixLength);
        output.AddRow("[red]Network Address[/]", Block.NetworkAddress.ToString());
        output.AddRow("Broadcast Address", Block.BroadcastAddress.ToString());
        output.AddRow("Network Type", Block.NetworkType);
        output.AddRow("[lime]Addressess (Total)[/]", Block.TotalHosts.ToString());
        output.AddRow("Valid Hosts", Block.TotalValidHosts.ToString());
        output.AddRow("[blue]Range[/]", $"[yellow]{Block.AddressRange}[/]");

        AnsiConsole.Write(output);
    }

    public static void WriteToConsole(this Subnet Subnet)
    {
        AnsiConsole.Write("\n");
        Rule rule = new($"[gold1]Subnet {Subnet.Number}[/]");
        rule.Justify(Justify.Left);
        AnsiConsole.Write(rule);

        Table output = new();
        output.Border(TableBorder.None);
        output.Width(85);
        output.AddColumns("", "");
        output.AddRow("[cyan]Network mask[/]", Subnet.NetworkMask.decimalMask.ToString());
        output.AddRow("[bold]Binary mask[/]", Subnet.NetworkMask.binaryMask.ToString());
        output.AddRow("Network Bits", "[red]/[/]" + Subnet.PrefixLength.ToString());
        output.AddRow("[red]Network Address[/]", Subnet.NetworkAddress.ToString());
        output.AddRow("Broadcast Address", Subnet.BroadcastAddress.ToString());
        output.AddRow("[lime]Addressess(Total)[/]", Subnet.TotalHosts.ToString());
        output.AddRow("Valid Host", Subnet.TotalValidHosts.ToString());
        output.AddRow("[blue]Range[/]", $"[yellow]{Subnet.AddressRange}[/]");

        AnsiConsole.Write("\n");
        AnsiConsole.Write(output);
    }

    public static void WriteToConsole(this FBlock FBlock)
    {
        AnsiConsole.Write("\n");

        Table output = new();
        output.BorderColor(Color.Gold1);
        output.MinimalBorder();
        output.Width(70);
        output.AddColumns($"", "[violet]Network Information[/]");
        output.AddRow("[cyan]Network mask[/]", FBlock.NetworkMask.decimalMask.ToString());
        output.AddRow("[blueviolet]Binary network mask[/]", FBlock.NetworkMask.binaryMask);
        output.AddRow("[navy]Wildcard mask[/]", FBlock.WildcardMask.ToString());
        output.AddRow("Prefix Length", "[red]/[/]" + FBlock.PrefixLength);
        output.AddRow("[red]Network Address[/]", FBlock.NetworkAddress.ToString());
        output.AddRow("Broadcast Address", FBlock.BroadcastAddress.ToString());
        output.AddRow("Network Type", FBlock.NetworkType);
        output.AddRow("[lime]Addressess (Total)[/]", FBlock.TotalValidHosts.ToString());
        output.AddRow("[blue]Range[/]", $"[yellow]{FBlock.AddressRange}[/]");

        AnsiConsole.Write(output);

        if (FBlock.NumberOfSubnets == 1)
        {
            return;
        }

        FBlock.Subnets = new();

        //create first subnet
        Subnet subnet = new()
        {
            Number = 1,
            Address = FBlock.Address,
            Hosts = FBlock.HostsPerSubnet,
            PrefixLength = 32 - (int)Math.Log2(FBlock.HostsPerSubnet)
        };
        
        FBlock.Subnets.Add(subnet);

        for (int i = 1; i < FBlock.NumberOfSubnets; i++)
        {
            Subnet subnet1 = new()
            {
                Number = i + 1,
                Address = Subnetting.GetNextAvailableIPAddress(FBlock.Subnets[i - 1].BroadcastAddress),
                Hosts = FBlock.NumberOfSubnets,
                PrefixLength = 32 - (int)Math.Log2(FBlock.HostsPerSubnet)
            };
            FBlock.Subnets.Add(subnet1);
        }

        if (FBlock.NumberOfSubnets <= 32)
        {
            foreach (var sn in FBlock.Subnets)
            {
                sn.WriteToConsole();
            }
        }
        else
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                        + $"\\{FBlock.Address!}-{FBlock.PrefixLength}-{FBlock.NumberOfSubnets}.csv";
            FileOperations.WriteToCSV(path, FBlock.Subnets);
        }
    }   

    public static void WriteToConsole(this VBlock VBlock)
    {
        AnsiConsole.Write("\n");

        Table output = new();
        output.BorderColor(Color.Gold1);
        output.MinimalBorder();
        output.Width(70);
        output.AddColumns($"", "[violet]Network Information[/]");
        output.AddRow("[cyan]Network mask[/]", VBlock.NetworkMask.decimalMask.ToString());
        output.AddRow("[blueviolet]Binary network mask[/]", VBlock.NetworkMask.binaryMask);
        output.AddRow("[navy]Wildcard mask[/]", VBlock.WildcardMask.ToString());
        output.AddRow("Prefix Length", "[red]/[/]" + VBlock.PrefixLength);
        output.AddRow("[red]Network Address[/]", VBlock.NetworkAddress.ToString());
        output.AddRow("Broadcast Address", VBlock.BroadcastAddress.ToString());
        output.AddRow("Network Type", VBlock.NetworkType);
        output.AddRow("[lime]Addressess (Total)[/]", VBlock.TotalHosts.ToString());
        output.AddRow("Valid Hosts", VBlock.TotalValidHosts.ToString());
        output.AddRow("[blue]Range[/]", $"[yellow]{VBlock.AddressRange}[/]");

        AnsiConsole.Write(output);
    }

    public static void WriteToConsole(this VSubnet VSubnet)
    {
        AnsiConsole.Write("\n");

        Rule rule = new($"[gold1]Subnet {VSubnet.Number}[/]");
        rule.Justify(Justify.Left);
        AnsiConsole.Write(rule);

        Table output = new();
        output.Border(TableBorder.None);
        output.Width(85);
        output.AddColumns("", "");
        output.AddRow("[cyan]Network mask[/]", VSubnet.NetworkMask.decimalMask.ToString());
        output.AddRow("[cyan]Binary mask[/]", VSubnet.NetworkMask.binaryMask.ToString());
        output.AddRow("Network Bits", "[red]/[/]" + VSubnet.PrefixLength.ToString());
        output.AddRow("[red]Network Address[/]", VSubnet.NetworkAddress.ToString());
        output.AddRow("[red]Broadcast Address[/]", VSubnet.BroadcastAddress.ToString());
        output.AddRow("[lime]Total Hosts[/]", VSubnet.TotalHosts.ToString());
        output.AddRow("[lime]Desired Hosts[/]", VSubnet.DesiredHosts.ToString());
        output.AddRow("[lime]Unused Hosts[/]", (VSubnet.TotalHosts - VSubnet.DesiredHosts).ToString());
        output.AddRow("[lime]Valid Hosts[/]", VSubnet.TotalValidHosts.ToString());
        output.AddRow("[blue]Range[/]", $"[bold italic]{VSubnet.AddressRange}[/]");

        AnsiConsole.Write("\n");
        AnsiConsole.Write(output);
    }

    public static void GenerateSubnets(this VBlock VBlock)
    {
        if (VBlock.CanBeSubnetted)
        {
            VBlock.Subnets!.First().Address = VBlock.Address;

            for (int i = 1; i < VBlock.NumberOfSubnets; i++)
            {
                VBlock.Subnets![i].Address = Subnetting.GetNextAvailableIPAddress(VBlock.Subnets![i - 1].BroadcastAddress);
            }

            if (VBlock.NumberOfSubnets <= 8)
            {
                foreach (var subnet in VBlock.Subnets!)
                {
                    subnet.WriteToConsole();
                }
            }
            else
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                            + $"\\{VBlock.Address!}-{VBlock.PrefixLength}-{VBlock.NumberOfSubnets}.csv";

                FileOperations.WriteToCSV(path, VBlock.Subnets!);
            }
        }

        AnsiConsole.MarkupLine($"[red]! Subnetting error - total number of desired host must be < {VBlock.TotalHosts}.[/]");
    }
}
