// This file provides polyfills for BCL types and attributes that are not available on all of this
// library's target frameworks (net472, netstandard2.0, and net6.0 for CollectionBuilderAttribute).
// Each polyfill is compiled only for the target frameworks that actually lack the real type.

#if !NET5_0_OR_GREATER
namespace System.Collections.Generic
{
    /// <summary>Polyfill of <see cref="System.Collections.Generic.IReadOnlySet{T}" />, introduced in .NET 5.0.</summary>
    /// <remarks>
    ///     This must be <see langword="public" /> because this library's own public interfaces (e.g.
    ///     <see cref="MorseCode.Collections.ValueEquality.IReadOnlySetWithValueEquality{T}" />) extend it.
    /// </remarks>
    public interface IReadOnlySet<T> : IReadOnlyCollection<T>
    {
        /// <summary>Determines if the set contains a specific item.</summary>
        bool Contains(T item);

        /// <summary>Determines whether the current set is a proper (strict) subset of a specified collection.</summary>
        bool IsProperSubsetOf(IEnumerable<T> other);

        /// <summary>Determines whether the current set is a proper (strict) superset of a specified collection.</summary>
        bool IsProperSupersetOf(IEnumerable<T> other);

        /// <summary>Determines whether the current set is a subset of a specified collection.</summary>
        bool IsSubsetOf(IEnumerable<T> other);

        /// <summary>Determines whether the current set is a superset of a specified collection.</summary>
        bool IsSupersetOf(IEnumerable<T> other);

        /// <summary>Determines whether the current set overlaps with the specified collection.</summary>
        bool Overlaps(IEnumerable<T> other);

        /// <summary>Determines whether the current set and the specified collection contain the same elements.</summary>
        bool SetEquals(IEnumerable<T> other);
    }
}

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>Polyfill of <see cref="MaybeNullWhenAttribute" />, introduced in .NET Core 3.0 / .NET Standard 2.1.</summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class MaybeNullWhenAttribute(bool returnValue) : Attribute
    {
        public bool ReturnValue { get; } = returnValue;
    }
}

namespace System.Collections.Generic
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Polyfills the <c>TryPeek</c> methods on <see cref="Queue{T}" /> and <see cref="Stack{T}" />, introduced
    ///     in .NET Core 2.0 and never added to .NET Framework or .NET Standard 2.0.
    /// </summary>
    internal static class QueueAndStackPolyfillExtensions
    {
        public static bool TryPeek<T>(this Queue<T> queue, [MaybeNullWhen(false)] out T result)
        {
            if (queue.Count == 0)
            {
                result = default;
                return false;
            }

            result = queue.Peek();
            return true;
        }

        public static bool TryPeek<T>(this Stack<T> stack, [MaybeNullWhen(false)] out T result)
        {
            if (stack.Count == 0)
            {
                result = default;
                return false;
            }

            result = stack.Peek();
            return true;
        }
    }
}
#endif

#if !NET8_0_OR_GREATER
namespace System.Runtime.CompilerServices
{
    /// <summary>
    ///     Polyfill of <see cref="CollectionBuilderAttribute" />, introduced in .NET 8.0 alongside collection
    ///     expressions.  Recognized by the compiler purely by its fully-qualified name, so this internal copy is
    ///     sufficient to enable collection-expression support for this library's own collection-builder-annotated
    ///     types on frameworks which do not define the attribute in the BCL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
    internal sealed class CollectionBuilderAttribute(Type builderType, string methodName) : Attribute
    {
        public Type BuilderType { get; } = builderType;

        public string MethodName { get; } = methodName;
    }
}
#endif