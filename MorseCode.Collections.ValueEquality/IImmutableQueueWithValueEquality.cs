using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace MorseCode.Collections.ValueEquality;

public interface IImmutableQueueWithValueEquality<T> : IReadOnlyQueueWithValueEquality<T>, IImmutableQueue<T>
{
    /// <summary>
    /// Gets an empty queue.
    /// </summary>
    new IImmutableQueueWithValueEquality<T> Clear();

    /// <summary>
    /// Gets the element at the front of the queue.
    /// </summary>
    /// <returns>
    /// The element at the front of the queue.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
    new T Peek();

    /// <summary>
    /// Adds an element to the back of the queue.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The new queue.
    /// </returns>
    new IImmutableQueueWithValueEquality<T> Enqueue(T value);

    /// <summary>
    /// Returns a queue that is missing the front element.
    /// </summary>
    /// <returns>A queue; never <c>null</c>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
    new IImmutableQueueWithValueEquality<T> Dequeue();

    /// <summary>
    /// Retrieves the item at the head of the queue, and returns a queue with the head element removed.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the queue.</typeparam>
    /// <param name="value">Receives the value from the head of the queue.</param>
    /// <returns>The new queue with the head element removed.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    IImmutableQueueWithValueEquality<T> Dequeue([MaybeNullWhen(false)] out T value);
}