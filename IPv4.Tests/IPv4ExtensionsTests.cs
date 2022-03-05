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
    }
}