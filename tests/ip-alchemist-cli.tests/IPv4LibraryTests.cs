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

}