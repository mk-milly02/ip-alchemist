using ip_alchemist_cli.models;
using Spectre.Console;

namespace ip_alchemist_cli.libs
{
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
            AnsiConsole.MarkupLine($"\n[blue]![/] Output printed to [link={path}]this file on your desktop[/]");
        }
    }
}