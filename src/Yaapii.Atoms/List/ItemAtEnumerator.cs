// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;
using Yaapii.Atoms.Fail;

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
                        throw 
                            new NoSuchElementException(
                                new FormattedText(
                                    "Enumerator doesn't have an element at position {0}",
                                    pos
                                ).AsString());
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

        /// <summary>
        /// Get the item.
        /// </summary>
        /// <returns>the item</returns>
        public T Value()
        {
            new FailPrecise(
                new FailWhen(this._pos < 0),
                new UnsupportedOperationException(
                    new FormattedText("The position must be non-negative but is {0}",
                        this._pos).AsString())).Go();
            T ret;
            try
            {
                new FailPrecise(
                    new FailWhen(!this._src.MoveNext()),
                    new NoSuchElementException(
                "Cannot get item because enumerable is empty")).Go(); //will never get out

                this._src.Reset();

                for (int cur = 0; cur <= this._pos && this._src.MoveNext(); ++cur) { }

                ret = this._src.Current;
            }
            catch (Exception)
            {
                ret = this._fallback.Invoke(new EnumerableOf<T>(this._src));
            }
            return ret;

        }
    }
}
