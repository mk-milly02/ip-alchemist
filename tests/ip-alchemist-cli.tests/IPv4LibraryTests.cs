using System.Net;
using ip_alchemist_cli.libs;
using Xunit;

namespace ip_alchemist_cli.tests;

public class IPv4LibraryTests
{
    [Theory]
    [InlineData("12.0.0.1")]
    [InlineData("192.168.1.2")]
    [InlineData("198.25.5.1")]
    [InlineData("96.45.45.45")]
    [InlineData("10.0.61.50")]
    [InlineData("0.0.0.0")]
    public void TrueValues_ValidateIPAddressTest(string ipAddress)
    {
        Assert.True(IPv4Library.ValidateIPAddress(ipAddress));
    }

    [Theory]
    [InlineData("12333333")]
    [InlineData("-23.34.77.67")]
    [InlineData("ab.cd.ef.gh")]
    [InlineData("....")]
    [InlineData("265.34.342.2")]
    [InlineData("130.200.100.8/24")]
    public void FalseValues_ValidateIPAddressTest(string ip)
    {
        Assert.False(IPv4Library.ValidateIPAddress(ip));
    }

    [Theory]
    [InlineData("12")]
    [InlineData("2")]
    [InlineData("10")]
    [InlineData("32")]
    [InlineData("24")]
    [InlineData("0")]
    public void ValidatePrefixLengthTest(string prefixLength)
    {
        Assert.True(IPv4Library.ValidatePrefixLength(prefixLength));
    }

    [Fact]
    public void GenerateDecimalNetworkMaskTest()
    {
        // Given
        string expected = "255.255.255.192";
        // When
        string actual = IPv4Library.GenerateNetworkMask(26).decimalMask.ToString();
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GenerateBinaryNetworkMaskTest()
    {
        // Given
        string expected = "11111111.11111111.11111111.11110000";
        // When
        string actual = IPv4Library.GenerateNetworkMask(28).binaryMask;
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GenerateWildcardMaskTest()
    {
        // Given
        string expected = "0.0.255.255";
        // When
        string actual = IPv4Library.GenerateWildcardMask(IPAddress.Parse("255.255.0.0")).ToString();
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MaximumTest_TotalNumberOfAddresses()
    {
        // Given
        long expected = 4294967296;
        // When
        long actual = IPv4Library.TotalNumberOfAddresses(0);
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GenerateNetworkAddressTest()
    {
        // Given
        IPAddress expected = IPAddress.Parse("192.168.1.0");
        // When
        IPAddress actual = IPv4Library.GenerateNetworkAddress(
            IPAddress.Parse("192.168.1.56"), IPAddress.Parse("255.255.255.0"));
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GenerateBroadcastAddressTest()
    {
        // Given
        IPAddress expected = IPAddress.Parse("192.168.1.255");
        // When
        IPAddress actual = IPv4Library.GenerateBroadcastAddress(
            IPAddress.Parse("192.168.1.0"), IPAddress.Parse("255.255.255.0"));
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalNumberOfAddressesTest()
    {
        // Given
        long expected = 256;
        // When
        long actual = IPv4Library.TotalNumberOfAddresses(24);
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GenerateAddressRangeTest()
    {
        // Given
        string expected = "192.168.1.1 ~ 192.168.1.254";
        // When
        string actual = IPv4Library.GenerateAddressRange(
            IPAddress.Parse("192.168.1.0"), IPAddress.Parse("192.168.1.255"), 254);
        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Null_GenerateAddressRangeTest()
    {
        // Given
        string expected = "There are no valid addresses.";
        // When
        IPAddress netAdd = IPv4Library.GenerateNetworkAddress(IPAddress.Parse("192.168.1.2"), IPAddress.Parse("255.255.255.254"));
        IPAddress broadAdd = IPv4Library.GenerateBroadcastAddress(netAdd, IPAddress.Parse("255.255.255.254"));
        string actual = IPv4Library.GenerateAddressRange(netAdd, broadAdd, 0);
        // Then
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("8.8.8.8")]
    [InlineData("225.0.0.4")]
    [InlineData("41.209.2.8")]
    [InlineData("0.100.0.2")]
    [InlineData("172.32.48.18")]
    [InlineData("192.178.0.1")]
    public void ValidPublicAddresses_GetAddressTypeTest(string ipAddress)
    {
        Assert.Equal("Public", IPv4Library.GetAddressType(IPAddress.Parse(ipAddress)));
    }

    [Theory]
    [InlineData("10.8.8.8")]
    [InlineData("172.16.0.4")]
    [InlineData("172.25.2.8")]
    [InlineData("192.168.1.2")]
    [InlineData("10.255.48.18")]
    [InlineData("192.168.0.1")]
    public void ValidPrivateAddresses_GetAddressTypeTest(string ipAddress)
    {
        Assert.Equal("Private", IPv4Library.GetAddressType(IPAddress.Parse(ipAddress)));
    }

    [Fact]
    public void Link_Local_GetAddressTypeTest()
    {
        Assert.Equal("Link-Local", IPv4Library.GetAddressType(IPAddress.Parse("169.254.1.3")));
    }

    [Fact]
    public void Loopback_GetAddressTypeTest()
    {
        Assert.Equal("Loopback", IPv4Library.GetAddressType(IPAddress.Parse("127.0.0.1")));
    }

    [Theory]
    [InlineData("100.2.0.1")]
    [InlineData("126.0.0.1")]
    [InlineData("1.0.0.1")]
    public void ClassA_GetAddressClassTest(string ip)
    {
        Assert.Equal("Class A", IPv4Library.GetAddressClass(IPAddress.Parse(ip)));
    }

    [Theory]
    [InlineData("130.2.0.1")]
    [InlineData("170.0.254.1")]
    [InlineData("190.0.0.1")]
    public void ClassB_GetAddressClassTest(string ip)
    {
        Assert.Equal("Class B", IPv4Library.GetAddressClass(IPAddress.Parse(ip)));
    }

    [Theory]
    [InlineData("200.2.0.1")]
    [InlineData("192.0.0.1")]
    [InlineData("197.200.0.1")]
    public void ClassC_GetAddressClassTest(string ip)
    {
        Assert.Equal("Class C", IPv4Library.GetAddressClass(IPAddress.Parse(ip)));
    }

    [Theory]
    [InlineData("229.0.0.1")]
    [InlineData("230.0.0.1")]
    [InlineData("235.2.0.1")]
    public void ClassD_GetAddressClassTest(string ip)
    {
        Assert.Equal("Class D", IPv4Library.GetAddressClass(IPAddress.Parse(ip)));
    }

    [Theory]
    [InlineData("250.0.0.1")]
    [InlineData("245.2.0.1")]
    [InlineData("251.0.0.1")]
    public void ClassE_GetAddressClassTest(string ip)
    {
        Assert.Equal("Class E", IPv4Library.GetAddressClass(IPAddress.Parse(ip)));
    }

    [Theory]
    [InlineData("20", 24)]
    [InlineData("200", 16)]
    [InlineData("2000", 12)]
    [InlineData("20000", 8)]
    [InlineData("2000000", 8)]
    public void Valid_ValidateNumberOfSubnetHosts(string hosts, int prefixLength)
    {
        Assert.True(IPv4Library.ValidateNumberOfHosts(hosts, prefixLength));
    }

    [Theory]
    [InlineData("20", 30)]
    [InlineData("16777217", 8)]
    [InlineData("2000", 24)]
    [InlineData("wde", 8)]
    [InlineData("1", 8)]
    public void InValid_ValidateNumberOfSubnetHosts(string hosts, int prefixLength)
    {
        Assert.False(IPv4Library.ValidateNumberOfHosts(hosts, prefixLength));
    }
}