using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace BinaryCoffee.Extensions.Helpers
{
    public static class AsyncExtensions
    {
        /// <summary>
        /// Asynchronously joins the elements of an IAsyncEnumerable&lt;T&gt; into a single string, 
        /// using a specified formatter function and an optional separator.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence of elements to join.</param>
        /// <param name="formatter">A function that formats each element into a string asynchronously.</param>
        /// <param name="separator">An optional string separator to insert between each formatted element. Default is an empty string.</param>
        /// <returns>A task that represents the asynchronous operation, with a string that contains the joined elements as its result.</returns>
        public static async Task<string> JoinAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<string>> formatter, string separator = "")
        {
            var builder = new StringBuilder();
            await foreach (var item in source)
            {
                builder.Append(await formatter(item));
                builder.Append(separator);
            }

            return builder.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// Converts a string to CamelCase format asynchronously.
        /// </summary>
        /// <param name="name">The string to convert.</param>
        /// <returns>A task that represents the asynchronous operation, with a string in CamelCase format as its result.</returns>
        public static async Task<string> ToCamelCaseAsync(this string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            return await Task.Run(() =>
            {
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
            });
        }

        /// <summary>
        /// Converts a string to PascalCase format asynchronously.
        /// </summary>
        /// <param name="name">The string to convert.</param>
        /// <returns>A task that represents the asynchronous operation, with a string in PascalCase format as its result.</returns>
        public static async Task<string> ToPascalCaseAsync(this string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            return await Task.Run(() =>
            {
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
            });
        }

        /// <summary>
        /// Converts a string to Title Case format using an optional culture asynchronously.
        /// </summary>
        /// <param name="name">The string to convert.</param>
        /// <param name="culture">The culture to use for formatting. If not specified, the invariant culture is used.</param>
        /// <returns>A task that represents the asynchronous operation, with a string in Title Case format as its result.</returns>
        public static async Task<string> ToTitleCaseAsync(this string name, CultureInfo? culture = null)
        {
            if (string.IsNullOrEmpty(name)) return name;

            culture ??= CultureInfo.InvariantCulture;
            return await Task.Run(() => culture.TextInfo.ToTitleCase(name.ToLower(culture)));
        }
    }
}