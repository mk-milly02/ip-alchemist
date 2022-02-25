using IPv4.Console;
using Xunit;

namespace IPv4.Tests
{
    public class IPv4ExtensionsTests
    {
        [Theory]
        [InlineData("196.65.45.4/2")]
        [InlineData("196.65.45.4/8")]
        [InlineData("196.65.45.4/16")]
        [InlineData("196.65.45.4/24")]
        [InlineData("196.65.45.4/4")]
        public void ValidateMask(string ip)
        {
            Assert.True(IPv4Extensions.ValidateMask(ip));
        }

        [Fact]
        public void ReturnTrue_IsIPValid()
        {
            // Given
            string ip = "196.65.45.4";

            Assert.True(IPv4Extensions.IsIPValid(ip));
        }

        [Theory]
        [InlineData("196.65.45.4")]
        [InlineData("196.0.45.4")]
        [InlineData("3.65.45")]
        [InlineData("0")]
        [InlineData("196.65")]
        public void IsIPValid(string ip)
        {
            Assert.True(IPv4Extensions.IsIPValid(ip));
        }

        [Fact]
        public void ShouldPass_GenerateMask()
        {
            // Given
            string expected = "255.255.255.0";
            // Then
            string actual = IPv4Extensions.GenerateMask(24);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPass_GetBroadcastAddress()
        {
            // Given
            string ip = "196.168.0.1"; int netBits = 16;

            string mask = IPv4Extensions.GenerateMask(netBits);
            string netAd = IPv4Extensions.GetNetworkAddress(ip, mask);
            string broadAd = IPv4Extensions.GetBroadcastAddress(netAd, mask);
            // Then
            Assert.True(netAd.Equals("196.168.0.0"));
            Assert.True(broadAd.Equals("196.168.255.255"));
        }

        [Fact]
        public void ShouldPass_TotalNumberOfAddresses()
        {
            // Given
            int expected = 256;
            // When
            int actual = IPv4Extensions.TotalNumberOfAddresses(24);
            // Then
            Assert.Equal(expected, actual);
        }
    }
}