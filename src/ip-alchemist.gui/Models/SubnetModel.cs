using ip_alchemist.core;
using System.Net;

namespace ip_alchemist.gui.Models
{
    class SubnetModel
    {
        public SubnetModel(Subnet subnet)
        {
            Subnet = subnet;
        }

        public Subnet Subnet { get; set; }

        public string BinaryNetworkMask => Subnet.NetworkMask.binaryMask;
        public IPAddress DecimalNetworkMask => Subnet.NetworkMask.decimalMask;
    }
}
