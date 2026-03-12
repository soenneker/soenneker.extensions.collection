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
    /// <remarks>This method efficiently removes all items in the specified sequence from the target
    /// collection, using optimized strategies for common collection types and sizes. The operation modifies the input
    /// collection in place. Duplicate elements in the sequence to remove are handled efficiently, and only existing
    /// items in the collection are affected. No exception is thrown if an element to remove does not exist in the
    /// collection.</remarks>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection from which elements will be removed. If null or empty, no action is taken.</param>
    /// <param name="toRemove">The sequence of elements to remove from the collection. If null or empty, no elements are removed.</param>
    /// <param name="comparer">An optional equality comparer to use for determining element equality. If null, the default equality comparer
    /// for the type is used.</param>
    public static void RemoveEnumerableFromCollection<T>(this ICollection<T>? collection, IEnumerable<T>? toRemove, IEqualityComparer<T>? comparer = null)
    {
        if (collection is null || collection.Count == 0 || toRemove is null)
            return;

        if (toRemove is ICollection<T> trc && trc.Count == 0)
            return;

        // Fast-path: target is HashSet<T>
        if (collection is HashSet<T> targetSet)
        {
            if (toRemove is HashSet<T> hs)
                targetSet.ExceptWith(hs);
            else
                targetSet.ExceptWith(toRemove);

            return;
        }

        // Fast-path: target is List<T>
        if (collection is List<T> list)
        {
            HashSet<T> set = toRemove as HashSet<T> ?? new HashSet<T>(toRemove, comparer);
            if (set.Count == 0)
                return;

            list.RemoveAll(static (item, state) => ((HashSet<T>)state!).Contains(item), set);
            return;
        }

        // If already deduped, remove directly
        if (toRemove is HashSet<T> hs2)
        {
            foreach (T item in hs2)
                collection.Remove(item);

            return;
        }

        // Small remove set: no extra alloc
        if (toRemove is ICollection<T> c && c.Count <= 32)
        {
            foreach (T item in toRemove)
                collection.Remove(item);

            return;
        }

        // Dedupe to reduce repeated Remove attempts
        var removalSet = new HashSet<T>(toRemove, comparer);

        foreach (T item in removalSet)
            collection.Remove(item);
    }

    /// <inheritdoc cref="RemoveEnumerableFromCollection{T}"/>
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