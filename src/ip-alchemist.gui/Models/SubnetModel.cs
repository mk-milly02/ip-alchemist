using ip_alchemist.core;
using System.Net;

namespace ip_alchemist.gui.Models
{
    public class SubnetModel
    {
        public SubnetModel(Subnet subnet)
        {
            Subnet = subnet;
        }

        public Subnet Subnet { get; set; }

        public string BinaryNetworkMask => Subnet.NetworkMask.binaryMask;
        public IPAddress DecimalNetworkMask => Subnet.NetworkMask.decimalMask;

        public override string ToString() => $"{Subnet.Number}, {Subnet.NetworkMask.decimalMask}, {BinaryNetworkMask}, {Subnet.PrefixLength}, {Subnet.NetworkAddress}, {Subnet.BroadcastAddress}, {Subnet.TotalHosts}, {Subnet.TotalValidHosts}, {Subnet.AddressRange}";
    }
}
