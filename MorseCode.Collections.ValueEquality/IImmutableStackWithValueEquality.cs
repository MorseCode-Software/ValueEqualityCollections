using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace MorseCode.Collections.ValueEquality;

public interface IImmutableStackWithValueEquality<T> : IReadOnlyStackWithValueEquality<T>, IImmutableStack<T>
{
    /// <summary>
    /// Gets an empty stack.
    /// </summary>
    new IImmutableStackWithValueEquality<T> Clear();

    /// <summary>
    /// Pushes an element onto a stack and returns the new stack.
    /// </summary>
    /// <param name="value">The element to push onto the stack.</param>
    /// <returns>The new stack.</returns>
    new IImmutableStackWithValueEquality<T> Push(T value);

    /// <summary>
    /// Pops the top element off the stack.
    /// </summary>
    /// <returns>The new stack; never <c>null</c></returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    new IImmutableStackWithValueEquality<T> Pop();

    /// <summary>
    /// Gets the element on the top of the stack.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    new T Peek();

    /// <summary>
    /// Pops the top element off the stack.
    /// </summary>
    /// <typeparam name="T">The type of values contained in the stack.</typeparam>
    /// <param name="value">The value that was removed from the stack.</param>
    /// <returns>
    /// A stack; never <c>null</c>
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    IImmutableStackWithValueEquality<T> Pop([MaybeNullWhen(false)] out T value);
}