using System;
using System.Collections;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MorseCode.Collections.ValueEquality;

public static class ValueEqualityCollectionFactory
{
    public static IReadOnlyListWithValueEquality<T> CreateReadOnlyList<T>(IReadOnlyList<T> readOnlyList) =>
        new ReadOnlyListWithValueEquality<IReadOnlyList<T>, T>(readOnlyList);

    public static IReadOnlySetWithValueEquality<T> CreateReadOnlySet<T>(IReadOnlySet<T> readOnlySet) => null;

    public static IReadOnlyDictionaryWithValueEquality<TKey, TValue> CreateReadOnlyDictionary<TKey, TValue>(
        IReadOnlyDictionary<TKey, TValue> readOnlyDictionary) =>
        null;

    public static IReadOnlyQueueWithValueEquality<T> CreateReadOnlyQueue<T>(Queue<T> readOnlyQueue) => null;

    public static IReadOnlyStackWithValueEquality<T> CreateReadOnlyStack<T>(Stack<T> readOnlyStack) => null;

    public static IImmutableListWithValueEquality<T> CreateImmutableList<T>(IImmutableList<T> immutableList) =>
        new ImmutableListWithValueEquality<T>(immutableList);

    public static IImmutableSetWithValueEquality<T> CreateImmutableSet<T>(IImmutableSet<T> immutableSet) => null;

    public static IImmutableDictionaryWithValueEquality<TKey, TValue> CreateImmutableDictionary<TKey, TValue>(
        IImmutableDictionary<TKey, TValue> immutableDictionary) =>
        null;

    public static IImmutableQueueWithValueEquality<T> CreateImmutableQueue<T>(Queue<T> immutableQueue) => null;

    public static IImmutableStackWithValueEquality<T> CreateImmutableStack<T>(Stack<T> immutableStack) => null;

    public static IFrozenListWithValueEquality<T> CreateFrozenList<T>(IEnumerable<T> enumerable) =>
        new FrozenListWithValueEquality<T>([.. enumerable]);

    public static IFrozenSetWithValueEquality<T> CreateFrozenSet<T>(FrozenSet<T> frozenSet) => null;

    public static IFrozenDictionaryWithValueEquality<TKey, TValue> CreateFrozenDictionary<TKey, TValue>(
        FrozenDictionary<TKey, TValue> frozenDictionary) where TKey : notnull =>
        null;

    public static IFrozenQueueWithValueEquality<T> CreateFrozenQueue<T>(IEnumerable<T> enumerable) => null;

    public static IFrozenStackWithValueEquality<T> CreateFrozenStack<T>(IEnumerable<T> enumerable) => null;

    private abstract class ReadOnlyCollectionWithValueEqualityBase<TCollection, T>(in TCollection underlying)
        : IReadOnlyCollection<T>
        where TCollection : IReadOnlyCollection<T>
    {
        protected readonly TCollection Underlying = underlying ?? throw new ArgumentNullException(nameof(underlying));

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.Underlying.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Underlying).GetEnumerator();

        int IReadOnlyCollection<T>.Count => this.Underlying.Count;

        /// <inheritdoc />
        public override int GetHashCode() => this.Underlying.Count;
    }

    private class ReadOnlyListWithValueEquality<TCollection, T>(in TCollection underlying)
        : ReadOnlyCollectionWithValueEqualityBase<TCollection, T>(underlying),
            IReadOnlyListWithValueEquality<T> where TCollection : IReadOnlyList<T>
    {
        T IReadOnlyList<T>.this[int index] => this.Underlying[index];

        /// <inheritdoc />
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
            obj is IReadOnlyListWithValueEquality<T> c && this.SequenceEqual(c);
    }

    private sealed class ImmutableListWithValueEquality<T>(in IImmutableList<T> underlying)
        : ReadOnlyListWithValueEquality<IImmutableList<T>, T>(underlying), IImmutableListWithValueEquality<T>
    {
        private static ImmutableListWithValueEquality<T> CreateNew(IImmutableList<T> immutableList) =>
            new(immutableList);

        public IImmutableListWithValueEquality<T> Add(T value) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.Add(value));

        public IImmutableListWithValueEquality<T> AddRange(IEnumerable<T> items) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.AddRange(items));

        public IImmutableListWithValueEquality<T> Clear() =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.Clear());

        public IImmutableListWithValueEquality<T> Insert(int index, T element) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.Insert(index: index, element: element));

        public IImmutableListWithValueEquality<T> InsertRange(int index, IEnumerable<T> items) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.InsertRange(index: index, items: items));

        public IImmutableListWithValueEquality<T> Remove(T value, IEqualityComparer<T>? equalityComparer) =>
            ImmutableListWithValueEquality<T>.CreateNew(
                this.Underlying.Remove(value: value, equalityComparer: equalityComparer));

        public IImmutableListWithValueEquality<T> RemoveAll(Predicate<T> match) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.RemoveAll(match));

        public IImmutableListWithValueEquality<T> RemoveAt(int index) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.RemoveAt(index));

        public IImmutableListWithValueEquality<T> RemoveRange(
            IEnumerable<T> items,
            IEqualityComparer<T>? equalityComparer) =>
            ImmutableListWithValueEquality<T>.CreateNew(
                this.Underlying.RemoveRange(items: items, equalityComparer: equalityComparer));

        public IImmutableListWithValueEquality<T> RemoveRange(int index, int count) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.RemoveRange(index: index, count: count));

        public IImmutableListWithValueEquality<T> Replace(
            T oldValue,
            T newValue,
            IEqualityComparer<T>? equalityComparer) =>
            ImmutableListWithValueEquality<T>.CreateNew(
                this.Underlying.Replace(oldValue: oldValue, newValue: newValue, equalityComparer: equalityComparer));

        public IImmutableListWithValueEquality<T> SetItem(int index, T value) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.SetItem(index: index, value: value));

        public IImmutableListWithValueEquality<T> Replace(T oldValue, T newValue) =>
            ImmutableListWithValueEquality<T>.CreateNew(
                this.Underlying.Replace(oldValue: oldValue, newValue: newValue));

        public IImmutableListWithValueEquality<T> Remove(T value) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.Remove(value));

        public IImmutableListWithValueEquality<T> RemoveRange(IEnumerable<T> items) =>
            ImmutableListWithValueEquality<T>.CreateNew(this.Underlying.RemoveRange(items));

        public int IndexOf(T item) => this.Underlying.IndexOf(item);

        public int IndexOf(T item, IEqualityComparer<T>? equalityComparer) =>
            this.Underlying.IndexOf(item: item, equalityComparer: equalityComparer);

        public int IndexOf(T item, int startIndex) => this.Underlying.IndexOf(item: item, startIndex: startIndex);

        public int IndexOf(T item, int startIndex, int count) =>
            this.Underlying.IndexOf(item: item, startIndex: startIndex, count: count);

        public int LastIndexOf(T item) => this.Underlying.LastIndexOf(item);

        public int LastIndexOf(T item, IEqualityComparer<T>? equalityComparer) =>
            this.Underlying.LastIndexOf(item: item, equalityComparer: equalityComparer);

        public int LastIndexOf(T item, int startIndex) =>
            this.Underlying.LastIndexOf(item: item, startIndex: startIndex);

        public int LastIndexOf(T item, int startIndex, int count) =>
            this.Underlying.LastIndexOf(item: item, startIndex: startIndex, count: count);

        IImmutableList<T> IImmutableList<T>.Add(T value) => this.Add(value);

        IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items) => this.AddRange(items);

        IImmutableList<T> IImmutableList<T>.Clear() => this.Clear();

        int IImmutableList<T>.IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer) =>
            this.Underlying.IndexOf(item: item, index: index, count: count, equalityComparer: equalityComparer);

        IImmutableList<T> IImmutableList<T>.Insert(int index, T element) => this.Insert(index: index, element: element);

        IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items) =>
            this.InsertRange(index: index, items: items);

        int IImmutableList<T>.LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer) =>
            this.Underlying.LastIndexOf(item: item, index: index, count: count, equalityComparer: equalityComparer);

        IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T>? equalityComparer) =>
            this.Remove(value: value, equalityComparer: equalityComparer);

        IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match) => this.RemoveAll(match);

        IImmutableList<T> IImmutableList<T>.RemoveAt(int index) => this.RemoveAt(index);

        IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer) =>
            this.RemoveRange(items: items, equalityComparer: equalityComparer);

        IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count) =>
            this.RemoveRange(index: index, count: count);

        IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer) =>
            this.Replace(oldValue: oldValue, newValue: newValue, equalityComparer: equalityComparer);

        IImmutableList<T> IImmutableList<T>.SetItem(int index, T value) => this.SetItem(index: index, value: value);
    }

    private class FrozenListWithValueEquality<T>(in ImmutableArray<T> immutableArray)
        : ReadOnlyListWithValueEquality<ImmutableArray<T>, T>(immutableArray), IFrozenListWithValueEquality<T>;
}