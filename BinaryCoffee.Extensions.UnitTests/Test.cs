using BinaryCoffee.Extensions.Helpers;
using FluentAssertions;
using Xunit;

namespace Mvvm.Extensions.UnitTests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("my variable", "MyVariable")]
        [InlineData("myVariable", "MyVariable")]
        [InlineData("my_variable", "MyVariable")]
        [InlineData("my-variable", "MyVariable")]
        [InlineData("my variAb-le", "MyVariAbLe")]
        public void ToPascalCaseTest(string input, string expected)
        {
            input.ToPascalCase().Should().Be(expected);
        }

        [Theory]
        [InlineData("MyVariable", "myVariable")]
        [InlineData("my_variable", "myVariable")]
        [InlineData("my-variable", "myVariable")]
        public void ToCamelCaseTest(string input, string expected)
        {
            input.ToCamelCase().Should().Be(expected);
        }

        [Theory]
        [InlineData("MyVariable", "my_variable")]
        [InlineData("myVariable", "my_variable")]
        [InlineData("my-variable", "my_variable")]
        [InlineData("my variAb-le", "my_vari_ab_le")]
        public void ToLowerSnakeCaseTest(string input, string expected)
        {
            input.ToSnakeLowerCase().Should().Be(expected);
        }

        [Theory]
        [InlineData("MyVariable", "MY_VARIABLE")]
        [InlineData("myVariable", "MY_VARIABLE")]
        [InlineData("my-variable", "MY_VARIABLE")]
        [InlineData("my variable", "MY_VARIABLE")]
        [InlineData("my variAb-le", "MY_VARI_AB_LE")]
        public void ToUpperSnakeCaseTest(string input, string expected)
        {
            input.ToSnakeUpperCase().Should().Be(expected);
        }

        private class Person
        {
            public int Age { get; set; }
        }
    }
}
