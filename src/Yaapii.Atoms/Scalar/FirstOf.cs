// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// First element in <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FirstOf<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="ex">Exception to throw if no value can be found.</param>
        public FirstOf(IEnumerable<T> source, Exception ex) : this(
            (enm) => true,
            source,
            (enm) => throw ex
        )
        { }

        public FirstOf(IEnumerable<T> source) : this(
            (enm) => true,
            source,
            new NoSuchElementException("Cannot get first element - no match.")
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="condition">condition to find the desired item</param>
        public FirstOf(Func<T, bool> condition, IEnumerable<T> source) : this(
            condition,
            source,
            new NoSuchElementException("Cannot get first element - no match.")
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="condition">condition to find the desired item</param>
        /// <param name="ex">Exception to throw if no value can be found.</param>
        public FirstOf(Func<T, bool> condition, IEnumerable<T> source, Exception ex) : this(
            condition,
            source,
            (enm) =>
            {
                throw ex;
            }
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public FirstOf(IEnumerable<T> source, T fallback) : this(
            enm => true,
            source,
            (b) => fallback
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="condition">condition to match in order to find the desired item</param>
        public FirstOf(Func<T, bool> condition, IEnumerable<T> source, T fallback) : this(
            condition,
            source,
            (b) => fallback
        )
        { }

        /// <summary>
        /// First Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public FirstOf(IEnumerable<T> source, IScalar<T> fallback) : this(
            enm => true,
            source,
            (enm) => fallback.Value()
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IBiFunc{X, Y, Z}"/>
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fallback">fallback if no match</param>
        public FirstOf(IEnumerable<T> src, Func<IEnumerable<T>, T> fallback) : this(item => true, src, fallback)
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IBiFunc{X, Y, Z}"/>
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fallback">fallback if no match</param>
        /// <param name="condition">condition to match</param>
        public FirstOf(Func<T, bool> condition, IEnumerable<T> src, Func<IEnumerable<T>, T> fallback)
            : base(() =>
            {
                var filtered = new Filtered<T>(condition, src).GetEnumerator();

                T result;
                if (filtered.MoveNext())
                {
                    result = filtered.Current;
                }
                else
                {
                    result = fallback(src);
                }
                return result;
            })
        { }
    }
    public static class FirstOf
    {
        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="ex">Exception to throw if no value can be found.</param>
        public static FirstOf<T> New<T>(IEnumerable<T> source, Exception ex)
            => new FirstOf<T>(source, ex);

        public static FirstOf<T> New<T>(IEnumerable<T> source)
            => new FirstOf<T>(source);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="condition">condition to find the desired item</param>
        public static FirstOf<T> New<T>(Func<T, bool> condition, IEnumerable<T> source)
            => new FirstOf<T>(condition, source);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="condition">condition to find the desired item</param>
        /// <param name="ex">Exception to throw if no value can be found.</param>
        public static FirstOf<T> New<T>(Func<T, bool> condition, IEnumerable<T> source, Exception ex)
            => new FirstOf<T>(condition, source, ex);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static FirstOf<T> New<T>(IEnumerable<T> source, T fallback)
            => new FirstOf<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="condition">condition to match in order to find the desired item</param>
        public static FirstOf<T> New<T>(Func<T, bool> condition, IEnumerable<T> source, T fallback)
            => new FirstOf<T>(condition, source, fallback);

        /// <summary>
        /// First Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static FirstOf<T> New<T>(IEnumerable<T> source, IScalar<T> fallback)
            => new FirstOf<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IBiFunc{X, Y, Z}"/>
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fallback">fallback if no match</param>
        public static FirstOf<T> New<T>(IEnumerable<T> src, Func<IEnumerable<T>, T> fallback)
            => new FirstOf<T>(src, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IBiFunc{X, Y, Z}"/>
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fallback">fallback if no match</param>
        /// <param name="condition">condition to match</param>
        public static FirstOf<T> New<T>(Func<T, bool> condition, IEnumerable<T> src, Func<IEnumerable<T>, T> fallback)
            => new FirstOf<T>(condition, src, fallback);

    }
}
