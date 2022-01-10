// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Element before another element in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of element</typeparam>
    public sealed class Sibling<T> : ScalarEnvelope<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source) : this(
            item,
            source,
            new FuncOf<IEnumerable<T>, T>(itr => { throw new IOException("Can't get right neighbour from iterable"); })
        )
        { }

        /// <summary>
        /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, T fallback) : this(item, source, 1, new FuncOf<IEnumerable<T>, T>(b => fallback))
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, int relativeposition) : this(item, source, relativeposition, new FuncOf<IEnumerable<T>, T>(itr => { throw new IOException("Can't get neighbour at position from iterable"); }))
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, int relativeposition, T fallback) : this(item, source, relativeposition, new FuncOf<IEnumerable<T>, T>(b => fallback))
        { }

        /// <summary>
        /// Next neighbour of an item in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(item, source, 1, fallback)
        { }

        /// <summary>
        /// Element that comes before another element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        public Sibling(T item, IEnumerable<T> source, int relativeposition, IFunc<IEnumerable<T>, T> fallback) : base(() =>
            new Enumerator.Sibling<T>(
                source.GetEnumerator(),
                item,
                relativeposition,
                fallback
            ).Value()
        )
        { }
    }

    public static class Sibling
    {

        /// <summary>
        /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        public static Sibling<T> New<T>(T item, IEnumerable<T> source)
            where T : IComparable<T> =>
            new Sibling<T>(item, source);

        /// <summary>
        /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public static Sibling<T> New<T>(T item, IEnumerable<T> source, T fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, fallback);

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="item">item to start</param>
        public static Sibling<T> New<T>(T item, IEnumerable<T> source, int relativeposition)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition);

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public static Sibling<T> New<T>(T item, IEnumerable<T> source, int relativeposition, T fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition, fallback);

        /// <summary>
        /// Next neighbour of an item in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        /// <param name="item">item to start</param>
        public static Sibling<T> New<T>(T item, IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, fallback);

        /// <summary>
        /// Element that comes before another element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        public static Sibling<T> New<T>(T item, IEnumerable<T> source, int relativeposition, IFunc<IEnumerable<T>, T> fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition, fallback);
    }
}
