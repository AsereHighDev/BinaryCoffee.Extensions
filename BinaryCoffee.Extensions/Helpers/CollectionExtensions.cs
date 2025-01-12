using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryCoffee.Extensions;

public static class CollectionExtensions
{
    public static string Join<T>(this IEnumerable<T> strs, Func<T, string> formatter, string? separator = "")
    {
        if (strs == null) throw new ArgumentNullException(nameof(strs));
        if (formatter == null) throw new ArgumentNullException(nameof(formatter));
        return string.Join(separator, strs.Select(formatter));
    }

    public static void AddNested<TList, TKey, TValueItem>(this Dictionary<TKey, TList> listHash, TKey key, TValueItem valueItem)
        where TList : ICollection<TValueItem>, new()
    {
        if (listHash.TryGetValue(key, out var valueItems))
        {
            valueItems.Add(valueItem);
        }
        else
        {
            listHash.Add(key, new TList { valueItem });
        }
    }
}