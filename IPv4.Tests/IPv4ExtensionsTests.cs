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
    }
}