using System.Net;
using ip_alchemist.core;
using Xunit;

namespace ip_alchemist_cli.tests
{
    public class SubnettingTests
    {
        [Theory]
        [InlineData("256", 24)]
        [InlineData("1024", 8)]
        [InlineData("512", 16)]
        [InlineData("16", 24)]
        [InlineData("2", 7)]
        public void ValidNumberOfSubnetsTest(string subnets, int prefixLength)
        {
            Assert.True(Subnetting.ValidateNumberOfSubnets(subnets, prefixLength));
        }

        [Fact]
        public void GetNextAvailableIPAddressTest()
        {
            // Given
            var expected = IPAddress.Parse("0.0.0.0");
            // When
            var actual = Subnetting.GetNextAvailableIPAddress(IPAddress.Parse("255.255.255.255"));
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPass_GetActualNumberOfHostsTest()
        {
            // Given
            PowerOfTwo expected = new(6, 64);
            // When
            PowerOfTwo actual = Subnetting.GetActualNumberOfHosts(50);
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPass_GetActualNumberOfHostsFalseTest()
        {
            // Given
            PowerOfTwo expected = new(6, 64);
            // When
            PowerOfTwo actual = Subnetting.GetActualNumberOfHosts(8);
            // Then
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void ShouldPass_GetTotalNumberOfDesiredHosts()
        {
            // Given
            List<VSubnet> subnets = new()
            {
                new() { DesiredHosts = 10 },
                new() { DesiredHosts = 10 },
                new() { DesiredHosts = 10 },
                new() { DesiredHosts = 10 },
                new() { DesiredHosts = 10 }
            };

            long expected = 50;
            // When
            long actual = Subnetting.GetTotalNumberOfDesiredHosts(subnets);
            // Then
            Assert.Equal(expected, actual);
        }
    }
}