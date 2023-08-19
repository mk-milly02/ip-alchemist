using System.Net;

namespace ip_alchemist.core
{
    public class Block : NetworkSegment
    {
        public Block(string ipAddress, int prefixLength)
        {
            Address = IPAddress.Parse(ipAddress);
            PrefixLength = prefixLength;
        }
    }
}