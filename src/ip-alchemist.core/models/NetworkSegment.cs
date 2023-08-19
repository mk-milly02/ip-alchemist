using System.Net;

namespace ip_alchemist.core;

public abstract class NetworkSegment
{
    public IPAddress? Address { get; set; }
    public int PrefixLength { get; set; }
    public (IPAddress decimalMask, string binaryMask) NetworkMask => IPv4Library.GenerateNetworkMask(PrefixLength);
    public IPAddress WildcardMask => IPv4Library.GenerateWildcardMask(NetworkMask.decimalMask);
    public IPAddress NetworkAddress => IPv4Library.GenerateNetworkAddress(Address!, NetworkMask.decimalMask);
    public IPAddress BroadcastAddress => IPv4Library.GenerateBroadcastAddress(NetworkAddress, NetworkMask.decimalMask);
    public long TotalHosts => IPv4Library.TotalNumberOfAddresses(PrefixLength);
    public long TotalValidHosts => (TotalHosts - 2) < 1 ? 0 : TotalHosts - 2;
    public string AddressRange => IPv4Library.GenerateAddressRange(NetworkAddress, BroadcastAddress, TotalValidHosts);
    public string NetworkType => IPv4Library.GetNetworkType(Address!);
}