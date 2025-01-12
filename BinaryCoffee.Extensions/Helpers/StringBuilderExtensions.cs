using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryCoffee.Extensions.Helpers;

public static class StringBuilderExtensions
{
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
        strs.Select(formatter).Aggregate(builder, (sb, s) => sb.Insert(startIndex, separator).Insert(startIndex += separator.Length, (startIndex += s?.Length ?? 0, s).s));

    public static StringBuilder InsertJoin<T>(this StringBuilder builder, int startIndex, IEnumerable<T> strs, string separator = "") =>
        strs.Select(a => a?.ToString()).Aggregate(builder, (sb, s) => sb.Insert(startIndex, separator).Insert(startIndex += separator.Length, (startIndex += s.Length, s).s));
}

[JetBrains.Annotations.StringFormatMethod("format")]
public delegate StringBuilder StringFormatter(string format, params object?[] arguments);