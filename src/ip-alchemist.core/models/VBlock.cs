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
    public long TotalNumberOfDesiredHosts => Subnetting.GetTotalNumberOfDesiredHosts(Subnets!);
    public bool CanBeSubnetted => TotalHosts > TotalNumberOfDesiredHosts;
}