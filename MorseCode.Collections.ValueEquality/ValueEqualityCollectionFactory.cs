using System;
using System.Collections;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace MorseCode.Collections.ValueEquality;

/// <summary>
///     Provides factory methods for creating collections which use value equality.  When these collections are compared to
///     another value equality collection, the items in the collection are compared to determine if the collections are
///     equal.
/// </summary>
public static class ValueEqualityCollectionFactory
{
    /// <summary>Creates a wrapper around the read-only list <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">An <see cref="IReadOnlyList{T}" /> to wrap as a read-only list which uses value equality.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlyListWithValueEquality{T}" /> which wraps the read-only list <paramref name="source" />
    ///     and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IReadOnlyListWithValueEquality<T> ToReadOnlyListWithValueEquality<T>(
        this IReadOnlyList<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyListWithValueEquality<IReadOnlyList<T>, T>(
            underlying: source,
            equalityComparer: equalityComparer);

    /// <summary>Creates a wrapper around the read-only set <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     An <see cref="System.Collections.Generic.IReadOnlySet{T}" /> to wrap as a read-only set which uses
    ///     value equality.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlySetWithValueEquality{T}" /> which wraps the read-only set <paramref name="source" />
    ///     and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IReadOnlySetWithValueEquality<T>
        ToReadOnlySetWithValueEquality<T>(this IReadOnlySet<T> source) =>
        new ReadOnlySetWithValueEquality<T>(source);
#if !NET5_0_OR_GREATER

    /// <summary>Creates a wrapper around the read-only set <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     An <see cref="System.Collections.Generic.HashSet{T}" /> to wrap as a read-only set which uses
    ///     value equality.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlySetWithValueEquality{T}" /> which wraps the read-only set <paramref name="source" />
    ///     and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IReadOnlySetWithValueEquality<T>
        ToReadOnlySetWithValueEquality<T>(this HashSet<T> source) =>
        new HashSetWithValueEquality<T>(source);
#endif

    /// <summary>Creates a wrapper around the read-only dictionary <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="TKey">The type of keys of <paramref name="source" />.</typeparam>
    /// <typeparam name="TValue">The type of values of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     An <see cref="IReadOnlyDictionary{TKey,TValue}" /> to wrap as a read-only dictionary which uses
    ///     value equality.
    /// </param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing values.  Uses
    ///     <see cref="EqualityComparer{TValue}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlyDictionaryWithValueEquality{TKey,TValue}" /> which wraps the read-only dictionary
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IReadOnlyDictionaryWithValueEquality<TKey, TValue>
        ToReadOnlyDictionaryWithValueEquality<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> source,
            IEqualityComparer<TValue>? equalityComparer = null) where TKey : notnull =>
        new ReadOnlyDictionaryWithValueEquality<IReadOnlyDictionary<TKey, TValue>, TKey, TValue>(
            underlying: source,
            equalityComparer: equalityComparer);

    /// <summary>Creates a wrapper around the read-only queue <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">A <see cref="Queue{T}" /> to wrap as a read-only queue which uses value equality.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlyQueueWithValueEquality{T}" /> which wraps the read-only queue
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IReadOnlyQueueWithValueEquality<T> ToReadOnlyQueueWithValueEquality<T>(
        this Queue<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyQueueWithValueEquality<T>(underlying: source, equalityComparer: equalityComparer);

    /// <summary>Creates a wrapper around the read-only stack <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">A <see cref="Stack{T}" /> to wrap as a read-only stack which uses value equality.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlyStackWithValueEquality{T}" /> which wraps the read-only stack
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IReadOnlyStackWithValueEquality<T> ToReadOnlyStackWithValueEquality<T>(
        this Stack<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyStackWithValueEquality<T>(underlying: source, equalityComparer: equalityComparer);

    /// <summary>
    ///     Copies the elements from <paramref name="source" /> into an immutable, read-only list and creates a wrapper
    ///     around it which uses value equality.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     An <see cref="IEnumerable{T}" /> to wrap as an immutable, read-only list which uses value
    ///     equality.
    /// </param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IFrozenListWithValueEquality{T}" /> which wraps the immutable, read-only list created from
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IFrozenListWithValueEquality<T> ToFrozenListWithValueEquality<T>(
        this IEnumerable<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenListWithValueEquality<T>(immutableArray: [.. source], equalityComparer: equalityComparer);

    /// <summary>
    ///     Creates a wrapper around the immutable, read-only set optimized for fast lookup and enumeration
    ///     <paramref name="source" /> which uses value equality.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">A <see cref="FrozenSet{T}" /> to wrap as an immutable, read-only set which uses value equality.</param>
    /// <returns>
    ///     An <see cref="IFrozenSetWithValueEquality{T}" /> which wraps the immutable, read-only set
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    /// <remarks>
    ///     Frozen sets are immutable and are optimized for situations where a set
    ///     is created very infrequently but is used very frequently at run-time. They have a relatively high
    ///     cost to create but provides excellent lookup performance. Thus, it is ideal for cases
    ///     where a set is created once, potentially at the startup of an application, and is used throughout
    ///     the remainder of the life of the application. A frozen set should only be initialized
    ///     with trusted elements, as the details of the elements impacts construction time.
    /// </remarks>
    public static IFrozenSetWithValueEquality<T> ToFrozenSetWithValueEquality<T>(this FrozenSet<T> source) =>
        new FrozenSetWithValueEquality<T>(source);

    /// <summary>
    ///     Creates a wrapper around the immutable, read-only dictionary optimized for fast lookup and enumeration
    ///     <paramref name="source" /> which uses value equality.
    /// </summary>
    /// <typeparam name="TKey">The type of keys of <paramref name="source" />.</typeparam>
    /// <typeparam name="TValue">The type of values of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     A <see cref="FrozenDictionary{TKey,TValue}" /> to wrap as an immutable, read-only dictionary which
    ///     uses value equality.
    /// </param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing values.  Uses
    ///     <see cref="EqualityComparer{TValue}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IFrozenDictionaryWithValueEquality{TKey,TValue}" /> which wraps the immutable, read-only dictionary
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    /// <remarks>
    ///     Frozen dictionaries are immutable and are optimized for situations where a set
    ///     is created very infrequently but is used very frequently at run-time. They have a relatively high
    ///     cost to create but provides excellent lookup performance. Thus, it is ideal for cases
    ///     where a dictionary is created once, potentially at the startup of an application, and is used throughout
    ///     the remainder of the life of the application. A frozen dictionary should only be initialized
    ///     with trusted keys, as the details of the keys impacts construction time.
    /// </remarks>
    public static IFrozenDictionaryWithValueEquality<TKey, TValue> ToFrozenDictionaryWithValueEquality<TKey, TValue>(
        this FrozenDictionary<TKey, TValue> source,
        IEqualityComparer<TValue>? equalityComparer = null) where TKey : notnull =>
        new FrozenDictionaryWithValueEquality<TKey, TValue>(
            underlying: source,
            equalityComparer: equalityComparer);

    /// <summary>
    ///     Copies the elements from <paramref name="source" /> into an immutable, read-only queue and creates a wrapper
    ///     around it which uses value equality.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     An <see cref="IEnumerable{T}" /> to wrap as an immutable, read-only queue which uses value
    ///     equality.
    /// </param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IFrozenQueueWithValueEquality{T}" /> which wraps the immutable, read-only queue created from
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IFrozenQueueWithValueEquality<T> ToFrozenQueueWithValueEquality<T>(
        this IEnumerable<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenQueueWithValueEquality<T>(
            immutableQueue: ImmutableQueue.CreateRange(source),
            equalityComparer: equalityComparer);

    /// <summary>
    ///     Copies the elements from <paramref name="source" /> into an immutable, read-only stack and creates a wrapper
    ///     around it which uses value equality.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     An <see cref="IEnumerable{T}" /> to wrap as an immutable, read-only stack which uses value
    ///     equality.
    /// </param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IFrozenStackWithValueEquality{T}" /> which wraps the immutable, read-only stack created from
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IFrozenStackWithValueEquality<T> ToFrozenStackWithValueEquality<T>(
        this IEnumerable<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenStackWithValueEquality<T>(
            immutableStack: ImmutableStack.CreateRange(source),
            equalityComparer: equalityComparer);

    /// <summary>Creates a wrapper around the immutable list <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">An <see cref="IImmutableList{T}" /> to wrap as an immutable list which uses value equality.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IImmutableListWithValueEquality{T}" /> which wraps the immutable list <paramref name="source" />
    ///     and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IImmutableListWithValueEquality<T> ToImmutableListWithValueEquality<T>(
        this IImmutableList<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableListWithValueEquality<T>(underlying: source, equalityComparer: equalityComparer);

    /// <summary>Creates a wrapper around the immutable set <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">An <see cref="IImmutableSet{T}" /> to wrap as an immutable set which uses value equality.</param>
    /// <returns>
    ///     An <see cref="IImmutableSetWithValueEquality{T}" /> which wraps the immutable set <paramref name="source" />
    ///     and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IImmutableSetWithValueEquality<T> ToImmutableSetWithValueEquality<T>(this IImmutableSet<T> source) =>
        new ImmutableSetWithValueEquality<T>(source);

    /// <summary>Creates a wrapper around the immutable dictionary <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="TKey">The type of keys of <paramref name="source" />.</typeparam>
    /// <typeparam name="TValue">The type of values of <paramref name="source" />.</typeparam>
    /// <param name="source">
    ///     An <see cref="IImmutableDictionary{TKey,TValue}" /> to wrap as an immutable dictionary which uses
    ///     value equality.
    /// </param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing values.  Uses
    ///     <see cref="EqualityComparer{TValue}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IImmutableDictionaryWithValueEquality{TKey,TValue}" /> which wraps the immutable dictionary
    ///     <paramref name="source" /> and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IImmutableDictionaryWithValueEquality<TKey, TValue>
        ToImmutableDictionaryWithValueEquality<TKey, TValue>(
            this IImmutableDictionary<TKey, TValue> source,
            IEqualityComparer<TValue>? equalityComparer = null) where TKey : notnull =>
        new ImmutableDictionaryWithValueEquality<TKey, TValue>(
            underlying: source,
            equalityComparer: equalityComparer);

    /// <summary>Creates a wrapper around the immutable queue <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">An <see cref="IImmutableQueue{T}" /> to wrap as an immutable queue which uses value equality.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IImmutableQueueWithValueEquality{T}" /> which wraps the immutable queue <paramref name="source" />
    ///     and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IImmutableQueueWithValueEquality<T> ToImmutableQueueWithValueEquality<T>(
        this IImmutableQueue<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableQueueWithValueEquality<T>(underlying: source, equalityComparer: equalityComparer);

    /// <summary>Creates a wrapper around the immutable stack <paramref name="source" /> which uses value equality.</summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">An <see cref="IImmutableStack{T}" /> to wrap as an immutable stack which uses value equality.</param>
    /// <param name="equalityComparer">
    ///     The equality comparer used when comparing elements.  Uses
    ///     <see cref="EqualityComparer{T}.Default" /> if not specified or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IImmutableStackWithValueEquality{T}" /> which wraps the immutable stack <paramref name="source" />
    ///     and uses value equality.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static IImmutableStackWithValueEquality<T> ToImmutableStackWithValueEquality<T>(
        this IImmutableStack<T> source,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableStackWithValueEquality<T>(underlying: source, equalityComparer: equalityComparer);

    /// <summary>
    ///     Provides factory methods for creating collections which use value equality given a <see cref="ReadOnlySpan{T}" />
    ///     to allow for usage as a collection expression builder.  When these collections are compared to
    ///     another value equality collection, the items in the collection are compared to determine if the collections are
    ///     equal.
    /// </summary>
    [PublicAPI]
    public static class CollectionExpressionBuilders
    {
        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into a read-only list and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as a read-only list which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IReadOnlyListWithValueEquality{T}" /> which wraps the read-only list
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IReadOnlyListWithValueEquality<T>
            CreateReadOnlyListWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ReadOnlyListWithValueEquality<IReadOnlyList<T>, T>(
                underlying: [.. source],
                equalityComparer: null);

#if NET5_0_OR_GREATER
        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into a read-only set and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as a read-only set which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IReadOnlySetWithValueEquality{T}" /> which wraps the read-only set created from
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IReadOnlySetWithValueEquality<T>
            CreateReadOnlySetWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ReadOnlySetWithValueEquality<T>(new HashSet<T>(collection: [.. source], comparer: null));
#else
        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into a read-only set and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="HashSet{T}" /> to wrap as a read-only set which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IReadOnlySetWithValueEquality{T}" /> which wraps the read-only set created from
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IReadOnlySetWithValueEquality<T>
            CreateReadOnlySetWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new HashSetWithValueEquality<T>(new HashSet<T>(collection: [.. source], comparer: null));
#endif

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into a read-only queue and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as a read-only queue which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IReadOnlyQueueWithValueEquality{T}" /> which wraps the read-only queue created from
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IReadOnlyQueueWithValueEquality<T>
            CreateReadOnlyQueueWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ReadOnlyQueueWithValueEquality<T>(underlying: new Queue<T>([.. source]), equalityComparer: null);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into a read-only stack and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as a read-only stack which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IReadOnlyStackWithValueEquality{T}" /> which wraps the read-only stack created from
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IReadOnlyStackWithValueEquality<T>
            CreateReadOnlyStackWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ReadOnlyStackWithValueEquality<T>(underlying: new Stack<T>([.. source]), equalityComparer: null);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable, read-only list and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable, read-only list which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IFrozenListWithValueEquality{T}" /> which wraps the immutable, read-only list
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IFrozenListWithValueEquality<T>
            CreateFrozenListWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new FrozenListWithValueEquality<T>(immutableArray: [.. source], equalityComparer: null);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable, read-only set and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable, read-only set which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IFrozenSetWithValueEquality{T}" /> which wraps the immutable, read-only set
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IFrozenSetWithValueEquality<T> CreateFrozenSetWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new FrozenSetWithValueEquality<T>(FrozenSet.Create(equalityComparer: null, source: source));

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable, read-only queue and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable, read-only queue which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IFrozenQueueWithValueEquality{T}" /> which wraps the immutable, read-only queue
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IFrozenQueueWithValueEquality<T>
            CreateFrozenQueueWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new FrozenQueueWithValueEquality<T>(immutableQueue: [.. source], equalityComparer: null);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable, read-only stack and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable, read-only stack which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IFrozenStackWithValueEquality{T}" /> which wraps the immutable, read-only stack
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IFrozenStackWithValueEquality<T>
            CreateFrozenStackWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new FrozenStackWithValueEquality<T>(immutableStack: [.. source], equalityComparer: null);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable list and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable list which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IImmutableListWithValueEquality{T}" /> which wraps the immutable list
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IImmutableListWithValueEquality<T>
            CreateImmutableListWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ImmutableListWithValueEquality<T>(underlying: [.. source], equalityComparer: null);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable set and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable set which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IImmutableSetWithValueEquality{T}" /> which wraps the immutable set
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IImmutableSetWithValueEquality<T>
            CreateImmutableSetWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ImmutableSetWithValueEquality<T>([.. source]);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable queue and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable queue which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IImmutableQueueWithValueEquality{T}" /> which wraps the immutable queue
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IImmutableQueueWithValueEquality<T>
            CreateImmutableQueueWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ImmutableQueueWithValueEquality<T>(underlying: [.. source], equalityComparer: null);

        /// <summary>
        ///     Copies the elements from <paramref name="source" /> into an immutable stack and creates a wrapper
        ///     around it which uses value equality.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     A <see cref="ReadOnlySpan{T}" /> to wrap as an immutable stack which uses value equality.
        /// </param>
        /// <returns>
        ///     An <see cref="IImmutableStackWithValueEquality{T}" /> which wraps the immutable stack
        ///     <paramref name="source" /> and uses value equality.
        /// </returns>
        public static IImmutableStackWithValueEquality<T>
            CreateImmutableStackWithValueEquality<T>(ReadOnlySpan<T> source) =>
            new ImmutableStackWithValueEquality<T>(underlying: [.. source], equalityComparer: null);
    }

    #region Base Classes

    private abstract class EnumerableWithValueEqualityBase<TCollection, T>(in TCollection underlying)
        : IEnumerable<T>
        where TCollection : IEnumerable<T>
    {
        protected readonly TCollection Underlying = underlying ?? throw new ArgumentNullException(nameof(underlying));

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.Underlying.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Underlying).GetEnumerator();
    }

    private abstract class ReadOnlyCollectionWithValueEqualityBase<TCollection, T>(in TCollection underlying)
        : EnumerableWithValueEqualityBase<TCollection, T>(underlying),
            IReadOnlyCollection<T>
        where TCollection : IReadOnlyCollection<T>
    {
        int IReadOnlyCollection<T>.Count => this.Underlying.Count;
    }

    #endregion Base Class

    #region List Implementations

    private class ReadOnlyListWithValueEquality<TCollection, T>(
        in TCollection underlying,
        in IEqualityComparer<T>? equalityComparer)
        : ReadOnlyCollectionWithValueEqualityBase<TCollection, T>(underlying),
            IReadOnlyListWithValueEquality<T> where TCollection : IReadOnlyList<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        T IReadOnlyList<T>.this[int index] => this.Underlying[index];

        public override bool Equals(object? obj) =>
            obj is IReadOnlyListWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.Underlying.Count;
    }

    private sealed class FrozenListWithValueEquality<T>(
        in ImmutableArray<T> immutableArray,
        in IEqualityComparer<T>? equalityComparer)
        : ReadOnlyListWithValueEquality<ImmutableArray<T>, T>(
                underlying: immutableArray,
                equalityComparer: equalityComparer),
            IFrozenListWithValueEquality<T>;

    private sealed class ImmutableListWithValueEquality<T>(
        in IImmutableList<T> underlying,
        in IEqualityComparer<T>? equalityComparer)
        : ReadOnlyListWithValueEquality<IImmutableList<T>, T>(
                underlying: underlying,
                equalityComparer: equalityComparer),
            IImmutableListWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        public IImmutableListWithValueEquality<T> Add(T value) => this.CreateNew(this.Underlying.Add(value));

        public IImmutableListWithValueEquality<T> AddRange(IEnumerable<T> items) =>
            this.CreateNew(this.Underlying.AddRange(items));

        public IImmutableListWithValueEquality<T> Clear() => this.CreateNew(this.Underlying.Clear());

        public IImmutableListWithValueEquality<T> Insert(int index, T element) =>
            this.CreateNew(this.Underlying.Insert(index: index, element: element));

        public IImmutableListWithValueEquality<T> InsertRange(int index, IEnumerable<T> items) =>
            this.CreateNew(this.Underlying.InsertRange(index: index, items: items));

        public IImmutableListWithValueEquality<T> Remove(T value, IEqualityComparer<T>? equalityComparerOverride) =>
            this.CreateNew(this.Underlying.Remove(value: value, equalityComparer: equalityComparerOverride));

        public IImmutableListWithValueEquality<T> RemoveAll(Predicate<T> match) =>
            this.CreateNew(this.Underlying.RemoveAll(match));

        public IImmutableListWithValueEquality<T> RemoveAt(int index) =>
            this.CreateNew(this.Underlying.RemoveAt(index));

        public IImmutableListWithValueEquality<T> RemoveRange(
            IEnumerable<T> items,
            IEqualityComparer<T>? equalityComparerOverride) =>
            this.CreateNew(this.Underlying.RemoveRange(items: items, equalityComparer: equalityComparerOverride));

        public IImmutableListWithValueEquality<T> RemoveRange(int index, int count) =>
            this.CreateNew(this.Underlying.RemoveRange(index: index, count: count));

        public IImmutableListWithValueEquality<T> Replace(
            T oldValue,
            T newValue,
            IEqualityComparer<T>? equalityComparerOverride) =>
            this.CreateNew(
                this.Underlying.Replace(
                    oldValue: oldValue,
                    newValue: newValue,
                    equalityComparer: equalityComparerOverride));

        public IImmutableListWithValueEquality<T> SetItem(int index, T value) =>
            this.CreateNew(this.Underlying.SetItem(index: index, value: value));

        public IImmutableListWithValueEquality<T> Replace(T oldValue, T newValue) =>
            this.Replace(oldValue: oldValue, newValue: newValue, equalityComparerOverride: this.equalityComparer);

        public IImmutableListWithValueEquality<T> Remove(T value) =>
            this.Remove(value: value, equalityComparerOverride: this.equalityComparer);

        public IImmutableListWithValueEquality<T> RemoveRange(IEnumerable<T> items) =>
            this.RemoveRange(items: items, equalityComparerOverride: this.equalityComparer);

        IImmutableList<T> IImmutableList<T>.Add(T value) => this.Add(value);

        IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items) => this.AddRange(items);

        IImmutableList<T> IImmutableList<T>.Clear() => this.Clear();

        public int IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparerOverride) =>
            this.Underlying.IndexOf(item: item, index: index, count: count, equalityComparer: equalityComparerOverride);

        public int IndexOf(T item) => this.IndexOf(item: item, equalityComparer: this.equalityComparer);

        public int IndexOf(T item, int startIndex) =>
            this.IndexOf(
                item: item,
                startIndex: startIndex,
                count: ((IImmutableListWithValueEquality<T>)this).Count - startIndex);

        public int IndexOf(T item, int startIndex, int count) =>
            this.IndexOf(item: item, index: startIndex, count: count, equalityComparerOverride: this.equalityComparer);

        IImmutableList<T> IImmutableList<T>.Insert(int index, T element) => this.Insert(index: index, element: element);

        IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items) =>
            this.InsertRange(index: index, items: items);

        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparerOverride) =>
            this.Underlying.LastIndexOf(
                item: item,
                index: index,
                count: count,
                equalityComparer: equalityComparerOverride);

        public int LastIndexOf(T item) => this.LastIndexOf(item: item, equalityComparer: this.equalityComparer);

        public int LastIndexOf(T item, int startIndex)
        {
            if (((IImmutableListWithValueEquality<T>)this).Count == 0 && startIndex == 0)
            {
                return -1;
            }

            return this.LastIndexOf(item: item, startIndex: startIndex, count: startIndex + 1);
        }

        public int LastIndexOf(T item, int startIndex, int count) =>
            this.LastIndexOf(
                item: item,
                index: startIndex,
                count: count,
                equalityComparerOverride: this.equalityComparer);

        IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T>? equalityComparerOverride) =>
            this.Remove(value: value, equalityComparerOverride: equalityComparerOverride);

        IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match) => this.RemoveAll(match);

        IImmutableList<T> IImmutableList<T>.RemoveAt(int index) => this.RemoveAt(index);

        IImmutableList<T> IImmutableList<T>.RemoveRange(
            IEnumerable<T> items,
            IEqualityComparer<T>? equalityComparerOverride) =>
            this.RemoveRange(items: items, equalityComparerOverride: equalityComparerOverride);

        IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count) =>
            this.RemoveRange(index: index, count: count);

        IImmutableList<T> IImmutableList<T>.Replace(
            T oldValue,
            T newValue,
            IEqualityComparer<T>? equalityComparerOverride) =>
            this.Replace(oldValue: oldValue, newValue: newValue, equalityComparerOverride: equalityComparerOverride);

        IImmutableList<T> IImmutableList<T>.SetItem(int index, T value) => this.SetItem(index: index, value: value);

        private ImmutableListWithValueEquality<T> CreateNew(IImmutableList<T> immutableList) =>
            new(underlying: immutableList, equalityComparer: this.equalityComparer);
    }

    #endregion List Implementations

    #region Set Implementations

    private class ReadOnlySetWithValueEquality<T>(in IReadOnlySet<T> underlying)
        : ReadOnlyCollectionWithValueEqualityBase<IReadOnlySet<T>, T>(underlying),
            IReadOnlySetWithValueEquality<T>
    {
        public bool Contains(T item) => this.Underlying.Contains(item);

        public bool IsProperSubsetOf(IEnumerable<T> other) => this.Underlying.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => this.Underlying.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => this.Underlying.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => this.Underlying.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => this.Underlying.Overlaps(other);

        public bool SetEquals(IEnumerable<T> other) => this.Underlying.SetEquals(other);

        public override bool Equals(object? obj) => obj is IReadOnlySetWithValueEquality<T> c && this.SetEquals(c);

        public override int GetHashCode() => this.Underlying.Count;
    }
#if !NET5_0_OR_GREATER

    /// <summary>
    ///     Used when building a set directly from a
    ///     <see cref="HashSet{T}" /> this library constructed itself (e.g. from a collection expression).  On
    ///     frameworks without <see cref="System.Collections.Generic.IReadOnlySet{T}" /> in the BCL,
    ///     <see cref="HashSet{T}" /> does not implement this library's polyfilled substitute, so it is stored and
    ///     accessed directly here instead of being converted.
    /// </summary>
    private sealed class HashSetWithValueEquality<T>(in HashSet<T> underlying)
        : ReadOnlyCollectionWithValueEqualityBase<HashSet<T>, T>(underlying),
            IReadOnlySetWithValueEquality<T>
    {
        public bool Contains(T item) => this.Underlying.Contains(item);

        public bool IsProperSubsetOf(IEnumerable<T> other) => this.Underlying.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => this.Underlying.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => this.Underlying.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => this.Underlying.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => this.Underlying.Overlaps(other);

        public bool SetEquals(IEnumerable<T> other) => this.Underlying.SetEquals(other);

        public override bool Equals(object? obj) => obj is IReadOnlySetWithValueEquality<T> c && this.SetEquals(c);

        public override int GetHashCode() => this.Underlying.Count;
    }
#endif

    /// <summary>
    ///     Does not dDoes not derive from <see cref="ReadOnlyCollectionWithValueEqualityBase{TCollection, T}" /> because the
    ///     downlevel build of
    ///     <see cref="FrozenSet{T}" /> shipped for older target frameworks by the
    ///     <c>System.Collections.Immutable</c> package does not implement
    ///     <see cref="System.Collections.Generic.IReadOnlySet{T}" />, even on frameworks where that interface
    ///     exists in the BCL, so it is stored and accessed directly here instead.
    /// </summary>
    private sealed class FrozenSetWithValueEquality<T>(in FrozenSet<T> frozenSet)
        : ReadOnlyCollectionWithValueEqualityBase<FrozenSet<T>, T>(frozenSet),
            IFrozenSetWithValueEquality<T>
    {
        public bool Contains(T item) => this.Underlying.Contains(item);

        public bool IsProperSubsetOf(IEnumerable<T> other) => this.Underlying.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => this.Underlying.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => this.Underlying.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => this.Underlying.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => this.Underlying.Overlaps(other);

        public bool SetEquals(IEnumerable<T> other) => this.Underlying.SetEquals(other);

        public override bool Equals(object? obj) => obj is IReadOnlySetWithValueEquality<T> c && this.SetEquals(c);

        public override int GetHashCode() => this.Underlying.Count;
    }

    private sealed class ImmutableSetWithValueEquality<T>(in IImmutableSet<T> underlying)
        : ReadOnlyCollectionWithValueEqualityBase<IImmutableSet<T>, T>(underlying),
            IImmutableSetWithValueEquality<T>
    {
        public bool Contains(T item) => this.Underlying.Contains(item);

        public bool IsProperSubsetOf(IEnumerable<T> other) => this.Underlying.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => this.Underlying.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => this.Underlying.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => this.Underlying.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => this.Underlying.Overlaps(other);

        public bool SetEquals(IEnumerable<T> other) => this.Underlying.SetEquals(other);

        public IImmutableSetWithValueEquality<T> Clear() =>
            ImmutableSetWithValueEquality<T>.CreateNew(this.Underlying.Clear());

        IImmutableSet<T> IImmutableSet<T>.Clear() => this.Clear();

        public IImmutableSetWithValueEquality<T> Add(T value) =>
            ImmutableSetWithValueEquality<T>.CreateNew(this.Underlying.Add(value));

        IImmutableSet<T> IImmutableSet<T>.Add(T value) => this.Add(value);

        public IImmutableSetWithValueEquality<T> Remove(T value) =>
            ImmutableSetWithValueEquality<T>.CreateNew(this.Underlying.Remove(value));

        IImmutableSet<T> IImmutableSet<T>.Remove(T value) => this.Remove(value);

        public bool TryGetValue(T equalValue, out T actualValue) =>
            this.Underlying.TryGetValue(equalValue: equalValue, actualValue: out actualValue);

        public IImmutableSetWithValueEquality<T> Intersect(IEnumerable<T> other) =>
            ImmutableSetWithValueEquality<T>.CreateNew(this.Underlying.Intersect(other));

        IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other) => this.Intersect(other);

        public IImmutableSetWithValueEquality<T> Except(IEnumerable<T> other) =>
            ImmutableSetWithValueEquality<T>.CreateNew(this.Underlying.Except(other));

        IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other) => this.Except(other);

        public IImmutableSetWithValueEquality<T> SymmetricExcept(IEnumerable<T> other) =>
            ImmutableSetWithValueEquality<T>.CreateNew(this.Underlying.SymmetricExcept(other));

        IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other) => this.SymmetricExcept(other);

        public IImmutableSetWithValueEquality<T> Union(IEnumerable<T> other) =>
            ImmutableSetWithValueEquality<T>.CreateNew(this.Underlying.Union(other));

        IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other) => this.Union(other);

        private static ImmutableSetWithValueEquality<T> CreateNew(IImmutableSet<T> immutableSet) =>
            new(underlying: immutableSet);

        public override bool Equals(object? obj) => obj is IReadOnlySetWithValueEquality<T> c && this.SetEquals(c);

        public override int GetHashCode() => this.Underlying.Count;
    }

    #endregion Set Implementations

    #region Dictionary Implementations

    private class ReadOnlyDictionaryWithValueEquality<TCollection, TKey, TValue>(
        in TCollection underlying,
        in IEqualityComparer<TValue>? equalityComparer)
        : ReadOnlyCollectionWithValueEqualityBase<TCollection, KeyValuePair<TKey, TValue>>(underlying),
            IReadOnlyDictionaryWithValueEquality<TKey, TValue>
        where TCollection : IReadOnlyDictionary<TKey, TValue>
        where TKey : notnull
    {
        private readonly IEqualityComparer<TValue> equalityComparer =
            equalityComparer ?? EqualityComparer<TValue>.Default;

        public bool ContainsKey(TKey key) => this.Underlying.ContainsKey(key);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) =>
            this.Underlying.TryGetValue(key: key, value: out value);

        public TValue this[TKey key] => this.Underlying[key];
        public IEnumerable<TKey> Keys => this.Underlying.Keys;
        public IEnumerable<TValue> Values => this.Underlying.Values;

        public override bool Equals(object? obj)
        {
            if (obj is IReadOnlyDictionaryWithValueEquality<TKey, TValue> otherReadOnlyDictionary)
            {
                if (((IReadOnlyDictionaryWithValueEquality<TKey, TValue>)this).Count != otherReadOnlyDictionary.Count)
                {
                    return false;
                }

                foreach (KeyValuePair<TKey, TValue> pair in this)
                {
                    if (!otherReadOnlyDictionary.TryGetValue(key: pair.Key, value: out TValue? otherValue))
                    {
                        return false;
                    }

                    // ReSharper disable once RedundantNameQualifier
                    if (!this.equalityComparer.Equals(x: pair.Value, y: otherValue))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode() => this.Underlying.Count;
    }

    private class FrozenDictionaryWithValueEquality<TKey, TValue>(
        in FrozenDictionary<TKey, TValue> underlying,
        in IEqualityComparer<TValue>? equalityComparer)
        : ReadOnlyDictionaryWithValueEquality<FrozenDictionary<TKey, TValue>, TKey, TValue>(
                underlying: underlying,
                equalityComparer: equalityComparer),
            IFrozenDictionaryWithValueEquality<TKey, TValue>
        where TKey : notnull;

    private class ImmutableDictionaryWithValueEquality<TKey, TValue>(
        in IImmutableDictionary<TKey, TValue> underlying,
        in IEqualityComparer<TValue>? equalityComparer)
        : ReadOnlyDictionaryWithValueEquality<IImmutableDictionary<TKey, TValue>, TKey, TValue>(
                underlying: underlying,
                equalityComparer: equalityComparer),
            IImmutableDictionaryWithValueEquality<TKey, TValue>
        where TKey : notnull
    {
        private readonly IEqualityComparer<TValue>? equalityComparer = equalityComparer;

        public IImmutableDictionaryWithValueEquality<TKey, TValue> Clear() => this.CreateNew(this.Underlying.Clear());

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear() => this.Clear();

        public IImmutableDictionaryWithValueEquality<TKey, TValue> Add(TKey key, TValue value) =>
            this.CreateNew(this.Underlying.Add(key: key, value: value));

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value) =>
            this.Add(key: key, value: value);

        public IImmutableDictionaryWithValueEquality<TKey, TValue> AddRange(
            IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
            this.CreateNew(this.Underlying.AddRange(pairs));

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(
            IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
            this.AddRange(pairs);

        public IImmutableDictionaryWithValueEquality<TKey, TValue> SetItem(TKey key, TValue value) =>
            this.CreateNew(this.Underlying.SetItem(key: key, value: value));

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value) =>
            this.SetItem(key: key, value: value);

        public IImmutableDictionaryWithValueEquality<TKey, TValue> SetItems(
            IEnumerable<KeyValuePair<TKey, TValue>> items) =>
            this.CreateNew(this.Underlying.SetItems(items));

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(
            IEnumerable<KeyValuePair<TKey, TValue>> items) =>
            this.SetItems(items);

        public IImmutableDictionaryWithValueEquality<TKey, TValue> RemoveRange(IEnumerable<TKey> keys) =>
            this.CreateNew(this.Underlying.RemoveRange(keys));

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys) =>
            this.RemoveRange(keys);

        public IImmutableDictionaryWithValueEquality<TKey, TValue> Remove(TKey key) =>
            this.CreateNew(this.Underlying.Remove(key));

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key) => this.Remove(key);

        public bool Contains(KeyValuePair<TKey, TValue> pair) => this.Underlying.Contains(pair);

        public bool TryGetKey(TKey equalKey, out TKey actualKey) =>
            this.Underlying.TryGetKey(equalKey: equalKey, actualKey: out actualKey);

        private ImmutableDictionaryWithValueEquality<TKey, TValue> CreateNew(
            IImmutableDictionary<TKey, TValue> immutableDictionary) =>
            new(underlying: immutableDictionary, equalityComparer: this.equalityComparer);
    }

    #endregion Dictionary Implementations

    #region Queue Implementations

    private class ReadOnlyQueueWithValueEquality<T>(in Queue<T> underlying, in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<Queue<T>, T>(underlying),
            IReadOnlyQueueWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        public bool IsEmpty => this.Underlying.Count == 0;
        public T Peek() => this.Underlying.Peek();
        public bool TryPeek([MaybeNullWhen(false)] out T result) => this.Underlying.TryPeek(out result);
        public bool Contains(T item) => this.Underlying.Contains(item);

        // ReSharper disable once UseCollectionExpression
        public T[] ToArray() => this.Underlying.ToArray();

        public override bool Equals(object? obj) =>
            obj is IReadOnlyQueueWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    private sealed class FrozenQueueWithValueEquality<T>(
        in IImmutableQueue<T> immutableQueue,
        in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<IImmutableQueue<T>, T>(immutableQueue),
            IFrozenQueueWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        public bool IsEmpty => this.Underlying.IsEmpty;
        public T Peek() => this.Underlying.Peek();

        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            if (this.IsEmpty)
            {
                result = default;
                return false;
            }

            result = this.Peek();
            return true;
        }

        public bool Contains(T item) => this.Underlying.Contains(item);

        // ReSharper disable once UseCollectionExpression
        public T[] ToArray() => this.Underlying.ToArray();

        public override bool Equals(object? obj) =>
            obj is IReadOnlyQueueWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    private sealed class ImmutableQueueWithValueEquality<T>(
        in IImmutableQueue<T> underlying,
        in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<IImmutableQueue<T>, T>(underlying),
            IImmutableQueueWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        public bool IsEmpty => this.Underlying.IsEmpty;
        public T Peek() => this.Underlying.Peek();

        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            if (this.IsEmpty)
            {
                result = default;
                return false;
            }

            result = this.Peek();
            return true;
        }

        public bool Contains(T item) => this.Underlying.Contains(item);

        // ReSharper disable once UseCollectionExpression
        public T[] ToArray() => this.Underlying.ToArray();

        public IImmutableQueueWithValueEquality<T> Clear() => this.CreateNew(this.Underlying.Clear());

        IImmutableQueue<T> IImmutableQueue<T>.Clear() => this.Clear();

        public IImmutableQueueWithValueEquality<T> Enqueue(T value) => this.CreateNew(this.Underlying.Enqueue(value));

        IImmutableQueue<T> IImmutableQueue<T>.Enqueue(T value) => this.Enqueue(value);

        public IImmutableQueueWithValueEquality<T> Dequeue() => this.CreateNew(this.Underlying.Dequeue());

        IImmutableQueue<T> IImmutableQueue<T>.Dequeue() => this.Dequeue();

        public IImmutableQueueWithValueEquality<T> Dequeue([MaybeNullWhen(false)] out T value) =>
            this.CreateNew(this.Underlying.Dequeue(out value));

        private ImmutableQueueWithValueEquality<T> CreateNew(IImmutableQueue<T> immutableQueue) =>
            new(underlying: immutableQueue, equalityComparer: this.equalityComparer);

        public override bool Equals(object? obj) =>
            obj is IReadOnlyQueueWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    #endregion Queue Implementations

    #region Stack Implementations

    private class ReadOnlyStackWithValueEquality<T>(in Stack<T> underlying, in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<Stack<T>, T>(underlying),
            IReadOnlyStackWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        public bool IsEmpty => this.Underlying.Count == 0;
        public T Peek() => this.Underlying.Peek();
        public bool TryPeek([MaybeNullWhen(false)] out T result) => this.Underlying.TryPeek(out result);
        public bool Contains(T item) => this.Underlying.Contains(item);

        // ReSharper disable once UseCollectionExpression
        public T[] ToArray() => this.Underlying.ToArray();

        public override bool Equals(object? obj) =>
            obj is IReadOnlyStackWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    private sealed class FrozenStackWithValueEquality<T>(
        in IImmutableStack<T> immutableStack,
        in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<IImmutableStack<T>, T>(immutableStack),
            IFrozenStackWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        public bool IsEmpty => this.Underlying.IsEmpty;
        public T Peek() => this.Underlying.Peek();

        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            if (this.IsEmpty)
            {
                result = default;
                return false;
            }

            result = this.Peek();
            return true;
        }

        public bool Contains(T item) => this.Underlying.Contains(item);

        // ReSharper disable once UseCollectionExpression
        public T[] ToArray() => this.Underlying.ToArray();

        public override bool Equals(object? obj) =>
            obj is IReadOnlyStackWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    private sealed class ImmutableStackWithValueEquality<T>(
        in IImmutableStack<T> underlying,
        in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<IImmutableStack<T>, T>(underlying),
            IImmutableStackWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        public bool IsEmpty => this.Underlying.IsEmpty;
        public T Peek() => this.Underlying.Peek();

        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            if (this.IsEmpty)
            {
                result = default;
                return false;
            }

            result = this.Peek();
            return true;
        }

        public bool Contains(T item) => this.Underlying.Contains(item);

        // ReSharper disable once UseCollectionExpression
        public T[] ToArray() => this.Underlying.ToArray();

        public IImmutableStackWithValueEquality<T> Clear() => this.CreateNew(this.Underlying.Clear());

        IImmutableStack<T> IImmutableStack<T>.Clear() => this.Clear();

        public IImmutableStackWithValueEquality<T> Push(T value) => this.CreateNew(this.Underlying.Push(value));

        IImmutableStack<T> IImmutableStack<T>.Push(T value) => this.Push(value);

        public IImmutableStackWithValueEquality<T> Pop() => this.CreateNew(this.Underlying.Pop());

        IImmutableStack<T> IImmutableStack<T>.Pop() => this.Pop();

        public IImmutableStackWithValueEquality<T> Pop([MaybeNullWhen(false)] out T value) =>
            this.CreateNew(this.Underlying.Pop(out value));

        private ImmutableStackWithValueEquality<T> CreateNew(IImmutableStack<T> immutableStack) =>
            new(underlying: immutableStack, equalityComparer: this.equalityComparer);

        public override bool Equals(object? obj) =>
            obj is IReadOnlyStackWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    #endregion Stack Implementations
}