using System;
using System.Globalization;

namespace BinaryCoffee.Extensions;

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
}