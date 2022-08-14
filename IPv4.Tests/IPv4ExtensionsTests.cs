using IPv4.Console;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace IPv4.Tests
{
    public class IPv4ExtensionsTests
    {
        [Fact]
        public void GenerateNetworkMaskInDecimalTest()
        {
            // Given
            string expected = "255.255.255.192";
            // When
            string actual = IPv4Extensions.GenerateNetworkMask(26).decimalMask.ToString();
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenerateNetworkMaskInBinaryTest()
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
        [InlineData("192.168.1.2")]
        [InlineData("198.25.5.1")]
        [InlineData("96.45.45.45")]
        [InlineData("10.0.61.50")]
        [InlineData("0.0.0.0")]
        public void TrueValues_IsAValidIPAddressTest(string ip)
        {
            Assert.True(IPv4Extensions.IsAnIPAddress(ip));
        }

        [Theory]
        [InlineData("12333333")]
        [InlineData("-23.34.77.67")]
        [InlineData("ab.cd.ef.gh")]
        [InlineData("....")]
        [InlineData("265.34.342.2")]
        [InlineData("130.200.100.8/24")]
        public void FalseValues_IsAValidIPAddressTest(string ip)
        {
            Assert.False(IPv4Extensions.IsAnIPAddress(ip));
        }

        [Fact]
        public void ValidateNumberOfSubnetsTest()
        {
            Assert.True(IPv4Extensions.ValidateNumberOfSubnets("32"));
        }

        [Fact]
        public void ValidateNumberOfHostsTest()
        {
            Assert.False(IPv4Extensions.ValidateNumberOfHosts("12022028287383772"));
        }

        [Fact]
        public void FindPowersOfTwoTest()
        {
            // Given
            List<PowerOfTwo> expected = new() { new(4, 16), new(5, 32), new(5, 32), new(6, 64), new(7, 128) };
            // When
            var actual = IPv4Extensions.FindPowersOfTwo(new() { 10, 20, 30, 40, 100 });
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetNextAvailableIPTest()
        {
            // Given
            string expected = "198.126.0.1";
            // When
            string actual = IPv4Extensions.GetNextAvailableIP(IPAddress.Parse("198.126.0.0")).ToString();
            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetWildcardMaskTest()
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
        public void IsAValidPrefixLengthTest(string prefixLength)
        {
            Assert.True(IPv4Extensions.ValidatePrefixLength(prefixLength));
        }
    }
}