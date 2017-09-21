using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ItemAtEnumerator<T> : IScalar<T>
    {
        /// <summary>
        /// enumerator to get item from
        /// </summary>
        private readonly IEnumerator<T> _src;

        /// <summary>
        /// fallback function for alternative value
        /// </summary>
        private readonly IFunc<IEnumerable<T>, T> _fallback;

        /// <summary>
        /// position of the item
        /// </summary>
        private readonly int _pos;

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        public ItemAtEnumerator(IEnumerator<T> src)
        :
            this(
                src,
                new FuncOf<IEnumerable<T>, T>(
                    (e) =>
                    {
                        throw new IOException("Enumerator is empty");
                    })
                )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="fallback">fallback value</param>
        public ItemAtEnumerator(IEnumerator<T> src, T fallback) : this(src, new FuncOf<IEnumerable<T>, T>((e) => fallback))
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="fallback">fallback function</param>
        public ItemAtEnumerator(IEnumerator<T> src, Func<IEnumerable<T>, T> fallback)
            : this(src, 0, new FuncOf<IEnumerable<T>, T>(fallback))
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="fallback">fallback function</param>
        public ItemAtEnumerator(IEnumerator<T> src, IFunc<IEnumerable<T>, T> fallback)
            : this(src, 0, fallback)
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="pos">position</param>
        public ItemAtEnumerator(IEnumerator<T> src, int pos)
        :
            this(
                src,
                pos,
                new FuncOf<IEnumerable<T>, T>(
                    (itr) =>
                    {
                        throw new IOException(
                            new FormattedText(
                                "Enumerator doesn't have an element at #%d position",
                                pos
                            ).AsString()
                        );
                    }
            ))
        { }

        /// <summary>
        /// Element at position in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="fbk">fallback function</param>
        /// <param name="pos">position</param>
        public ItemAtEnumerator(
            IEnumerator<T> src,
            int pos,
            IFunc<IEnumerable<T>, T> fbk
        )
        {
            this._pos = pos;
            this._src = src;
            this._fallback = fbk;
        }

        public T Value()
        {
            new FailPrecise(
                new FailWhen(() => this._pos < 0),
                new IOException(
                    new FormattedText("The position must be non-negative: %d",
                        this._pos).AsString())).Go();

            T ret;

            for (int cur = 0; cur <= this._pos && this._src.MoveNext(); ++cur) { }

            try
            {
                ret = this._src.Current;
            }
            catch (Exception ex)
            {
                ret = this._fallback.Invoke(new EnumerableOf<T>(this._src));
            }
            return ret;

        }
    }
}
