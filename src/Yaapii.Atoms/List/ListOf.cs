using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable NoProperties // No Properties
#pragma warning disable MaxClassLength // Class length max

/// <summary>
/// MTU:
/// Removed 13092017
/// Reason: We dont need this class, since it cannot prevent mutability for sure. User can cast it to List 
/// and then it will use the list methods instead of these here. Coders will get confused.
/// </summary>



namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Iterable as {@link List}.
    ///
    /// This class should be used very carefully. You must understand that
    /// it will fetch the entire content of the encapsulated {@link List} on each
    /// method call. It doesn't cache the data anyhow.
    ///
    /// If you don't need this {@link List} to re-fresh its content on every call,
    /// by doing round-trips to the encapsulated iterable, use
    /// StickyList.
    ///
    /// There is no thread-safety guarantee.
    ///
    /// @Author MTU (ported)
    /// </summary>
    //public sealed class ListOf<T> : List<T>
    //{
    //    private readonly Scalar<IEnumerable<T>> _enumerable;

    //    public ListOf(List<T> lst) : this(new ScalarOf<IEnumerable<T>>(() => new EnumerableOf<T>(lst.GetEnumerator())))
    //    { }

    //    public ListOf(IEnumerator<T> enumerator) : this(new EnumerableOf<T>(enumerator))
    //    { }

    //    public ListOf(params T[] array) : this(new EnumerableOf<T>(array))
    //    { }

    //    public ListOf(IEnumerable<T> src) : this(new ScalarOf<IEnumerable<T>>(src))
    //    { }

    //    /// <summary>
    //    /// Use StickyList if you need a cached list (Performance).
    //    /// </summary>
    //    /// <param name="src"></param>
    //    private ListOf(Scalar<IEnumerable<T>> src)
    //    {
    //        this._enumerable = src;
    //    }

    //    public T this[int index]
    //    {
    //        get
    //        {
    //            return this.List()[index];
    //        }
    //        set
    //        {
    //            throw new UnsupportedOperationException("#[<index>]= is not supported");
    //        }
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            return new LengthOf<T>(this._enumerable.Value()).Value();
    //        }
    //    }

    //    public int Capacity
    //    {
    //        get
    //        {
    //            throw new UnsupportedOperationException("#getcapacity is not supported");
    //        }
    //    }

    //    public bool Add(T item)
    //    {
    //        throw new UnsupportedOperationException("#add(T element) is not supported");
    //    }

    //    public bool AddRange(IEnumerable<T> collection)
    //    {
    //        throw new UnsupportedOperationException("#addRange(final Collection<? extends T> collection) is not supported");
    //    }

    //    public IReadOnlyCollection<T> AsReadOnly()
    //    {
    //        return List().AsReadOnly();
    //    }

    //    public int BinarySearch(T item)
    //    {
    //        return List().BinarySearch(item);
    //    }

    //    public int BinarySearch(T item, IComparer<T> comparer)
    //    {
    //        return List().BinarySearch(item, comparer);
    //    }

    //    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    //    {
    //        return List().BinarySearch(index, count, item, comparer);
    //    }

    //    public void Clear()
    //    {
    //        throw new UnsupportedOperationException(
    //            "#clear() is not supported"
    //        );
    //    }

    //    public bool Contains(T obj)
    //    {
    //        return this.List().Contains(obj);
    //    }

    //    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    //    {
    //        this.List().CopyTo(index, array, arrayIndex, count);
    //    }

    //    public void CopyTo(T[] array, int arrayIndex)
    //    {
    //        this.List().CopyTo(array, arrayIndex);
    //    }

    //    public void CopyTo(T[] array)
    //    {
    //        this.List().CopyTo(array);
    //    }

    //    public bool Exists(Predicate<T> match)
    //    {
    //        return this.List().Exists(match);
    //    }
    //    public T Find(Predicate<T> match)
    //    {
    //        return this.List().Find(match);
    //    }

    //    public IEnumerable<T> FindAll(Predicate<T> match)
    //    {
    //        return new ListOf<T>(this.List().FindAll(match).GetEnumerator());
    //    }

    //    public int FindIndex(int startIndex, int count, Predicate<T> match)
    //    {
    //        return this.List().FindIndex(startIndex, count, match);
    //    }

    //    public int FindIndex(int startIndex, Predicate<T> match)
    //    {
    //        return this.List().FindIndex(startIndex, match);
    //    }

    //    public int FindIndex(Predicate<T> match)
    //    {
    //        return this.List().FindIndex(match);
    //    }

    //    public T FindLast(Predicate<T> match)
    //    {
    //        return this.List().FindLast(match);
    //    }

    //    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    //    {
    //        return this.List().FindLastIndex(startIndex, count, match);
    //    }

    //    public int FindLastIndex(int startIndex, Predicate<T> match)
    //    {
    //        return this.List().FindLastIndex(startIndex, match);
    //    }

    //    public int FindLastIndex(Predicate<T> match)
    //    {
    //        return this.List().FindLastIndex(match);
    //    }

    //    public void ForEach(Action<T> action)
    //    {
    //        this.List().ForEach(action);
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return this._enumerable.Value().GetEnumerator();
    //    }

    //    public IEnumerable<T> GetRange(int index, int count)
    //    {
    //        return new ListOf<T>(this.List().GetRange(index, count));
    //    }

    //    public int IndexOf(T item, int index, int count)
    //    {
    //        return this.List().IndexOf(item, index, count);
    //    }

    //    public int IndexOf(T item, int index)
    //    {
    //        return this.List().IndexOf(item, index);
    //    }

    //    public int IndexOf(T item)
    //    {
    //        return this.List().IndexOf(item);
    //    }

    //    public void Insert(int index, T element)
    //    {
    //        throw new UnsupportedOperationException(
    //            "#insert(final int index, final T element) is not supported"
    //        );
    //    }

    //    public void InsertRange(int index, IEnumerable<T> collection)
    //    {
    //        throw new UnsupportedOperationException(
    //            "#insertRange(final int index, final T element) is not supported"
    //        );
    //    }

    //    public int LastIndexOf(T item, int index, int count)
    //    {
    //        return this.List().LastIndexOf(item, index, count);
    //    }

    //    public int LastIndexOf(T item, int index)
    //    {
    //        return this.List().LastIndexOf(item, index);
    //    }

    //    public int LastIndexOf(T item)
    //    {
    //        return this.List().LastIndexOf(item);
    //    }

    //    public bool Remove(T item)
    //    {
    //        throw new UnsupportedOperationException("#Remove(T item) is not supported");
    //    }

    //    public bool RemoveAll(Predicate<T> match)
    //    {
    //        throw new UnsupportedOperationException("#RemoveAll(T item) is not supported");
    //    }

    //    public T RemoveAt(int index)
    //    {
    //        throw new UnsupportedOperationException("#RemoveAt(int index) is not supported");
    //    }

    //    public T RemoveRange(int index, int count)
    //    {
    //        throw new UnsupportedOperationException("#RemoveRange(int index, int count) is not supported");
    //    }

    //    public T Reverse(int index, int count)
    //    {
    //        throw new UnsupportedOperationException("#Reverse(int index, int count) is not supported");
    //    }

    //    public T Reverse()
    //    {
    //        throw new UnsupportedOperationException("#Reverse() is not supported");
    //    }

    //    public void Sort(Comparison<T> comparison)
    //    {
    //        throw new UnsupportedOperationException("#Sort(Comparison<T> comparison) is not supported");
    //    }

    //    public void Sort(int index, int count, IComparer<T> comparer)
    //    {
    //        throw new UnsupportedOperationException("#Sort(int index, int count, IComparer<T> comparer) is not supported");
    //    }

    //    public void Sort()
    //    {
    //        throw new UnsupportedOperationException("#Sort() is not supported");
    //    }

    //    public void Sort(IComparer<T> comparer)
    //    {
    //        throw new UnsupportedOperationException("#Sort(IComparer<T> comparer) is not supported");
    //    }

    //    public void TrimExcess()
    //    {
    //        throw new UnsupportedOperationException("#TrimExcess() is not supported");
    //    }

    //    public bool TrueForAll(Predicate<T> match)
    //    {
    //        return this.List().TrueForAll(match);
    //    }

    //    public T[] ToArray()
    //    {
    //        return this.List().ToArray();
    //    }

    //    /**
    //     * Build a list.
    //     * @return List
    //     */

    //    private List<T> List()
    //    {
    //        var list = new List<T>();
    //        foreach (T item in this._enumerable.Value())
    //        {
    //            list.Add(item);
    //        }
    //        return list;
    //    }

    //}
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore NoGetOrSet // No Statics
#pragma warning restore NoProperties // No Properties
#pragma warning restore MaxClassLength // Class length max