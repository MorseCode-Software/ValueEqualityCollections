using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">Specifies the type of elements in the immutable queue.</typeparam>
/// <summary>
///     Represents an immutable first-in, first-out collection of objects that is compared to other queues using value
///     equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality{T}" />
///     , each element is sequentially compared for equality.
/// </remarks>
[CollectionBuilder(
    builderType: typeof(ValueEqualityCollectionFactory.CollectionExpressionBuilders),
    methodName: nameof(ValueEqualityCollectionFactory.CollectionExpressionBuilders
        .CreateImmutableQueueWithValueEquality))]
public interface IImmutableQueueWithValueEquality<T> : IReadOnlyQueueWithValueEquality<T>, IImmutableQueue<T>
{
    /// <summary>
    ///     Gets a value indicating whether this is the empty queue.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this queue is empty; otherwise, <c>false</c>.
    /// </value>
    new bool IsEmpty { get; }

    /// <summary>
    ///     Gets an empty queue.
    /// </summary>
    new IImmutableQueueWithValueEquality<T> Clear();

    /// <summary>
    ///     Gets the element at the front of the queue.
    /// </summary>
    /// <returns>
    ///     The element at the front of the queue.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
    new T Peek();

    /// <summary>
    ///     Adds an element to the back of the queue.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    ///     The new queue.
    /// </returns>
    new IImmutableQueueWithValueEquality<T> Enqueue(T value);

    /// <summary>
    ///     Returns a queue that is missing the front element.
    /// </summary>
    /// <returns>A queue; never <c>null</c>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
    new IImmutableQueueWithValueEquality<T> Dequeue();

    /// <summary>
    ///     Retrieves the item at the head of the queue, and returns a queue with the head element removed.
    /// </summary>
    /// <param name="value">Receives the value from the head of the queue.</param>
    /// <returns>The new queue with the head element removed.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    IImmutableQueueWithValueEquality<T> Dequeue([MaybeNullWhen(false)] out T value);
}