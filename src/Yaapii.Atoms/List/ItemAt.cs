using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of element</typeparam>
    public sealed class ItemAt<T> : IScalar<T>
    {
        /// <summary>
        /// source enum
        /// </summary>
        private readonly IEnumerable<T> src;

        /// <summary>
        /// fallback func
        /// </summary>
        private readonly IFunc<IEnumerable<T>, T> fbk;

        /// <summary>
        /// position
        /// </summary>
        private readonly int pos;

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public ItemAt(IEnumerable<T> source) : this(
                source,
                new FuncOf<IEnumerable<T>, T>(itr => { throw new IOException("The iterable is empty"); }))
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, T fallback) : this(source, new FuncOf<IEnumerable<T>, T>(b => fallback))
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, T fallback) : this(source, new FuncOf<IEnumerable<T>, T>(b => fallback))
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public ItemAt(IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(source, 0, fallback)
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        public ItemAt(IEnumerable<T> source, int position) : this(
                source,
                position,
                new FuncOf<IEnumerable<T>, T>(itr =>
                {
                    throw new IOException(
                        new FormattedText(
                            "The iterable doesn't have the position #%d",
                            position
                        ).AsString()
                    );
                }))
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, Func<IEnumerable<T>, T> fallback) : this(source, position, new FuncOf<IEnumerable<T>, T>(fallback))
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, IFunc<IEnumerable<T>, T> fallback)
        {
            this.pos = position;
            this.src = source;
            this.fbk = fallback;
        }

        public T Value()
        {
            return new ItemAtEnumerator<T>(
                this.src.GetEnumerator(), this.pos, this.fbk
            ).Value();
        }
    }
}
