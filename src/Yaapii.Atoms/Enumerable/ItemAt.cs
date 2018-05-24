﻿// MIT License
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
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Enumerable
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
        private readonly IEnumerable<T> _src;

        /// <summary>
        /// fallback func
        /// </summary>
        private readonly IBiFunc<Exception, IEnumerable<T>, T> _fbk;

        /// <summary>
        /// position
        /// </summary>
        private readonly int _pos;

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with given Exception thrwon on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public ItemAt(IEnumerable<T> source, Exception ex) : this(source, 0, ex)
        { }

        /// <summary>
        /// Element at position in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="position"></param>
        /// <param name="ex"></param>
        public ItemAt(IEnumerable<T> source, int position, Exception ex) : this(
            source,
            position,
            new FuncOf<IEnumerable<T>, T>(
                (itr) => throw ex
            )
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public ItemAt(IEnumerable<T> source) : this(
                source,
                new BiFuncOf<Exception, IEnumerable<T>, T>(
                    (ex, itr) => throw new NoSuchElementException(new FormattedText("Cannot get first element: {0}", ex.Message).AsString())))
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, T fallback) : this(
            source, 
            new FuncOf<IEnumerable<T>, T>(
                b => fallback))
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, T fallback) : this(
            source, 
            new FuncOf<IEnumerable<T>, T>(b => fallback))
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public ItemAt(IEnumerable<T> source, IBiFunc<Exception, IEnumerable<T>, T> fallback) : this(
            source, 0, fallback)
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public ItemAt(IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(
            source, 0, fallback)
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        public ItemAt(IEnumerable<T> source, int position) : this(
                source,
                position,
                new BiFuncOf<Exception, IEnumerable<T>, T>((ex, itr) =>
                {
                    throw 
                        new NoSuchElementException(
                            new FormattedText(
                                "Cannot get element at position {0}: {1}",
                                position,
                                ex.Message
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
        public ItemAt(IEnumerable<T> source, int position, IFunc<IEnumerable<T>, T> fallback) : this(
            source,
            position,
            (ex, enumerable) => fallback.Invoke(enumerable))
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, Func<IEnumerable<T>, T> fallback) : this(
            source, 
            position,
            new BiFuncOf<Exception, IEnumerable<T>, T>((ex, enumerable) => fallback.Invoke(enumerable)))
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, Func<Exception, IEnumerable<T>, T> fallback) : this(
            source,
            position,
            new BiFuncOf<Exception, IEnumerable<T>, T>((ex, enumerable) => fallback.Invoke(ex, enumerable)))
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, IBiFunc<Exception, IEnumerable<T>, T> fallback)
        {
            this._pos = position;
            this._src = source;
            this._fbk = fallback;
        }

        /// <summary>
        /// Get the item.
        /// </summary>
        /// <returns>the item</returns>
        public T Value()
        {
            return new ItemAtEnumerator<T>(
                this._src.GetEnumerator(), this._pos, this._fbk
            ).Value();
        }
    }
}
