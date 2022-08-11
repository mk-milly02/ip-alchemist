using IPv4.Console;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace IPv4.Tests
{
    public class IPv4ExtensionsTests
    {
        [Fact]
        public void ShouldPass_GenerateNetworkMaskInDecimal()
        {
            // Given
            string expected = "255.255.255.192";
            // When
            string actual = IPv4Extensions.GenerateNetworkMask(26).decimalMask.ToString();
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPass_GenerateNetworkMaskInBinary()
        {
            // Given
            string expected = "11111111.11111111.11111111.11000000";
            // When
            string actual = IPv4Extensions.GenerateNetworkMask(26).binaryMask;
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

        [Fact]
        public void ShouldPass_ValidateNumberOfSubnets()
        {
            Assert.True(IPv4Extensions.ValidateNumberOfSubnets("32"));
        }

        [Fact]
        public void ShouldPass_ValidateNumberOfHosts()
        {
            Assert.False(IPv4Extensions.ValidateNumberOfHosts("12022028287383772"));
        }

        [Fact]
        public void ShouldPass_FindPowersOfTwo()
        {
            // Given
            List<PowerOfTwo> expected = new() { new(4, 16), new(5, 32), new(5, 32), new(6, 64), new(7, 128) };
            // When
            var actual = IPv4Extensions.FindPowersOfTwo(new() { 10, 20, 30, 40, 100 });
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPass_GetNextAvailableIP()
        {
            // Given
            string expected = "198.126.0.1";
            // When
            string actual = IPv4Extensions.GetNextAvailableIP(IPAddress.Parse("198.126.0.0")).ToString();
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPass_GetWildcardMask()
        {
            // Given
            string expected = "0.0.0.255";
            // When
            string actual = IPv4Extensions.GetWildcardMask(IPAddress.Parse("255.255.255.0")).ToString();
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaxTest_TotalNumberOfAddresses()
        {
            // Given
            uint expected = 2147483648;
            // When
            uint actual = IPv4Extensions.TotalNumberOfAddresses(1);
            // Then
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("2")]
        [InlineData("10")]
        [InlineData("32")]
        [InlineData("24")]
        public void IsAValidPrefixLength(string prefixLength)
        {
            Assert.True(IPv4Extensions.ValidatePrefixLength(prefixLength));
        }
    }
}