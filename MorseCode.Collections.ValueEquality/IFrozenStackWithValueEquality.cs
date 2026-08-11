namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">Specifies the type of elements in the immutable, read-only stack.</typeparam>
/// <summary>
///     Represents an immutable, read-only variable size last-in-first-out (LIFO) collection of instances of the same
///     specified type that is compared to other stacks using value equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyStackWithValueEquality{T}" />
///     , each element is sequentially compared for equality.
/// </remarks>
public interface IFrozenStackWithValueEquality<T> : IReadOnlyStackWithValueEquality<T>;