namespace ip_alchemist.core;

public class Subnet : NetworkSegment
{
    public int Number { get; set; }
    public long Hosts { get; set; }

    public override string ToString() => $"{Number}, {NetworkMask.decimalMask}, {NetworkMask.binaryMask}, {PrefixLength}, {NetworkAddress}, {BroadcastAddress}, {TotalHosts}, {TotalValidHosts}, {AddressRange}";
}