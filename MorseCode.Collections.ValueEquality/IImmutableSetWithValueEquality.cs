using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">The type of elements in the immutable set.</typeparam>
/// <summary>Provides an immutable abstraction of a set that is compared to other sets using value equality.</summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlySetWithValueEquality{T}" />,
///     each element is tested for equality to ensure that the set of items in both sets are the same.
/// </remarks>
public interface IImmutableSetWithValueEquality<T> : IReadOnlySetWithValueEquality<T>, IImmutableSet<T>
{
    /// <summary>
    ///     Determines if the set contains a specific item
    /// </summary>
    /// <param name="item">The item to check if the set contains.</param>
    /// <returns><see langword="true" /> if found; otherwise <see langword="false" />.</returns>
    new bool Contains(T item);

    /// <summary>
    ///     Determines whether the current set is a proper (strict) subset of a specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current set.</param>
    /// <returns><see langword="true" /> if the current set is a proper subset of other; otherwise <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">other is <see langword="null" />.</exception>
    new bool IsProperSubsetOf(IEnumerable<T> other);

    /// <summary>
    ///     Determines whether the current set is a proper (strict) superset of a specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current set.</param>
    /// <returns><see langword="true" /> if the collection is a proper superset of other; otherwise <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">other is <see langword="null" />.</exception>
    new bool IsProperSupersetOf(IEnumerable<T> other);

    /// <summary>
    ///     Determine whether the current set is a subset of a specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current set.</param>
    /// <returns><see langword="true" /> if the current set is a subset of other; otherwise <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">other is <see langword="null" />.</exception>
    new bool IsSubsetOf(IEnumerable<T> other);

    /// <summary>
    ///     Determine whether the current set is a super set of a specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current set</param>
    /// <returns><see langword="true" /> if the current set is a subset of other; otherwise <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">other is <see langword="null" />.</exception>
    new bool IsSupersetOf(IEnumerable<T> other);

    /// <summary>
    ///     Determines whether the current set overlaps with the specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current set.</param>
    /// <returns>
    ///     <see langword="true" /> if the current set and other share at least one common element; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException">other is <see langword="null" />.</exception>
    new bool Overlaps(IEnumerable<T> other);

    /// <summary>
    ///     Determines whether the current set and the specified collection contain the same elements.
    /// </summary>
    /// <param name="other">The collection to compare to the current set.</param>
    /// <returns><see langword="true" /> if the current set is equal to other; otherwise, <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">other is <see langword="null" />.</exception>
    new bool SetEquals(IEnumerable<T> other);

    /// <summary>
    ///     Gets an empty set that retains the same sort or unordered semantics that this instance has.
    /// </summary>
    new IImmutableSetWithValueEquality<T> Clear();

    /// <summary>
    ///     Adds the specified value to this set.
    /// </summary>
    /// <param name="value">The value to add.</param>
    /// <returns>A new set with the element added, or this set if the element is already in this set.</returns>
    new IImmutableSetWithValueEquality<T> Add(T value);

    /// <summary>
    ///     Removes the specified value from this set.
    /// </summary>
    /// <param name="value">The value to remove.</param>
    /// <returns>A new set with the element removed, or this set if the element is not in this set.</returns>
    new IImmutableSetWithValueEquality<T> Remove(T value);

    /// <summary>
    ///     Produces a set that contains elements that exist in both this set and the specified set.
    /// </summary>
    /// <param name="other">The set to intersect with this one.</param>
    /// <returns>A new set that contains any elements that exist in both sets.</returns>
    new IImmutableSetWithValueEquality<T> Intersect(IEnumerable<T> other);

    /// <summary>
    ///     Removes a given set of items from this set.
    /// </summary>
    /// <param name="other">The items to remove from this set.</param>
    /// <returns>The new set with the items removed; or the original set if none of the items were in the set.</returns>
    new IImmutableSetWithValueEquality<T> Except(IEnumerable<T> other);

    /// <summary>
    ///     Produces a set that contains elements either in this set or a given sequence, but not both.
    /// </summary>
    /// <param name="other">The other sequence of items.</param>
    /// <returns>The new set.</returns>
    new IImmutableSetWithValueEquality<T> SymmetricExcept(IEnumerable<T> other);

    /// <summary>
    ///     Adds a given set of items to this set.
    /// </summary>
    /// <param name="other">The items to add.</param>
    /// <returns>The new set with the items added; or the original set if all the items were already in the set.</returns>
    new IImmutableSetWithValueEquality<T> Union(IEnumerable<T> other);
}