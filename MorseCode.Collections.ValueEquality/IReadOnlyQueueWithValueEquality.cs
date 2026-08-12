using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">Specifies the type of elements in the read-only queue.</typeparam>
/// <summary>
///     Represents a read-only first-in, first-out collection of objects that is compared to other queues using value
///     equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality{T}" />
///     , each element is sequentially compared for equality.
/// </remarks>
[CollectionBuilder(
    builderType: typeof(ValueEqualityCollectionFactory.CollectionExpressionBuilders),
    methodName: nameof(ValueEqualityCollectionFactory.CollectionExpressionBuilders
        .CreateReadOnlyQueueWithValueEquality))]
public interface IReadOnlyQueueWithValueEquality<T> : IEnumerable<T>
{
    /// <summary>
    ///     Gets a value indicating whether this is the empty queue.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this queue is empty; otherwise, <c>false</c>.
    /// </value>
    bool IsEmpty { get; }

    /// <summary>
    ///     Returns the object at the beginning of the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" /> without removing it.
    /// </summary>
    /// <returns>
    ///     The object at the beginning of the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///     The
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" /> is empty.
    /// </exception>
    T Peek();

    /// <param name="result">
    ///     If present, the object at the beginning of the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />; otherwise, the default
    ///     value of <typeparamref name="T" />.
    /// </param>
    /// <summary>
    ///     Returns a value that indicates whether there is an object at the beginning of the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />, and if one is present,
    ///     copies it to the <paramref name="result" /> parameter. The object is not removed from the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />.
    /// </summary>
    /// <returns>
    ///     <see langword="true" /> if there is an object at the beginning of the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />; <see langword="false" /> if
    ///     the <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" /> is empty.
    /// </returns>
    bool TryPeek([MaybeNullWhen(false)] out T result);

    /// <param name="item">
    ///     The object to locate in the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />. The value can be
    ///     <see langword="null" /> for reference types.
    /// </param>
    /// <summary>
    ///     Determines whether an element is in the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />.
    /// </summary>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="item" /> is found in the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    bool Contains(T item);

    /// <summary>
    ///     Copies the <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" /> elements to
    ///     a new array.
    /// </summary>
    /// <returns>
    ///     A new array containing elements copied from the
    ///     <see cref="T:MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality`1" />.
    /// </returns>
    T[] ToArray();
}