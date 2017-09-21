using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Find the smallest item in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Min<T> : IScalar<T>
        where T : IComparable<T>
    {
        private readonly IEnumerable<IScalar<T>> items;

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items"><see cref="Func{TResult}"/> functions which retrieve items to compare</param>
        public Min(params Func<T>[] items) : this(
            new Mapped<Func<T>, IScalar<T>>(
                new EnumerableOf<Func<T>>(items),
                item => new ScalarOf<T>(() => item.Invoke())))
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<T> items) : this(
            new Mapped<T, IScalar<T>>(items, item => new ScalarOf<T>(item)))
        { }

        /// <summary>
        /// Find the smallest item in the given items
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params T[] items) : this(
            new Mapped<T, IScalar<T>>(items, item => new ScalarOf<T>(item)))
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{IScalar{T}}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<IScalar<T>> items)
        {
            this.items = items; //C# params Bug
        }

        /// <summary>
        /// Find the smallest item in the given scalars.
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params IScalar<T>[] items)
        {
            this.items = items;
        }

        public T Value()
        {
            IEnumerator<IScalar<T>> e = this.items.GetEnumerator();
            if (!e.MoveNext())
            {
                throw new NoSuchElementException("Can't find greater element in an empty iterable");
            }

            T min = e.Current.Value();
            while (e.MoveNext())
            {
                T next = e.Current.Value();
                if (next.CompareTo(min) < 0)
                {
                    min = next;
                }
            }
            return min;
        }

    }
}
