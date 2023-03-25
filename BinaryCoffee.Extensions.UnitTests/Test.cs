using BinaryCoffee.Extensions;
using FluentAssertions;
using Xunit;

namespace Mvvm.Extensions.UnitTests
{
    public static class Test
    {
        [Fact]
        public static void ShouldConvertToCamelCase()
        {
            "1Test this string".ToCamelCase().Should().Be("_1TestThisString");
        }
    }
}
