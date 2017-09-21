using System;
using System.Collections.Generic;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
#pragma warning disable MaxPublicMethodCount // a public methods count maximum
#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable NoProperties // No Properties
#pragma warning disable MaxClassLength // Class length max

    /// <summary>
    /// A <see cref="List{X}"/> which returns the same items from a cache, always.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class StickyList<X> : List<X>
    {
        private readonly UncheckedScalar<List<X>> _gate;

        /// <summary>
        /// A <see cref="List{X}"/> which returns the same items from a cache, always.
        /// </summary>
        /// <param name="items">items to cache</param>
        public StickyList(params X[] items) :
            this(new List<X>(new List<X>(items)))
        { }

        /// <summary>
        /// A <see cref="List{X}"/> which returns the same items from a cache, always.
        /// </summary>
        /// <param name="list">list to cache</param>
        public StickyList(List<X> list)
        {
            this._gate =
                new UncheckedScalar<List<X>>(
                    new StickyScalar<List<X>>(
                        () =>
                            {
                                var temp = new List<X>();
                                foreach (var item in list)
                                {
                                    temp.Add(item);
                                }
                                return temp;
                            }));
        }

        public new X this[int index]
        {
            get
            {
                return this._gate.Value()[index];
            }
            set
            {
                throw new UnsupportedOperationException("#[<index>]= is not supported");
            }
        }

        public new int Count
        {
            get
            {
                return this._gate.Value().Count;
            }
        }

        public new int Capacity
        {
            get
            {
                throw new UnsupportedOperationException("#getcapacity is not supported");
            }
        }

        public new bool Add(X item)
        {
            throw new UnsupportedOperationException("#Add(T element) is not supported");
        }

        public new bool AddRange(IEnumerable<X> items)
        {
            throw new UnsupportedOperationException("#AddRange(List<? extends T> items) is not supported");
        }

        public new IReadOnlyCollection<X> AsReadOnly()
        {
            return this._gate.Value().AsReadOnly();
        }

        public new int BinarySearch(X item)
        {
            return this._gate.Value().BinarySearch(item);
        }

        public new int BinarySearch(X item, IComparer<X> comparer)
        {
            return this._gate.Value().BinarySearch(item, comparer);
        }

        public new int BinarySearch(int index, int count, X item, IComparer<X> comparer)
        {
            return this._gate.Value().BinarySearch(index, count, item, comparer);
        }

        public new void Clear()
        {
            throw new UnsupportedOperationException(
                "#Clear() is not supported"
            );
        }

        public new bool Contains(X obj)
        {
            return this._gate.Value().Contains(obj);
        }

        public new void CopyTo(int index, X[] array, int arrayIndex, int count)
        {
            this._gate.Value().CopyTo(index, array, arrayIndex, count);
        }

        public new void CopyTo(X[] array, int arrayIndex)
        {
            this._gate.Value().CopyTo(array, arrayIndex);
        }

        public new void CopyTo(X[] array)
        {
            this._gate.Value().CopyTo(array);
        }

        public new bool Exists(Predicate<X> match)
        {
            return this._gate.Value().Exists(match);
        }
        public new X Find(Predicate<X> match)
        {
            return this._gate.Value().Find(match);
        }

        public new IEnumerable<X> FindAll(Predicate<X> match)
        {
            return this._gate.Value().FindAll(match);
        }

        public new int FindIndex(int startIndex, int count, Predicate<X> match)
        {
            return this._gate.Value().FindIndex(startIndex, count, match);
        }

        public new int FindIndex(int startIndex, Predicate<X> match)
        {
            return this._gate.Value().FindIndex(startIndex, match);
        }

        public new int FindIndex(Predicate<X> match)
        {
            return this._gate.Value().FindIndex(match);
        }

        public new X FindLast(Predicate<X> match)
        {
            return this._gate.Value().FindLast(match);
        }

        public new int FindLastIndex(int startIndex, int count, Predicate<X> match)
        {
            return this._gate.Value().FindLastIndex(startIndex, count, match);
        }

        public new int FindLastIndex(int startIndex, Predicate<X> match)
        {
            return this._gate.Value().FindLastIndex(startIndex, match);
        }

        public new int FindLastIndex(Predicate<X> match)
        {
            return this._gate.Value().FindLastIndex(match);
        }

        public new void ForEach(Action<X> action)
        {
            this._gate.Value().ForEach(action);
        }

        public new IEnumerator<X> GetEnumerator()
        {
            return this._gate.Value().GetEnumerator();
        }

        public new List<X> GetRange(int index, int count)
        {
            return new List<X>(this._gate.Value().GetRange(index, count));
        }

        public new int IndexOf(X item, int index, int count)
        {
            return this._gate.Value().IndexOf(item, index, count);
        }

        public new int IndexOf(X item, int index)
        {
            return this._gate.Value().IndexOf(item, index);
        }

        public new int IndexOf(X item)
        {
            return this._gate.Value().IndexOf(item);
        }

        public new void Insert(int index, X element)
        {
            throw new UnsupportedOperationException(
                "#insert(final int index, final T element) is not supported"
            );
        }

        public new void InsertRange(int index, IEnumerable<X> collection)
        {
            throw new UnsupportedOperationException(
                "#insertRange(final int index, final T element) is not supported"
            );
        }

        public new int LastIndexOf(X item, int index, int count)
        {
            return this._gate.Value().LastIndexOf(item, index, count);
        }

        public new int LastIndexOf(X item, int index)
        {
            return this._gate.Value().LastIndexOf(item, index);
        }

        public new int LastIndexOf(X item)
        {
            return this._gate.Value().LastIndexOf(item);
        }

        public new bool Remove(X item)
        {
            throw new UnsupportedOperationException("#Remove(T item) is not supported");
        }

        public new bool RemoveAll(Predicate<X> match)
        {
            throw new UnsupportedOperationException("#RemoveAll(T item) is not supported");
        }

        public new X RemoveAt(int index)
        {
            throw new UnsupportedOperationException("#RemoveAt(int index) is not supported");
        }

        public new X RemoveRange(int index, int count)
        {
            throw new UnsupportedOperationException("#RemoveRange(int index, int count) is not supported");
        }

        public new X Reverse(int index, int count)
        {
            throw new UnsupportedOperationException("#Reverse(int index, int count) is not supported");
        }

        public new X Reverse()
        {
            throw new UnsupportedOperationException("#Reverse() is not supported");
        }

        public new void Sort(Comparison<X> comparison)
        {
            throw new UnsupportedOperationException("#Sort(Comparison<T> comparison) is not supported");
        }

        public new void Sort(int index, int count, IComparer<X> comparer)
        {
            throw new UnsupportedOperationException("#Sort(int index, int count, IComparer<T> comparer) is not supported");
        }

        public new void Sort()
        {
            throw new UnsupportedOperationException("#Sort() is not supported");
        }

        public new void Sort(IComparer<X> comparer)
        {
            throw new UnsupportedOperationException("#Sort(IComparer<T> comparer) is not supported");
        }

        public new void TrimExcess()
        {
            throw new UnsupportedOperationException("#TrimExcess() is not supported");
        }

        public new bool TrueForAll(Predicate<X> match)
        {
            return this._gate.Value().TrueForAll(match);
        }

        public new X[] ToArray()
        {
            return this._gate.Value().ToArray();
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore NoGetOrSet // No Statics
#pragma warning restore NoProperties // No Properties
#pragma warning restore MaxClassLength // Class length max

