namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">The type of elements in the immutable, read-only set.</typeparam>
/// <summary>Provides an immutable, read-only abstraction of a set that is compared to other sets using value equality.</summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlySetWithValueEquality{T}" />,
///     each element is tested for equality to ensure that the set of items in both sets are the same.
/// </remarks>
public interface IFrozenSetWithValueEquality<T> : IReadOnlySetWithValueEquality<T>;