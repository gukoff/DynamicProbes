using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace UnitTests;

static class EquatableArray
{
    public static EquatableArray<T> Create<T>(ReadOnlySpan<T> items)
        where T : IEquatable<T> => new([..items]);
}

/// <summary>
/// A thin wrapper around an <see cref="ImmutableArray{T}"/> that requires the
/// array items to be equatable and consequently provides value equality
/// semantics for entire array; this is, two arrays are equal if they have the
/// same length and all their items are equal.
/// </summary>
[DebuggerDisplay("Length = {Length}")]
[CollectionBuilder(typeof(EquatableArray), nameof(EquatableArray.Create))]
readonly record struct EquatableArray<T> : IReadOnlyList<T>, IList<T>
    where T : IEquatable<T>
{
    readonly ImmutableArray<T> items;

    public EquatableArray(ImmutableArray<T> items) => this.items = items;

    public static implicit operator EquatableArray<T>(ImmutableArray<T> items) => new(items);

    public int Length => this.items.IsDefaultOrEmpty ? 0 : this.items.Length;

    public T this[int index]
    {
        get
        {
            if (this.items.IsDefaultOrEmpty)
                ThrowIndexOutOfRangeException();
            return this.items[index];
        }
    }

    [DoesNotReturn]
    static void ThrowIndexOutOfRangeException() =>
#pragma warning disable CA2201 // Do not raise reserved exception types (thrown from an indexer)
        throw new IndexOutOfRangeException();
#pragma warning restore CA2201 // Do not raise reserved exception types

    int IList<T>.IndexOf(T item) =>
        !this.items.IsDefaultOrEmpty ? this.items.IndexOf(item) : -1;

    public bool Contains(T item) =>
        !this.items.IsDefaultOrEmpty && this.items.Contains(item);

    public bool Equals(EquatableArray<T> other) =>
        this.items.SequenceEqual(other.items);

    public override int GetHashCode()
    {
        if (this.items.IsDefaultOrEmpty)
            return 0;

        var hash = new HashCode();
        foreach (var item in this.items)
            hash.Add(item);

        return hash.ToHashCode();
    }

    public Enumerator GetEnumerator() => new(this.items);

    public override string ToString() => $"[{string.Join(", ", this)}]";

    T IList<T>.this[int index]
    {
        get => this[index];
        set => throw new NotSupportedException();
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) =>
        this.items.CopyTo(array, arrayIndex);

    int ICollection<T>.Count => Length;
    int IReadOnlyCollection<T>.Count => Length;
    bool ICollection<T>.IsReadOnly => true;

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator(ImmutableArray<T> items) : IEnumerator<T>
    {
        int index = 0;

        public T Current { get; private set; }

        readonly object? IEnumerator.Current => Current;

        public readonly void Dispose() { }

        public bool MoveNext()
        {
            if (items.IsDefaultOrEmpty || this.index >= items.Length)
                return false;

            Current = items[this.index++];
            return true;
        }

        public void Reset() => this.index = 0;
    }

    // Unsupported members

    void ICollection<T>.Add(T item) => throw new NotSupportedException();
    void ICollection<T>.Clear() => throw new NotSupportedException();
    bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

    void IList<T>.Insert(int index, T item) => throw new NotSupportedException();
    void IList<T>.RemoveAt(int index) => throw new NotSupportedException();
}
