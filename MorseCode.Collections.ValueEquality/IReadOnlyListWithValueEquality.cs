using System.Collections.Generic;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">The type of elements in the read-only list.</typeparam>
/// <summary>
///     Represents a read-only collection of elements that can be accessed by index and is compared to other lists
///     using value equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyListWithValueEquality{T}" />,
///     each element is sequentially compared for equality.
/// </remarks>
public interface IReadOnlyListWithValueEquality<out T> : IReadOnlyList<T>;