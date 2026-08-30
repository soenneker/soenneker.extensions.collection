using Soenneker.Extensions.List;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Extensions.Collection;

/// <summary>
/// Helpful Collection extension methods
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Removes all elements in the specified sequence from the target collection.
    /// </summary>
    /// <remarks>The operation modifies the collection in place and removes every matching occurrence. List and hash-set targets use
    /// optimized removal paths. Other collection types are enumerated before removal so they are not modified during enumeration.</remarks>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection from which elements will be removed. If null or empty, no action is taken.</param>
    /// <param name="toRemove">The sequence of elements to remove from the collection. If null or empty, no elements are removed.</param>
    /// <param name="comparer">An optional equality comparer. When omitted, a hash-set target's comparer is used; other collections use
    /// <see cref="EqualityComparer{T}.Default"/>.</param>
    public static void RemoveEnumerableFromCollection<T>(this ICollection<T>? collection, IEnumerable<T>? toRemove, IEqualityComparer<T>? comparer = null)
    {
        if (collection is null || collection.Count == 0 || toRemove is null)
            return;

        IEqualityComparer<T> effectiveComparer = comparer ?? (collection as HashSet<T>)?.Comparer ?? EqualityComparer<T>.Default;
        var removalSet = new HashSet<T>(toRemove, effectiveComparer);

        if (removalSet.Count == 0)
            return;

        if (collection is List<T> list)
        {
            list.RemoveAll(static (item, state) => ((HashSet<T>)state!).Contains(item), removalSet);
            return;
        }

        if (collection is HashSet<T> targetSet)
        {
            targetSet.RemoveWhere(removalSet.Contains);
            return;
        }

        var matches = new List<T>();
        foreach (T item in collection)
        {
            if (removalSet.Contains(item))
                matches.Add(item);
        }

        foreach (T item in matches)
            collection.Remove(item);
    }

    /// <summary>
    /// Removes every supplied item from the collection using the collection's equality semantics.
    /// </summary>
    /// <typeparam name="T">The sequence element or result type.</typeparam>
    /// <param name="collection">The collection to mutate.</param>
    /// <param name="toRemove">The items to remove; absent items are ignored.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddIfNotNull<T>(this ICollection<T> collection, T? item)
    {
        if (item is null)
            return;

        collection.Add(item);
    }
}
