using System.Net;

namespace IPv4
{
    public class BasicNetwork
    {
        public IPAddress? AvailableAddress { get; set; }

        public IPAddress? NetworkMask { get; set; }

        public int NetworkBits { get; set; }

        public IPAddress? NetworkAddress { get; set; }

        public IPAddress? BroadcastAddress { get; set; }

        public IPAddress? FirstAddress { get; set; }

        public IPAddress? LastAddress { get; set; }

        public int TotalHosts { get; set; }

        public int TotalValidHosts { get; set; }
    }
}