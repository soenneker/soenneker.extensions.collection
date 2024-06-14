using System.Collections.Generic;
using Soenneker.Extensions.Enumerable;

namespace Soenneker.Extensions.Collection;

/// <summary>
/// Helpful Collection extension methods
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Simple foreach over toRemove collection, removing each from the target. This method is safe; no exceptions will be thrown if the collection or the enumerable is null or empty.
    /// </summary>
    public static void RemoveEnumerableFromCollection<T>(this ICollection<T>? collection, IEnumerable<T>? toRemove)
    {
        if (collection.IsNullOrEmpty())
            return;

        if (toRemove.IsNullOrEmpty())
            return;

        foreach (T item in toRemove)
        {
            collection.Remove(item);
        }
    }

    /// <inheritdoc cref="RemoveEnumerableFromCollection{T}"/>
    public static void RemoveFromCollection<T>(this ICollection<T> collection, params T[] toRemove)
    {
        RemoveEnumerableFromCollection(collection, toRemove);
    }

    /// <summary>
    /// Adds an item to the collection if the item is not null.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to add the item to.</param>
    /// <param name="item">The item to add to the collection if it is not null.</param>
    public static void AddIfNotNull<T>(this ICollection<T> collection, T? item)
    {
        if (item == null)
            return;

        collection.Add(item);
    }
}