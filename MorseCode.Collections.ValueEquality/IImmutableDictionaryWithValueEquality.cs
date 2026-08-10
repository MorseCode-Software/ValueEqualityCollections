using System.Collections.Immutable;

namespace MorseCode.Collections.ValueEquality;

public interface IImmutableDictionaryWithValueEquality<TKey, TValue>
    : IReadOnlyDictionaryWithValueEquality<TKey, TValue>, IImmutableDictionary<TKey, TValue>
{
}