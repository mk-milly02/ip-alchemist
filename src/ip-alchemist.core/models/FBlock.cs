using System.Net;

namespace ip_alchemist.core
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
    }
}