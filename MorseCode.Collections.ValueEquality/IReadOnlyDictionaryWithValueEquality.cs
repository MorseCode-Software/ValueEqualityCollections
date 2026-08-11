using System.Collections.Generic;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="TKey">The type of keys in the read-only dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the read-only dictionary.</typeparam>
/// <summary>
///     Represents a generic read-only collection of key/value pairs that is compared to other dictionaries using
///     value equality.
/// </summary>
/// <remarks>
///     When compared to another
///     <see cref="MorseCode.Collections.ValueEquality.IReadOnlyDictionaryWithValueEquality{TKey,TValue}" />, each
///     key/value pair is sequentially compared for equality.
/// </remarks>
public interface IReadOnlyDictionaryWithValueEquality<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull;