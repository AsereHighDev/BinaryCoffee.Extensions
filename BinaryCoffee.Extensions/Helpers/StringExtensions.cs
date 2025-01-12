using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace BinaryCoffee.Extensions.Helpers;

public static class StringExtensions
{
    /// <summary>
    /// Converts a string to CamelCase format.
    /// </summary>
    /// <param name="name">The string to convert.</param>
    /// <returns>A string in CamelCase format.</returns>
    public static string ToCamelCase(this string name)
    {
        if (string.IsNullOrEmpty(name)) return name;

        Span<char> buffer = stackalloc char[name.Length];
        int index = 0;
        bool needUpper = true;
        char lastChar = char.MinValue;

        foreach (char ch in name)
        {
            if (char.IsWhiteSpace(ch) || char.IsSeparator(ch))
            {
                needUpper = true;
                continue;
            }

            if (needUpper)
            {
                buffer[index++] = char.ToUpperInvariant(ch);
                needUpper = false;
            }
            else
            {
                buffer[index++] = char.ToLowerInvariant(ch);
            }

            lastChar = ch;
        }

        return new string(buffer[..index]);
    }

    /// <summary>
    /// Converts a string to PascalCase format.
    /// </summary>
    /// <param name="name">The string to convert.</param>
    /// <returns>A string in PascalCase format.</returns>
    public static string ToPascalCase(this string name)
    {
        if (string.IsNullOrEmpty(name)) return name;

        Span<char> buffer = stackalloc char[name.Length];
        int index = 0;
        bool capitalize = true;

        foreach (var ch in name)
        {
            if (char.IsWhiteSpace(ch) || char.IsSeparator(ch))
            {
                capitalize = true;
                continue;
            }

            buffer[index++] = capitalize ? char.ToUpperInvariant(ch) : char.ToLowerInvariant(ch);
            capitalize = false;
        }

        return new string(buffer[..index]);
    }

    /// <summary>
    /// Converts a string to Snake Case in lowercase.
    /// </summary>
    /// <param name="name">The string to convert.</param>
    /// <returns>A string in lowercase Snake Case format.</returns>
    public static string ToSnakeLowerCase(this string name)
    {
        if (string.IsNullOrEmpty(name)) return name;

        Span<char> buffer = stackalloc char[name.Length * 2];
        int index = 0;
        bool start = false;
        char lastChar = char.MinValue;

        foreach (char ch in name)
        {
            if (char.IsUpper(ch) && start && char.IsLower(lastChar))
            {
                buffer[index++] = '_';
            }

            buffer[index++] = char.ToLowerInvariant(ch);
            start = true;
            lastChar = ch;
        }

        return new string(buffer[..index]);
    }

    /// <summary>
    /// Converts a string to Snake Case in uppercase.
    /// </summary>
    /// <param name="name">The string to convert.</param>
    /// <returns>A string in uppercase Snake Case format.</returns>
    public static string ToSnakeUpperCase(this string name)
    {
        if (string.IsNullOrEmpty(name)) return name;

        Span<char> buffer = stackalloc char[name.Length * 2];
        int index = 0;
        bool start = false;
        char lastChar = char.MinValue;

        foreach (char ch in name)
        {
            if (char.IsUpper(ch) && start && char.IsLower(lastChar))
            {
                buffer[index++] = '_';
            }

            buffer[index++] = char.ToUpperInvariant(ch);
            start = true;
            lastChar = ch;
        }

        return new string(buffer[..index]);
    }

    /// <summary>
    /// Converts a string to Title Case format using an optional culture.
    /// </summary>
    /// <param name="name">The string to convert.</param>
    /// <param name="culture">The culture to use for formatting. If not specified, the invariant culture is used.</param>
    /// <returns>A string in Title Case format.</returns>
    public static string ToTitleCase(this string name, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.InvariantCulture;
        return culture.TextInfo.ToTitleCase(name.ToLower(culture));
    }
    
    /// <summary>
        /// Checks if the string matches the given regular expression pattern.
        /// </summary>
        /// <param name="input">The input string to check.</param>
        /// <param name="pattern">The regex pattern to match.</param>
        /// <returns>True if the string matches the pattern, otherwise false.</returns>
        public static bool MatchesPattern(this string input, string pattern)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrEmpty(pattern)) throw new ArgumentNullException(nameof(pattern));

            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// Removes unwanted characters from the string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="characters">Characters to remove.</param>
        /// <returns>The string without the specified characters.</returns>
        public static string RemoveCharacters(this string input, params char[] characters)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return new string(input.Where(c => !characters.Contains(c)).ToArray());
        }

        /// <summary>
        /// Reverses the input string.
        /// </summary>
        /// <param name="input">The string to reverse.</param>
        /// <returns>The reversed string.</returns>
        public static string Reverse(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return new string(input.ToCharArray().Reverse().ToArray());
        }

        /// <summary>
        /// Converts a string to a URL-friendly slug format.
        /// </summary>
        /// <param name="input">The string to slugify.</param>
        /// <returns>A URL-friendly slug.</returns>
        public static string ToSlug(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            string result = input.ToLowerInvariant();
            result = Regex.Replace(result, "[^a-z0-9\\s-]", ""); // Remove invalid chars
            result = Regex.Replace(result, "\\s+", "-").Trim('-'); // Replace spaces with dashes

            return result;
        }

        /// <summary>
        /// Capitalizes the first letter of each word in the string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The string with each word capitalized.</returns>
        public static string CapitalizeWords(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return string.Join(" ", input.Split(' ').Select(word => 
                char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }

        /// <summary>
        /// Normalizes spaces by removing extra spaces and trimming the string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The normalized string.</returns>
        public static string NormalizeSpaces(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return Regex.Replace(input.Trim(), "\\s+", " ");
        }

        /// <summary>
        /// Strips HTML or XML tags from the string.
        /// </summary>
        /// <param name="input">The input string containing tags.</param>
        /// <returns>The string without tags.</returns>
        public static string StripTags(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    
    public static T[] ArrayFrom<T>(params T[] items) => items;
        
    public static string ToCamelCase(this Enum value) => value.ToString().ToCamelCase();

    public static string ToPascalCase(this Enum value) => value.ToString().ToPascalCase();

    public static string ToLowerSnakeCase(this Enum value) => value.ToString().ToSnakeLowerCase();

    public static string ToUpperSnakeCase(this Enum value) => value.ToString().ToSnakeUpperCase();
    
    public static string Join<T>(this IEnumerable<T> strs, string? separator = "") => StringExtensions.Join(strs, t => t?.ToString() ?? "", separator);
    
    public static string Join<T>(this IEnumerable<T> strs, Func<T, string> formmater, string? separator = "") => string.Join(separator, strs.Select(formmater));
}