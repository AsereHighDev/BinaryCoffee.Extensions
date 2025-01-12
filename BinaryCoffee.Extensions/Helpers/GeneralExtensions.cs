#nullable enable

using System.Collections.Generic;

namespace BinaryCoffee.Extensions.Helpers;

/// <summary>
/// Provides additional extension methods for collections.
/// </summary>
public static class AdditionalExtensions
{
    /// <summary>
    /// Adds a value item to a nested collection within a dictionary. If the key does not exist, a new collection is created.
    /// </summary>
    /// <typeparam name="TList">The type of the collection that holds the value items.</typeparam>
    /// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
    /// <typeparam name="TValueItem">The type of the value item to be added to the collection.</typeparam>
    /// <param name="listHash">The dictionary containing the nested collections.</param>
    /// <param name="key">The key associated with the collection to which the value item will be added.</param>
    /// <param name="valueItem">The value item to add to the collection.</param>
    public static void AddNested<TList, TKey, TValueItem>(this Dictionary<TKey, TList> listHash, TKey key, TValueItem valueItem) where TList : ICollection<TValueItem>, new()
    {
        if (listHash.TryGetValue(key, out var valueItems))
            valueItems.Add(valueItem);
        else
            listHash.Add(key, new() { valueItem });
    }
}
