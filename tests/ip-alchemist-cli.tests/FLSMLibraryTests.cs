using System.Net;
using ip_alchemist_cli.libs;
using Xunit;

namespace ip_alchemist_cli.tests
{
    public class FLSMLibraryTests
    {
        [Theory]
        [InlineData("256", 24)]
        [InlineData("1024", 8)]
        [InlineData("512", 16)]
        [InlineData("16", 24)]
        [InlineData("2", 7)]
        public void ValidNumberOfSubnetsTest(string subnets, int prefixLength)
        {
            Assert.True(FLSMLibrary.ValidateNumberOfSubnets(subnets, prefixLength));
        }

        [Fact]
        public void GetNextAvailableIPAddressTest()
        {
            // Given
            var expected = IPAddress.Parse("0.0.0.0");
            // When
            var actual = FLSMLibrary.GetNextAvailableIPAddress(IPAddress.Parse("255.255.255.255"));
            // Then
            Assert.Equal(expected, actual);
        }
    }
}