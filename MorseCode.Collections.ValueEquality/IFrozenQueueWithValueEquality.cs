using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">Specifies the type of elements in the immutable, read-only queue.</typeparam>
/// <summary>
///     Represents an immutable, read-only first-in, first-out collection of objects that is compared to other queues
///     using value equality.
/// </summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlyQueueWithValueEquality{T}" />
///     , each element is sequentially compared for equality.
/// </remarks>
[CollectionBuilder(
    builderType: typeof(ValueEqualityCollectionFactory.CollectionExpressionBuilders),
    methodName: nameof(ValueEqualityCollectionFactory.CollectionExpressionBuilders.CreateFrozenQueueWithValueEquality))]
public interface IFrozenQueueWithValueEquality<T> : IReadOnlyQueueWithValueEquality<T>;