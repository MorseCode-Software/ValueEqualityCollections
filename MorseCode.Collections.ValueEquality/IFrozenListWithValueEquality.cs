using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">The type of elements in the immutable, read-only list.</typeparam>
/// <summary>
///     Represents an immutable, read-only collection of elements that can be accessed by index and is compared to
///     other lists using value equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyListWithValueEquality{T}" />,
///     each element is sequentially compared for equality.
/// </remarks>
[CollectionBuilder(
    builderType: typeof(ValueEqualityCollectionFactory.CollectionExpressionBuilders),
    methodName: nameof(ValueEqualityCollectionFactory.CollectionExpressionBuilders.CreateFrozenListWithValueEquality))]
public interface IFrozenListWithValueEquality<out T> : IReadOnlyListWithValueEquality<T>;