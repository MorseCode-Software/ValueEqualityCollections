using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="TKey">The type of keys in the immutable dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the immutable dictionary.</typeparam>
/// <summary>
///     Represents a generic immutable collection of key/value pairs that is compared to other dictionaries using
///     value equality.
/// </summary>
/// <remarks>
///     When compared to another
///     <see cref="MorseCode.Collections.ValueEquality.IReadOnlyDictionaryWithValueEquality{TKey,TValue}" />, each
///     key/value pair is sequentially compared for equality.
/// </remarks>
public interface IImmutableDictionaryWithValueEquality<TKey, TValue>
    : IReadOnlyDictionaryWithValueEquality<TKey, TValue>, IImmutableDictionary<TKey, TValue> where TKey : notnull
{
    /// <summary>
    ///     Gets an empty dictionary with equivalent ordering and key/value comparison rules.
    /// </summary>
    new IImmutableDictionaryWithValueEquality<TKey, TValue> Clear();

    /// <summary>
    ///     Adds the specified key and value to the dictionary.
    /// </summary>
    /// <param name="key">The key of the entry to add.</param>
    /// <param name="value">The value of the entry to add.</param>
    /// <returns>The new dictionary containing the additional key-value pair.</returns>
    /// <exception cref="ArgumentException">
    ///     Thrown when the given key already exists in the dictionary but has a different
    ///     value.
    /// </exception>
    /// <remarks>
    ///     If the given key-value pair are already in the dictionary, the existing instance is returned.
    /// </remarks>
    new IImmutableDictionaryWithValueEquality<TKey, TValue> Add(TKey key, TValue value);

    /// <summary>
    ///     Adds the specified key-value pairs to the dictionary.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <returns>The new dictionary containing the additional key-value pairs.</returns>
    /// <exception cref="ArgumentException">
    ///     Thrown when one of the given keys already exists in the dictionary but has a
    ///     different value.
    /// </exception>
    new IImmutableDictionaryWithValueEquality<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

    /// <summary>
    ///     Sets the specified key and value to the dictionary, possibly overwriting an existing value for the given key.
    /// </summary>
    /// <param name="key">The key of the entry to add.</param>
    /// <param name="value">The value of the entry to add.</param>
    /// <returns>The new dictionary containing the additional key-value pair.</returns>
    /// <remarks>
    ///     If the given key-value pair are already in the dictionary, the existing instance is returned.
    ///     If the key already exists but with a different value, a new instance with the overwritten value will be returned.
    /// </remarks>
    new IImmutableDictionaryWithValueEquality<TKey, TValue> SetItem(TKey key, TValue value);

    /// <summary>
    ///     Applies a given set of key=value pairs to an immutable dictionary, replacing any conflicting keys in the resulting
    ///     dictionary.
    /// </summary>
    /// <param name="items">
    ///     The key=value pairs to set on the dictionary.  Any keys that conflict with existing keys will
    ///     overwrite the previous values.
    /// </param>
    /// <returns>An immutable dictionary.</returns>
    new IImmutableDictionaryWithValueEquality<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

    /// <summary>
    ///     Removes the specified keys from the dictionary with their associated values.
    /// </summary>
    /// <param name="keys">The keys to remove.</param>
    /// <returns>A new dictionary with those keys removed; or this instance if those keys are not in the dictionary.</returns>
    new IImmutableDictionaryWithValueEquality<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

    /// <summary>
    ///     Removes the specified key from the dictionary with its associated value.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    /// <returns>A new dictionary with the matching entry removed; or this instance if the key is not in the dictionary.</returns>
    new IImmutableDictionaryWithValueEquality<TKey, TValue> Remove(TKey key);
}