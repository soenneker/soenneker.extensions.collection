using System.Collections.Generic;
using Soenneker.Extensions.Enumerable;

namespace Soenneker.Extensions.Collection;

/// <summary>
/// Helpful Collection extension methods
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Simple foreach over toRemove list, removing each from the target. This method is safe; no exceptions will be thrown if the collection or the enumerable is null or empty.
    /// </summary>
    public static void RemoveEnumerableFromCollection<T>(this ICollection<T>? list, IEnumerable<T>? toRemove)
    {
        if (list.IsNullOrEmpty())
            return;

        if (toRemove.IsNullOrEmpty())
            return;

        foreach (T item in toRemove)
        {
            list.Remove(item);
        }
    }

    /// <inheritdoc cref="RemoveEnumerableFromCollection{T}"/>
    public static void RemoveFromCollection<T>(this ICollection<T> list, params T[] toRemove)
    {
        RemoveEnumerableFromCollection(list, toRemove);
    }
}