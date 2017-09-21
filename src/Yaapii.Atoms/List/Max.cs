using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// The greatest item in the given <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Max<T> : IScalar<T>
        where T : IComparable<T>
    {
        private readonly IEnumerable<IScalar<T>> items;

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(params Func<T>[] items) : this(
            new Mapped<Func<T>, IScalar<T>>(
                new EnumerableOf<Func<T>>(items),
                item => new ScalarOf<T>(() => item.Invoke())))
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(IEnumerable<T> items) : this(
            new Mapped<T, IScalar<T>>(items, item => new ScalarOf<T>(item)))
        { }

        /// <summary>
        /// The greatest item in the given items.
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(params T[] items) : this(
            new Mapped<T, IScalar<T>>(items, item => new ScalarOf<T>(item)))
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(params IScalar<T>[] items) : this(new EnumerableOf<IScalar<T>>(items))
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(IEnumerable<IScalar<T>> items)
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

            T max = e.Current.Value();
            while (e.MoveNext())
            {
                T next = e.Current.Value();
                if (next.CompareTo(max) > 0)
                {
                    max = next;
                }
            }
            return max;
        }

    }
}
