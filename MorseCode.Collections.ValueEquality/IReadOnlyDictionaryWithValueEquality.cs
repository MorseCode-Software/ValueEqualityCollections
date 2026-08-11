using System.Collections.Generic;

namespace MorseCode.Collections.ValueEquality;

public interface IReadOnlyDictionaryWithValueEquality<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull;