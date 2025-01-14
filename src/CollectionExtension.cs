using System.Collections.Generic;
using Soenneker.Extensions.Enumerable;
// ReSharper disable PossibleMultipleEnumeration

namespace Soenneker.Extensions.Collection;

/// <summary>
/// Helpful Collection extension methods
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Removes all elements in the <paramref name="toRemove"/> enumerable from the specified <paramref name="collection"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to remove elements from.</param>
    /// <param name="toRemove">The enumerable of items to remove.</param>
    public static void RemoveEnumerableFromCollection<T>(this ICollection<T>? collection, IEnumerable<T>? toRemove)
    {
        // Null or empty checks for collection and toRemove
        if (collection is null || collection.Count == 0)
            return;

        if (toRemove is null)
            return;

        // Optimize for cases where toRemove is a HashSet for fast lookups
        if (toRemove is ICollection<T> {Count: 0})
            return;

        // Use HashSet to optimize repeated `Remove` operations for large `toRemove` enumerables
        HashSet<T>? removalSet = null;
        if (toRemove is not ICollection<T>)
        {
            removalSet = [..toRemove];
        }

        // Perform removal directly or using the HashSet for efficiency
        foreach (T item in toRemove)
        {
            if (removalSet is null || removalSet.Contains(item))
            {
                collection.Remove(item);
            }
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