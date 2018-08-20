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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// First element in <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FirstOf<T> : IScalar<T>
    {
        private readonly IEnumerable<T> src;
        private readonly IBiFunc<Exception, IEnumerable<T>, T> fbk;

        /// <summary>
        /// First element in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public FirstOf(IEnumerable<T> source, Exception ex) : this(
            source,
            new FuncOf<IEnumerable<T>, T>(
                (itr) => throw ex
            )
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public FirstOf(IEnumerable<T> source) : this(
                source,
                new BiFuncOf<Exception, IEnumerable<T>, T>((ex, itr) =>
                {
                    throw
                        new NoSuchElementException(
                            new FormattedText(
                                "Cannot get first element: {0}",
                                ex.Message
                            ).AsString()
                    );
                }))
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public FirstOf(IEnumerable<T> source, T fallback) : this(
            source,
            new FuncOf<IEnumerable<T>, T>(b => fallback)
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public FirstOf(IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(
            source,
            (ex, enumerable) => fallback.Invoke(enumerable)
        )
        { }

        /// <summary>
        /// First Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public FirstOf(IEnumerable<T> source, Func<Exception, IEnumerable<T>, T> fallback) : this(
            source,
            new BiFuncOf<Exception, IEnumerable<T>, T>((ex, enumerable) => fallback.Invoke(ex, enumerable)
            )
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IBiFunc{X, Y, Z}"/>
        /// </summary>
        /// <param name="src"></param>
        /// <param name="fallback"></param>
        public FirstOf(IEnumerable<T> src, IBiFunc<Exception, IEnumerable<T>, T> fallback)
        {
            this.src = src;
            this.fbk = fallback;
        }

        public T Value()
        {
            return new ItemAt<T>(this.src, this.fbk).Value();
        }
    }
}