using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryCoffee.Extensions;

/// <summary>
/// Provides extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Joins the elements of a sequence using a specified separator and a formatter function.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="strs">The sequence of elements to join.</param>
    /// <param name="formatter">A function to format each element.</param>
    /// <param name="separator">The string to use as a separator. Default is an empty string.</param>
    /// <returns>A string that consists of the elements in the sequence separated by the separator string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="strs"/> or <paramref name="formatter"/> is null.</exception>
    public static string Join<T>(this IEnumerable<T> strs, Func<T, string> formatter, string? separator = "")
    {
        if (strs == null) throw new ArgumentNullException(nameof(strs));
        if (formatter == null) throw new ArgumentNullException(nameof(formatter));
        return string.Join(separator, strs.Select(formatter));
    }

    /// <summary>
    /// Adds a value to a nested collection within a dictionary. If the key does not exist, a new collection is created.
    /// </summary>
    /// <typeparam name="TList">The type of the collection to store the values.</typeparam>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValueItem">The type of the values in the collection.</typeparam>
    /// <param name="listHash">The dictionary containing the nested collections.</param>
    /// <param name="key">The key associated with the collection to which the value will be added.</param>
    /// <param name="valueItem">The value to add to the collection.</param>
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