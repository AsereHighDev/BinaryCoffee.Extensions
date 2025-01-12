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

        [Theory]
        [InlineData("example@domain.com", "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", true)]
        [InlineData("not-an-email", "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", false)]
        public void MatchesPatternTest(string input, string pattern, bool expected)
        {
            input.MatchesPattern(pattern).Should().Be(expected);
        }

        [Theory]
        [InlineData("<p>Hello World</p>", "Hello World")]
        [InlineData("<div><span>Content</span></div>", "Content")]
        public void StripTagsTest(string input, string expected)
        {
            input.StripTags().Should().Be(expected);
        }

        [Theory]
        [InlineData("Hello World", "dlroW olleH")]
        [InlineData("abcd", "dcba")]
        public void ReverseTest(string input, string expected)
        {
            input.Reverse().Should().Be(expected);
        }

        [Theory]
        [InlineData("This is a Test", "this-is-a-test")]
        [InlineData("URL Slug Example", "url-slug-example")]
        public void ToSlugTest(string input, string expected)
        {
            input.ToSlug().Should().Be(expected);
        }

        [Theory]
        [InlineData("hello world", "Hello World")]
        [InlineData("multiple words example", "Multiple Words Example")]
        public void CapitalizeWordsTest(string input, string expected)
        {
            input.CapitalizeWords().Should().Be(expected);
        }

        [Theory]
        [InlineData("  Extra   spaces  here  ", "Extra spaces here")]
        [InlineData("Multiple\tTabs and\nNewlines", "Multiple Tabs and Newlines")]
        public void NormalizeSpacesTest(string input, string expected)
        {
            input.NormalizeSpaces().Should().Be(expected);
        }

        [Theory]
        [InlineData("Hello123", new char[] { '1', '2', '3' }, "Hello")]
        [InlineData("Test-String", new char[] { '-' }, "TestString")]
        public void RemoveCharactersTest(string input, char[] charsToRemove, string expected)
        {
            input.RemoveCharacters(charsToRemove).Should().Be(expected);
        }

        private class Person
        {
            public int Age { get; set; }
        }
    }
}
