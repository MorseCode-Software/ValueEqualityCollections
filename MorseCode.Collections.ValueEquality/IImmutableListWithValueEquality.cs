using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">The type of elements in the immutable list.</typeparam>
/// <summary>
///     Represents an immutable collection of elements that can be accessed by index and is compared to other lists
///     using value equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyListWithValueEquality{T}" />,
///     each element is sequentially compared for equality.
/// </remarks>
[CollectionBuilder(
    builderType: typeof(ValueEqualityCollectionFactory.CollectionExpressionBuilders),
    methodName: nameof(ValueEqualityCollectionFactory.CollectionExpressionBuilders
        .CreateImmutableListWithValueEquality))]
public interface IImmutableListWithValueEquality<T> : IReadOnlyListWithValueEquality<T>, IImmutableList<T>
{
    /// <summary>
    ///     Gets an empty list that retains the same sort semantics that this instance has.
    /// </summary>
    new IImmutableListWithValueEquality<T> Clear();

    /// <summary>
    ///     Adds the specified value to this list.
    /// </summary>
    /// <param name="value">The value to add.</param>
    /// <returns>A new list with the element added.</returns>
    new IImmutableListWithValueEquality<T> Add(T value);

    /// <summary>
    ///     Adds the specified values to this list.
    /// </summary>
    /// <param name="items">The values to add.</param>
    /// <returns>A new list with the elements added.</returns>
    new IImmutableListWithValueEquality<T> AddRange(IEnumerable<T> items);

    /// <summary>
    ///     Inserts the specified value at the specified index.
    /// </summary>
    /// <param name="index">The index at which to insert the value.</param>
    /// <param name="element">The element to insert.</param>
    /// <returns>The new immutable list.</returns>
    new IImmutableListWithValueEquality<T> Insert(int index, T element);

    /// <summary>
    ///     Inserts the specified values at the specified index.
    /// </summary>
    /// <param name="index">The index at which to insert the value.</param>
    /// <param name="items">The elements to insert.</param>
    /// <returns>The new immutable list.</returns>
    new IImmutableListWithValueEquality<T> InsertRange(int index, IEnumerable<T> items);

    /// <summary>
    ///     Removes the specified value from this list.
    /// </summary>
    /// <param name="value">The value to remove.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer to use in the search.
    ///     If <c>null</c>, <see cref="EqualityComparer{T}.Default" /> is used.
    /// </param>
    /// <returns>A new list with the element removed, or this list if the element is not in this list.</returns>
    new IImmutableListWithValueEquality<T> Remove(T value, IEqualityComparer<T>? equalityComparer);

    /// <summary>
    ///     Removes all the elements that match the conditions defined by the specified
    ///     predicate.
    /// </summary>
    /// <param name="match">
    ///     The <see cref="Predicate{T}" /> delegate that defines the conditions of the elements
    ///     to remove.
    /// </param>
    /// <returns>
    ///     The new list.
    /// </returns>
    new IImmutableListWithValueEquality<T> RemoveAll(Predicate<T> match);

    /// <summary>
    ///     Removes the specified values from this list.
    /// </summary>
    /// <param name="items">The items to remove if matches are found in this list.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer to use in the search.
    ///     If <c>null</c>, <see cref="EqualityComparer{T}.Default" /> is used.
    /// </param>
    /// <returns>
    ///     A new list with the elements removed.
    /// </returns>
    new IImmutableListWithValueEquality<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer);

    /// <summary>
    ///     Removes the specified values from this list.
    /// </summary>
    /// <param name="index">The starting index to begin removal.</param>
    /// <param name="count">The number of elements to remove.</param>
    /// <returns>
    ///     A new list with the elements removed.
    /// </returns>
    new IImmutableListWithValueEquality<T> RemoveRange(int index, int count);

    /// <summary>
    ///     Removes the element at the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>A new list with the elements removed.</returns>
    new IImmutableListWithValueEquality<T> RemoveAt(int index);

    /// <summary>
    ///     Replaces an element in the list at a given position with the specified element.
    /// </summary>
    /// <param name="index">The position in the list of the element to replace.</param>
    /// <param name="value">The element to replace the old element with.</param>
    /// <returns>The new list -- even if the value being replaced is equal to the new value for that position.</returns>
    new IImmutableListWithValueEquality<T> SetItem(int index, T value);

    /// <summary>
    ///     Replaces the first equal element in the list with the specified element.
    /// </summary>
    /// <param name="oldValue">The element to replace.</param>
    /// <param name="newValue">The element to replace the old element with.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer to use in the search.
    ///     If <c>null</c>, <see cref="EqualityComparer{T}.Default" /> is used.
    /// </param>
    /// <returns>The new list -- even if the value being replaced is equal to the new value for that position.</returns>
    /// <exception cref="ArgumentException">Thrown when the old value does not exist in the list.</exception>
    new IImmutableListWithValueEquality<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer);

    /// <summary>
    ///     Replaces the first equal element in this list with the specified element.
    /// </summary>
    /// <param name="oldValue">The element to replace.</param>
    /// <param name="newValue">The element to replace the old element with.</param>
    /// <returns>The new list -- even if the value being replaced is equal to the new value for that position.</returns>
    /// <exception cref="ArgumentException">Thrown when the old value does not exist in the list.</exception>
    IImmutableListWithValueEquality<T> Replace(T oldValue, T newValue);

    /// <summary>
    ///     Removes the specified value from this list.
    /// </summary>
    /// <param name="value">The value to remove.</param>
    /// <returns>A new list with the element removed, or this list if the element is not in this list.</returns>
    IImmutableListWithValueEquality<T> Remove(T value);

    /// <summary>
    ///     Removes the specified values from this list.
    /// </summary>
    /// <param name="items">The items to remove if matches are found in this list.</param>
    /// <returns>
    ///     A new list with the elements removed.
    /// </returns>
    IImmutableListWithValueEquality<T> RemoveRange(IEnumerable<T> items);

    /// <summary>
    ///     Searches for the specified object and returns the zero-based index of the
    ///     first occurrence within this list.
    /// </summary>
    /// <param name="item">
    ///     The object to locate in this list. The value
    ///     can be null for reference types.
    /// </param>
    /// <returns>
    ///     The zero-based index of the first occurrence of item within the range of
    ///     elements in this list that extends from index
    ///     to the last element, if found; otherwise, -1.
    /// </returns>
    int IndexOf(T item);

    /// <summary>
    ///     Searches for the specified object and returns the zero-based index of the
    ///     first occurrence within the range of elements in this list
    ///     that extends from the specified index to the last element.
    /// </summary>
    /// <param name="item">
    ///     The object to locate in this list. The value
    ///     can be null for reference types.
    /// </param>
    /// <param name="startIndex">
    ///     The zero-based starting index of the search. 0 (zero) is valid in an empty
    ///     list.
    /// </param>
    /// <returns>
    ///     The zero-based index of the first occurrence of item within the range of
    ///     elements in this list that extends from index
    ///     to the last element, if found; otherwise, -1.
    /// </returns>
    int IndexOf(T item, int startIndex);

    /// <summary>
    ///     Searches for the specified object and returns the zero-based index of the
    ///     first occurrence within the range of elements in this list
    ///     that extends from the specified index to the last element.
    /// </summary>
    /// <param name="item">
    ///     The object to locate in this list. The value
    ///     can be null for reference types.
    /// </param>
    /// <param name="startIndex">
    ///     The zero-based starting index of the search. 0 (zero) is valid in an empty
    ///     list.
    /// </param>
    /// <param name="count">
    ///     The number of elements in the section to search.
    /// </param>
    /// <returns>
    ///     The zero-based index of the first occurrence of item within the range of
    ///     elements in this list that extends from index
    ///     to the last element, if found; otherwise, -1.
    /// </returns>
    int IndexOf(T item, int startIndex, int count);

    /// <summary>
    ///     Searches for the specified object and returns the zero-based index of the
    ///     last occurrence within this entire list.
    /// </summary>
    /// <param name="item">
    ///     The object to locate in this list. The value
    ///     can be null for reference types.
    /// </param>
    /// <returns>
    ///     The zero-based index of the last occurrence of item within this entire
    ///     list, if found; otherwise, -1.
    /// </returns>
    int LastIndexOf(T item);

    /// <summary>
    ///     Searches for the specified object and returns the zero-based index of the
    ///     last occurrence within the range of elements in this list
    ///     that extends from the first element to the specified index.
    /// </summary>
    /// <param name="item">
    ///     The object to locate in this list. The value
    ///     can be null for reference types.
    /// </param>
    /// <param name="startIndex">
    ///     The zero-based starting index of the backward search.
    /// </param>
    /// <returns>
    ///     The zero-based index of the last occurrence of item within the range of elements
    ///     in this list that extends from the first element
    ///     to index, if found; otherwise, -1.
    /// </returns>
    int LastIndexOf(T item, int startIndex);

    /// <summary>
    ///     Searches for the specified object and returns the zero-based index of the
    ///     last occurrence within the range of elements in this list
    ///     that extends from the first element to the specified index.
    /// </summary>
    /// <param name="item">
    ///     The object to locate in this list. The value
    ///     can be null for reference types.
    /// </param>
    /// <param name="startIndex">
    ///     The zero-based starting index of the backward search.
    /// </param>
    /// <param name="count">
    ///     The number of elements in the section to search.
    /// </param>
    /// <returns>
    ///     The zero-based index of the last occurrence of item within the range of elements
    ///     in this list that extends from the first element
    ///     to index, if found; otherwise, -1.
    /// </returns>
    int LastIndexOf(T item, int startIndex, int count);
}