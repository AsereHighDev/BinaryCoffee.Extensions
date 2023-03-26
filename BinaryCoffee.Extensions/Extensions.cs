#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace BinaryCoffee.Extensions
{
    public static class Helpers
    {
        public static string Join<T>(this IEnumerable<T> strs, Func<T, string> formmater, string? separator = "")
        {
            return string.Join(separator, strs.Select(formmater));
        }

        public static StringBuilder AppendIf(this StringBuilder builder, bool condition, Func<object?> getter)
        {
            if (condition)
                builder.Append(getter());
            return builder;
        }



        public static StringBuilder AppendFormatIf(this StringBuilder builder, bool condition, Func<StringFormatter, StringBuilder> ifAppend, Func<StringFormatter, StringBuilder>? elseAppend = null)
        {
            if (condition)
                return ifAppend(builder.AppendFormat);
            return elseAppend?.Invoke(builder.AppendFormat) ?? builder;
        }

        public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, Func<string?> getter)
        {
            if (condition)
                builder.AppendLine(getter());
            return builder;
        }
        public static StringBuilder InsertJoin<T>(this StringBuilder builder, int startIndex, IEnumerable<T> strs, Func<T, string?> formatter, string separator = "") =>
            strs.Select(formatter)
                .Aggregate(builder, (sb, s) => sb
                    .Insert(startIndex, separator)
                    .Insert(startIndex += separator.Length, (startIndex += s?.Length ?? 0, s).s));

        public static StringBuilder InsertJoin<T>(this StringBuilder builder, int startIndex, IEnumerable<T> strs, string separator = "") =>
            strs.Select(a => a?.ToString())
                .Aggregate(builder, (sb, s) => sb
                    .Insert(startIndex, separator)
                    .Insert(startIndex += separator.Length, (startIndex += s.Length, s).s));

        public static string Join<T>(this IEnumerable<T> strs, string? separator = "")
        {
            return strs.Join(t => t?.ToString() ?? "", separator);
        }

        public static void AddNested<TList, TKey, TValueItem>(this Dictionary<TKey, TList> listHash, TKey key, TValueItem valueItem)
            where TList : ICollection<TValueItem>, new()
        {
            if (listHash.TryGetValue(key, out var valueItems))
                valueItems.Add(valueItem);
            else
                listHash.Add(key, new() { valueItem });
        }

        public static T[] ArrayFrom<T>(params T[] items) => items;

        public static string ToCamelCase(this string name)
        {
            Span<char> buffer = stackalloc char[name.Length];
            int index = 0;
            bool needUpper = true;
            char lastChar = char.MinValue;

            foreach (char ch in name)
            {
                if (char.IsDigit(ch))
                {
                    if (index == 0)
                    {
                        buffer = stackalloc char[buffer.Length + 1];                        
                        buffer[index++] = '_';
                    }

                    buffer[index++] = ch;
                    needUpper = true;
                }
                else if (!char.IsLetterOrDigit(ch))
                    needUpper = true;
                else if (index == 0 && char.IsUpper(ch))
                {
                    buffer[index++] = char.ToLower(ch);
                    needUpper = false;
                }
                else if (index > 0 && (needUpper || char.IsLower(lastChar) && char.IsUpper(ch)))
                {
                    buffer[index++] = char.ToUpper(ch);
                    needUpper = false;
                }
                else
                {
                    buffer[index++] = ch;
                    needUpper = false;
                }

                lastChar = ch;
            }

            return new(buffer[..index]);
        }

        public static string ToPascalCase(this string name)
        {
            Span<char> buffer = stackalloc char[name.Length];
            int index = 0;
            bool needUpper = true;
            char lastChar = char.MinValue;

            foreach (char ch in name)
            {
                if (char.IsDigit(ch))
                {
                    if (index == 0)
                    {
                        buffer = stackalloc char[buffer.Length + 1];
                        buffer[index++] = '_';
                    }
                    buffer[index++] = ch;
                    needUpper = true;
                }
                else if (char.IsSeparator(ch) || ch is '_' or '-' or '~')
                    needUpper = true;
                else if (needUpper || char.IsLower(lastChar) && char.IsUpper(ch))
                {
                    buffer[index++] = char.ToUpperInvariant(ch);
                    needUpper = false;
                }
                else
                {
                    buffer[index++] = ch;
                    needUpper = false;
                }

                lastChar = ch;
            }

            return new string(buffer[..index]);
        }

        public static string ToSnakeLowerCase(this string name)
        {
            Span<char> buffer = stackalloc char[name.Length * 2];
            int index = 0;
            bool start = false;
            char lastChar = char.MinValue;

            for (int current = 0; current < name.Length; current++)
            {
                char ch = name[current];
                bool isUpper = char.IsUpper(ch), isDigit = char.IsDigit(ch), isSeparator = !char.IsLetterOrDigit(ch);

                if (isSeparator || (char.IsLower(lastChar) && isUpper) || isDigit)
                {
                    if (!start)
                    {
                        if (isSeparator)
                            continue;

                        if (isDigit)
                            buffer = stackalloc char[buffer.Length + 1];
                    }

                    if (char.IsLetterOrDigit(lastChar) && isSeparator || char.IsLower(lastChar) && isUpper)
                        buffer[index++] = '_';
                }

                if (!isSeparator)
                    buffer[index++] = char.ToLowerInvariant(ch);

                if (!start)
                    start = true;

                lastChar = ch;
            }

            return new string(buffer[..index]);
        }

        public static string ToSnakeUpperCase(this string name)
        {
            Span<char> buffer = stackalloc char[name.Length * 2];
            int index = 0;
            bool start = false;
            char lastChar = char.MinValue;

            for (int current = 0; current < name.Length; current++)
            {
                char ch = name[current];
                bool isUpper = char.IsUpper(ch), isDigit = char.IsDigit(ch), isSeparator = !char.IsLetterOrDigit(ch);

                if (isSeparator || (char.IsLower(lastChar) && isUpper) || isDigit)
                {
                    if (!start)
                    {
                        if (isSeparator)
                            continue;
                        if (isDigit)
                            buffer = stackalloc char[buffer.Length + 1];
                    }

                    if (char.IsLetterOrDigit(lastChar) && isSeparator || char.IsLower(lastChar) && isUpper)
                        buffer[index++] = '_';
                }

                if (!isSeparator)
                    buffer[index++] = char.ToUpperInvariant(ch);

                if (!start)
                    start = true;

                lastChar = ch;
            }

            return new string(buffer[..index]);
        }

        public static string ToCamelCase(this Enum value) => value.ToString().ToCamelCase();

        public static string ToPascalCase(this Enum value) => value.ToString().ToPascalCase();

        public static string ToLowerSnakeCase(this Enum value) => value.ToString().ToSnakeLowerCase();

        public static string ToUpperSnakeCase(this Enum value) => value.ToString().ToSnakeUpperCase();

    }

    [JetBrains.Annotations.StringFormatMethod("format")]
    public delegate StringBuilder StringFormatter(string format, params object?[] arguments);

}
