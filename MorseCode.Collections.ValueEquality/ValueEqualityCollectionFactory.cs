using System;
using System.Collections;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MorseCode.Collections.ValueEquality;

public static class ValueEqualityCollectionFactory
{
    public static IReadOnlyListWithValueEquality<T> ToReadOnlyListWithValueEquality<T>(
        this IReadOnlyList<T> readOnlyList,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyListWithValueEquality<IReadOnlyList<T>, T>(
            underlying: readOnlyList,
            equalityComparer: equalityComparer);

    public static IReadOnlyListWithValueEquality<T> CreateReadOnlyListWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyListWithValueEquality<IReadOnlyList<T>, T>(
            underlying: [.. readOnlySpan],
            equalityComparer: equalityComparer);

    public static IReadOnlySetWithValueEquality<T>
        ToReadOnlySetWithValueEquality<T>(this IReadOnlySet<T> readOnlySet) =>
        new ReadOnlySetWithValueEquality<T>(readOnlySet);

    public static IReadOnlySetWithValueEquality<T> CreateReadOnlySetWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlySetWithValueEquality<T>(new HashSet<T>(collection: [.. readOnlySpan], comparer: equalityComparer));

    public static IReadOnlyDictionaryWithValueEquality<TKey, TValue>
        ToReadOnlyDictionaryWithValueEquality<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> readOnlyDictionary,
            IEqualityComparer<TValue>? equalityComparer = null) where TKey : notnull =>
        new ReadOnlyDictionaryWithValueEquality<IReadOnlyDictionary<TKey, TValue>, TKey, TValue>(
            underlying: readOnlyDictionary,
            equalityComparer: equalityComparer);

    public static IReadOnlyQueueWithValueEquality<T> ToReadOnlyQueueWithValueEquality<T>(
        this Queue<T> readOnlyQueue,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyQueueWithValueEquality<T>(underlying: readOnlyQueue, equalityComparer: equalityComparer);

    public static IReadOnlyQueueWithValueEquality<T> CreateReadOnlyQueueWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyQueueWithValueEquality<T>(
            underlying: new Queue<T>([.. readOnlySpan]),
            equalityComparer: equalityComparer);

    public static IReadOnlyStackWithValueEquality<T> ToReadOnlyStackWithValueEquality<T>(
        this Stack<T> readOnlyStack,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyStackWithValueEquality<T>(underlying: readOnlyStack, equalityComparer: equalityComparer);

    public static IReadOnlyStackWithValueEquality<T> CreateReadOnlyStackWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ReadOnlyStackWithValueEquality<T>(
            underlying: new Stack<T>([.. readOnlySpan]),
            equalityComparer: equalityComparer);

    public static IFrozenListWithValueEquality<T> ToFrozenListWithValueEquality<T>(
        this IEnumerable<T> enumerable,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenListWithValueEquality<T>(immutableArray: [.. enumerable], equalityComparer: equalityComparer);

    public static IFrozenListWithValueEquality<T> CreateFrozenListWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenListWithValueEquality<T>(immutableArray: [.. readOnlySpan], equalityComparer: equalityComparer);

    public static IFrozenSetWithValueEquality<T> ToFrozenSetWithValueEquality<T>(this FrozenSet<T> frozenSet) =>
        new FrozenSetWithValueEquality<T>(frozenSet);

    public static IFrozenSetWithValueEquality<T> CreateFrozenSetWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenSetWithValueEquality<T>(FrozenSet.Create(equalityComparer: equalityComparer, source: readOnlySpan));

    public static IFrozenDictionaryWithValueEquality<TKey, TValue> ToFrozenDictionaryWithValueEquality<TKey, TValue>(
        this FrozenDictionary<TKey, TValue> frozenDictionary,
        IEqualityComparer<TValue>? equalityComparer = null) where TKey : notnull =>
        new FrozenDictionaryWithValueEquality<TKey, TValue>(
            underlying: frozenDictionary,
            equalityComparer: equalityComparer);

    public static IFrozenQueueWithValueEquality<T> ToFrozenQueueWithValueEquality<T>(
        this IEnumerable<T> enumerable,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenQueueWithValueEquality<T>(
            immutableQueue: ImmutableQueue.CreateRange(enumerable),
            equalityComparer: equalityComparer);

    public static IFrozenQueueWithValueEquality<T> CreateFrozenQueueWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenQueueWithValueEquality<T>(
            immutableQueue: [.. readOnlySpan],
            equalityComparer: equalityComparer);

    public static IFrozenStackWithValueEquality<T> ToFrozenStackWithValueEquality<T>(
        this IEnumerable<T> enumerable,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenStackWithValueEquality<T>(
            immutableStack: ImmutableStack.CreateRange(enumerable),
            equalityComparer: equalityComparer);

    public static IFrozenStackWithValueEquality<T> CreateFrozenStackWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new FrozenStackWithValueEquality<T>(
            immutableStack: [.. readOnlySpan],
            equalityComparer: equalityComparer);

    public static IImmutableListWithValueEquality<T> ToImmutableListWithValueEquality<T>(
        this IImmutableList<T> immutableList,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableListWithValueEquality<T>(underlying: immutableList, equalityComparer: equalityComparer);

    public static IImmutableListWithValueEquality<T> CreateImmutableListWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableListWithValueEquality<T>(underlying: [.. readOnlySpan], equalityComparer: equalityComparer);

    public static IImmutableSetWithValueEquality<T> ToImmutableSetWithValueEquality<T>(
        this IImmutableSet<T> immutableSet) =>
        new ImmutableSetWithValueEquality<T>(immutableSet);

    public static IImmutableSetWithValueEquality<T>
        CreateImmutableSetWithValueEquality<T>(ReadOnlySpan<T> readOnlySpan) =>
        new ImmutableSetWithValueEquality<T>([.. readOnlySpan]);

    public static IImmutableDictionaryWithValueEquality<TKey, TValue>
        CreateImmutableDictionaryWithValueEquality<TKey, TValue>(
            IImmutableDictionary<TKey, TValue> immutableDictionary,
            IEqualityComparer<TValue>? equalityComparer = null) where TKey : notnull =>
        new ImmutableDictionaryWithValueEquality<TKey, TValue>(
            underlying: immutableDictionary,
            equalityComparer: equalityComparer);

    public static IImmutableQueueWithValueEquality<T> ToImmutableQueueWithValueEquality<T>(
        this IImmutableQueue<T> immutableQueue,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableQueueWithValueEquality<T>(underlying: immutableQueue, equalityComparer: equalityComparer);

    public static IImmutableQueueWithValueEquality<T> CreateImmutableQueueWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableQueueWithValueEquality<T>(underlying: [.. readOnlySpan], equalityComparer: equalityComparer);

    public static IImmutableStackWithValueEquality<T> ToImmutableStackWithValueEquality<T>(
        this IImmutableStack<T> immutableStack,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableStackWithValueEquality<T>(underlying: immutableStack, equalityComparer: equalityComparer);

    public static IImmutableStackWithValueEquality<T> CreateImmutableStackWithValueEquality<T>(
        ReadOnlySpan<T> readOnlySpan,
        IEqualityComparer<T>? equalityComparer = null) =>
        new ImmutableStackWithValueEquality<T>(underlying: [.. readOnlySpan], equalityComparer: equalityComparer);

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

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateReadOnlyListWithValueEquality))]
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

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateFrozenListWithValueEquality))]
    private sealed class FrozenListWithValueEquality<T>(
        in ImmutableArray<T> immutableArray,
        in IEqualityComparer<T>? equalityComparer)
        : ReadOnlyListWithValueEquality<ImmutableArray<T>, T>(
                underlying: immutableArray,
                equalityComparer: equalityComparer),
            IFrozenListWithValueEquality<T>;

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateImmutableListWithValueEquality))]
    private sealed class ImmutableListWithValueEquality<T>(
        in IImmutableList<T> underlying,
        in IEqualityComparer<T>? equalityComparer)
        : ReadOnlyListWithValueEquality<IImmutableList<T>, T>(
                underlying: underlying,
                equalityComparer: equalityComparer),
            IImmutableListWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        private ImmutableListWithValueEquality<T> CreateNew(IImmutableList<T> immutableList) =>
            new(underlying: immutableList, equalityComparer: this.equalityComparer);

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
    }

    #endregion List Implementations

    #region Set Implementations

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateReadOnlySetWithValueEquality))]
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

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateFrozenSetWithValueEquality))]
    private sealed class FrozenSetWithValueEquality<T>(in FrozenSet<T> frozenSet)
        : ReadOnlySetWithValueEquality<T>(frozenSet),
            IFrozenSetWithValueEquality<T>;

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateImmutableSetWithValueEquality))]
    private sealed class ImmutableSetWithValueEquality<T>(in IImmutableSet<T> underlying)
        : ReadOnlyCollectionWithValueEqualityBase<IImmutableSet<T>, T>(underlying),
            IImmutableSetWithValueEquality<T>
    {
        private static ImmutableSetWithValueEquality<T> CreateNew(IImmutableSet<T> immutableSet) =>
            new(underlying: immutableSet);

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

        private ImmutableDictionaryWithValueEquality<TKey, TValue> CreateNew(
            IImmutableDictionary<TKey, TValue> immutableDictionary) =>
            new(underlying: immutableDictionary, equalityComparer: this.equalityComparer);

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
    }

    #endregion Dictionary Implementations

    #region Queue Implementations

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateReadOnlyQueueWithValueEquality))]
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

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateFrozenQueueWithValueEquality))]
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

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateImmutableQueueWithValueEquality))]
    private sealed class ImmutableQueueWithValueEquality<T>(
        in IImmutableQueue<T> underlying,
        in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<IImmutableQueue<T>, T>(underlying),
            IImmutableQueueWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        private ImmutableQueueWithValueEquality<T> CreateNew(IImmutableQueue<T> immutableQueue) =>
            new(underlying: immutableQueue, equalityComparer: this.equalityComparer);

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

        public override bool Equals(object? obj) =>
            obj is IReadOnlyQueueWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    #endregion Queue Implementations

    #region Stack Implementations

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateReadOnlyStackWithValueEquality))]
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

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateFrozenStackWithValueEquality))]
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

    [CollectionBuilder(
        builderType: typeof(ValueEqualityCollectionFactory),
        methodName: nameof(ValueEqualityCollectionFactory.CreateImmutableStackWithValueEquality))]
    private sealed class ImmutableStackWithValueEquality<T>(
        in IImmutableStack<T> underlying,
        in IEqualityComparer<T>? equalityComparer)
        : EnumerableWithValueEqualityBase<IImmutableStack<T>, T>(underlying),
            IImmutableStackWithValueEquality<T>
    {
        private readonly IEqualityComparer<T>? equalityComparer = equalityComparer;

        private ImmutableStackWithValueEquality<T> CreateNew(IImmutableStack<T> immutableStack) =>
            new(underlying: immutableStack, equalityComparer: this.equalityComparer);

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

        public override bool Equals(object? obj) =>
            obj is IReadOnlyStackWithValueEquality<T> c &&
            this.SequenceEqual(second: c, comparer: this.equalityComparer);

        public override int GetHashCode() => this.IsEmpty.GetHashCode();
    }

    #endregion Stack Implementations
}