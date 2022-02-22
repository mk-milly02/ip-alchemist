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
    }
}