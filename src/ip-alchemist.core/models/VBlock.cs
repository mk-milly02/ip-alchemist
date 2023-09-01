using System.Net;

namespace ip_alchemist.core;

public class VBlock : NetworkSegment
{
    public VBlock(string ipAddress, int prefixLength)
    {
        Address = IPAddress.Parse(ipAddress);
        PrefixLength = prefixLength;
    }

    public int NumberOfSubnets { get; set; }
    public List<VSubnet>? Subnets { get; set; }
    public long TotalNumberOfDesiredHosts => Subnetting.GetTotalNumberOfHosts(Subnets!);
    public bool CanBeSubnetted => TotalHosts > TotalNumberOfDesiredHosts;

    //percentage of available major network address space used.
    public double AddressSpaceUsed => TotalNumberOfDesiredHosts * 100/ TotalHosts;
}