using System;
using System.Collections.Generic;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IList{T}"/> sorted by the given comparison .
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Sorted<T> : ListEnvelope<T>
    {
        /// <summary>
        /// A <see cref="IList{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(params T[] src) : this(Comparer<T>.Default, new List<T>(src))
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(bool live, params T[] src) : this(Comparer<T>.Default, new List<T>(src), live)
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IEnumerable<T> src, bool live = false) : this(Comparer<T>.Default, new List<T>(src), live)
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Func<T, T, int> comparison, IEnumerable<T> src, bool live = false) : this(
            Comparer<T>.Create(
                new Comparison<T>((left, right) => comparison(left, right))
            ),
            new List<T>(src),
            live
        )
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(List<T> src, bool live = false) : this(Comparer<T>.Default, src, live)
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IList<T> src, bool live = false) : this(Comparer<T>.Default, src, live)
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Func<T, T, int> comparison, IList<T> src, bool live = false) : this(
            Comparer<T>.Create(
                new Comparison<T>((left, right) => comparison(left, right))
            ),
            src,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Func<T, T, int> comparison, List<T> src, bool live = false) : this(
            Comparer<T>.Create(
                new Comparison<T>((left, right) => comparison(left, right))
            ),
            src,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IComparer<T> cmp, IEnumerable<T> src, bool live = false) : this(
            cmp, new List<T>(src), live
        )
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IComparer<T> cmp, IList<T> src, bool live = false) : this(
            cmp, new List<T>(src), live
        )
        { }

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IComparer<T> cmp, List<T> src, bool live = false) : base(() =>
            {
                src.Sort(cmp);
                return src;
            },
            live
        )
        {
        }
    }

    /// <summary>
    /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public static class Sorted
    {
        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(IEnumerable<T> src) =>
            new Sorted<T>(src);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(IEnumerable<T> src, bool live = false) =>
            new Sorted<T>(src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(IComparer<T> cmp, IEnumerable<T> src, bool live = false) =>
            new Sorted<T>(cmp, src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given comparison function />.
        /// </summary>
        /// <param name="comparison">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(Func<T, T, int> comparison, IEnumerable<T> src, bool live = false) =>
            new Sorted<T>(comparison, src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(IList<T> src, bool live = false) =>
            new Sorted<T>(src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(IComparer<T> cmp, IList<T> src, bool live = false) =>
            new Sorted<T>(cmp, src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given comparison function />.
        /// </summary>
        /// <param name="comparison">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(Func<T, T, int> comparison, IList<T> src, bool live) =>
            new Sorted<T>(comparison, src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(List<T> src, bool live = false) =>
            new Sorted<T>(src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(IComparer<T> cmp, List<T> src, bool live = false) =>
            new Sorted<T>(cmp, src, live);

        /// <summary>
        /// A <see cref="IList{T}"/> sorted by the given comparison function />.
        /// </summary>
        /// <param name="comparison">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IList<T> New<T>(Func<T, T, int> comparison, List<T> src, bool live) =>
            new Sorted<T>(comparison, src, live);
    }
}

