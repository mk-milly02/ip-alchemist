using System.Collections.Generic;
using IPv4.Console;
using Xunit;

namespace IPv4.Tests
{
    public class IPv4ExtensionsTests
    {
        [Fact]
        public void ShouldPass_GenerateNetworkMask()
        {
            // Given
            string expected = "255.255.255.192";
            // When
            string actual = IPv4Extensions.GenerateNetworkMask(26).ToString();
            // Then
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("12.0.0.1")]
        [InlineData("0.200.100.8")]
        [InlineData("198.25.5.1")]
        public void IsAValidIPAddress(string ip)
        {
            Assert.True(IPv4Extensions.IsIPAddress(ip));
        }
    }
}