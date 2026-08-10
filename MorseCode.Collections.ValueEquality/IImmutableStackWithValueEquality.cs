using System.Collections.Immutable;

namespace MorseCode.Collections.ValueEquality;

public interface IImmutableStackWithValueEquality<T> : IReadOnlyStackWithValueEquality<T>, IImmutableStack<T>
{
}