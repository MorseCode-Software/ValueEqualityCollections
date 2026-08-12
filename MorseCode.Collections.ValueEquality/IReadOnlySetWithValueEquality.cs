using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

/// <typeparam name="T">The type of elements in the read-only set.</typeparam>
/// <summary>Provides a read-only abstraction of a set that is compared to other sets using value equality.</summary>
/// <remarks>
///     When compared to another <see cref="MorseCode.Collections.ValueEquality.IReadOnlySetWithValueEquality{T}" />,
///     each element is tested for equality to ensure that the set of items in both sets are the same.
/// </remarks>
[CollectionBuilder(
    builderType: typeof(ValueEqualityCollectionFactory.CollectionExpressionBuilders),
    methodName: nameof(ValueEqualityCollectionFactory.CollectionExpressionBuilders.CreateReadOnlySetWithValueEquality))]
public interface IReadOnlySetWithValueEquality<T> : IReadOnlySet<T>;