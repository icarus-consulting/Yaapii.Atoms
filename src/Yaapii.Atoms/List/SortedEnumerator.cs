using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of items in enumertor</typeparam>
    public sealed class SortedEnumerator<T> : IEnumerator<T>
        where T : IComparable<T>
    {
        private readonly UncheckedScalar<IEnumerator<T>> _sorted;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerator to sort</param>
        public SortedEnumerator(Comparer<T> cmp, IEnumerator<T> src)
        {
            this._sorted =
                new UncheckedScalar<IEnumerator<T>>(
                    new StickyScalar<IEnumerator<T>>(
                        () =>
                        {
                            var items = new List<T>();
                            while (src.MoveNext())
                            {
                                items.Add(src.Current);
                            }
                            items.Sort(cmp);

                            return items.GetEnumerator();
                        }));
        }

        public void Dispose()
        { }

        public Boolean MoveNext()
        {
            return this._sorted.Value().MoveNext();
        }

        public T Current
        {
            get
            {
                return this._sorted.Value().Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this._sorted.Value().Current;
            }
        }

        public void Reset()
        {
            throw new NotSupportedException("#Reset() is not supported");
        }
    }
}
#pragma warning restore NoProperties // No Properties