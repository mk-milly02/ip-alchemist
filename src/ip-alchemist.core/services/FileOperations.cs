using System.Text;

namespace ip_alchemist.core;

public static class FileOperations
{
    public static void WriteToCSV(string path, IEnumerable<Subnet> subnets)
    {
        StreamWriter writer = new(new FileStream(path, FileMode.Create, FileAccess.Write));

        writer.WriteLine($"Number, Network Mask, Binary Mask, Prefix Length, Network Address, Broadcast Address, Total Hosts, Total Valid Hosts, Address Range");

        foreach (var subnet in subnets)
        {
            writer.WriteLine(subnet.ToString());

        }
        writer.Close();
    }

    public static string WriteToCSV(IEnumerable<Subnet> subnets)
    {
        StringBuilder builder = new();

        builder.AppendLine($"Number, Network Mask, Binary Mask, Prefix Length, Network Address, Broadcast Address, Total Hosts, Total Valid Hosts, Address Range");

        foreach (Subnet subnet in subnets)
        {
            builder.AppendLine(subnet.ToString());

        }
        return builder.ToString();
    }
}