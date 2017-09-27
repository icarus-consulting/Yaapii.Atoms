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

        /// <summary>
        /// Get the item.
        /// </summary>
        /// <returns>the item</returns>
        public T Value()
        {
            return new ItemAtEnumerator<T>(
                this.src.GetEnumerator(), this.pos, this.fbk
            ).Value();
        }
    }
}
