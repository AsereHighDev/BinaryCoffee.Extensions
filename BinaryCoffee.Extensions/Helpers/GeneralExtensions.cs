#nullable enable

using System.Collections.Generic;

namespace BinaryCoffee.Extensions.Helpers;

public static class AdditionalExtensions
{
    public static void AddNested<TList, TKey, TValueItem>(this Dictionary<TKey, TList> listHash, TKey key, TValueItem valueItem) where TList : ICollection<TValueItem>, new()
    {
        if (listHash.TryGetValue(key, out var valueItems))
            valueItems.Add(valueItem);
        else
            listHash.Add(key, new() { valueItem });
    }
}
