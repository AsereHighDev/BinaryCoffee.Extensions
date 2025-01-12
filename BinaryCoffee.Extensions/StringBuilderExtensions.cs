using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryCoffee.Extensions;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendIf(this StringBuilder builder, bool condition, Func<object?> getter)
    {
        if (condition)
            builder.Append(getter());
        
        return builder;
    }

    public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, Func<string?> getter)
    {
        if (condition)
            builder.AppendLine(getter());
        
        return builder;
    }

    public static StringBuilder InsertJoin<T>(this StringBuilder builder, int startIndex, IEnumerable<T> strs, Func<T, string?> formatter, string separator = "")
    {
        foreach (var str in strs.Select(formatter))
        {
            builder.Insert(startIndex, str + separator);
        }
        
        return builder;
    }
}