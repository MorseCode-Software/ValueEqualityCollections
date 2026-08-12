using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">Specifies the type of elements in the immutable stack.</typeparam>
/// <summary>
///     Represents an immutable variable size last-in-first-out (LIFO) collection of instances of the same specified
///     type that is compared to other stacks using value equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyStackWithValueEquality{T}" />
///     , each element is sequentially compared for equality.
/// </remarks>
[CollectionBuilder(
    builderType: typeof(ValueEqualityCollectionFactory.CollectionExpressionBuilders),
    methodName: nameof(ValueEqualityCollectionFactory.CollectionExpressionBuilders
        .CreateImmutableStackWithValueEquality))]
public interface IImmutableStackWithValueEquality<T> : IReadOnlyStackWithValueEquality<T>, IImmutableStack<T>
{
    /// <summary>
    ///     Gets a value indicating whether this is the empty stack.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this stack is empty; otherwise, <c>false</c>.
    /// </value>
    new bool IsEmpty { get; }

    /// <summary>
    ///     Gets an empty stack.
    /// </summary>
    new IImmutableStackWithValueEquality<T> Clear();

    /// <summary>
    ///     Pushes an element onto a stack and returns the new stack.
    /// </summary>
    /// <param name="value">The element to push onto the stack.</param>
    /// <returns>The new stack.</returns>
    new IImmutableStackWithValueEquality<T> Push(T value);

    /// <summary>
    ///     Pops the top element off the stack.
    /// </summary>
    /// <returns>The new stack; never <c>null</c></returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    new IImmutableStackWithValueEquality<T> Pop();

    /// <summary>
    ///     Gets the element on the top of the stack.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    new T Peek();

    /// <summary>
    ///     Pops the top element off the stack.
    /// </summary>
    /// <param name="value">The value that was removed from the stack.</param>
    /// <returns>
    ///     A stack; never <c>null</c>
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    IImmutableStackWithValueEquality<T> Pop([MaybeNullWhen(false)] out T value);
}