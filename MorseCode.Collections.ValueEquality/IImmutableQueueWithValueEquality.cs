using System.Collections.Immutable;

namespace MorseCode.Collections.ValueEquality;

public interface IImmutableQueueWithValueEquality<T> : IReadOnlyQueueWithValueEquality<T>, IImmutableQueue<T>
{
}