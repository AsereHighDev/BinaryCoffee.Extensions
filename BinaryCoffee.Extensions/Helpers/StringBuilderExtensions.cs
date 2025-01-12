using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryCoffee.Extensions.Helpers;

public static class StringBuilderExtensions
{
    /// <summary>
/// Appends a formatted string to the StringBuilder if the specified condition is true.
/// </summary>
/// <param name="builder">The StringBuilder to append to.</param>
/// <param name="condition">The condition to evaluate.</param>
/// <param name="ifAppend">The function to execute if the condition is true.</param>
/// <param name="elseAppend">The function to execute if the condition is false (optional).</param>
/// <returns>The updated StringBuilder.</returns>
public static StringBuilder AppendFormatIf(this StringBuilder builder, bool condition, Func<StringFormatter, StringBuilder> ifAppend, Func<StringFormatter, StringBuilder>? elseAppend = null)
{
    if (condition)
        return ifAppend(builder.AppendFormat);
    
    return elseAppend?.Invoke(builder.AppendFormat) ?? builder;
}

/// <summary>
/// Appends a line to the StringBuilder if the specified condition is true.
/// </summary>
/// <param name="builder">The StringBuilder to append to.</param>
/// <param name="condition">The condition to evaluate.</param>
/// <param name="getter">The function to get the string to append if the condition is true.</param>
/// <returns>The updated StringBuilder.</returns>
public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, Func<string?> getter)
{
    if (condition)
        builder.AppendLine(getter());
    
    return builder;
}

/// <summary>
/// Inserts a formatted collection of strings into the StringBuilder at the specified index.
/// </summary>
/// <typeparam name="T">The type of the elements in the collection.</typeparam>
/// <param name="builder">The StringBuilder to insert into.</param>
/// <param name="startIndex">The starting index to insert at.</param>
/// <param name="strs">The collection of strings to insert.</param>
/// <param name="formatter">The function to format each element.</param>
/// <param name="separator">The separator to use between elements (optional).</param>
/// <returns>The updated StringBuilder.</returns>
public static StringBuilder InsertJoin<T>(this StringBuilder builder, int startIndex, IEnumerable<T> strs, Func<T, string?> formatter, string separator = "") =>
    strs.Select(formatter).Aggregate(builder, (sb, s) => sb.Insert(startIndex, separator).Insert(startIndex += separator.Length, (startIndex += s?.Length ?? 0, s).s));

/// <summary>
/// Inserts a collection of strings into the StringBuilder at the specified index.
/// </summary>
/// <typeparam name="T">The type of the elements in the collection.</typeparam>
/// <param name="builder">The StringBuilder to insert into.</param>
/// <param name="startIndex">The starting index to insert at.</param>
/// <param name="strs">The collection of strings to insert.</param>
/// <param name="separator">The separator to use between elements (optional).</param>
/// <returns>The updated StringBuilder.</returns>
public static StringBuilder InsertJoin<T>(this StringBuilder builder, int startIndex, IEnumerable<T> strs, string separator = "") =>
    strs.Select(a => a?.ToString()).Aggregate(builder, (sb, s) => sb.Insert(startIndex, separator).Insert(startIndex += separator.Length, (startIndex += s.Length, s).s));
}

[JetBrains.Annotations.StringFormatMethod("format")]
public delegate StringBuilder StringFormatter(string format, params object?[] arguments);