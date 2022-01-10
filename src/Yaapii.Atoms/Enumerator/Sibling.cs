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
using System.IO;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// Element from position, starting with given item in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sibling<T> : IScalar<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// enumerator to get item from
        /// </summary>
        private readonly IEnumerator<T> src;

        /// <summary>
        /// fallback function for alternative value
        /// </summary>
        private readonly IFunc<IEnumerable<T>, T> fallback;

        /// <summary>
        /// position of the item
        /// </summary>
        private readonly int pos;

        /// <summary>
        /// item to start with
        /// </summary>
        private readonly T needle;

        /// <summary>
        /// Right neighbour of a given item with a fallback value.
        /// </summary>
        /// <param name="src">the enumerator</param>
        /// <param name="item">the item to start with</param>
        /// <param name="fallback">the fallback to return if fails</param>
        public Sibling(IEnumerator<T> src, T item, T fallback) : this(
            src,
            item,
            1,
            new FuncOf<IEnumerable<T>, T>(() => fallback))
        { }

        /// <summary>
        /// Right neighbour of a given item with a fallback value.
        /// </summary>
        /// <param name="src">the enumerator</param>
        /// <param name="item">the item to start with</param>
        /// <param name="pos">position of the neighbour</param>
        /// <param name="fallback">the fallback to return if fails</param>
        public Sibling(IEnumerator<T> src, T item, int pos, T fallback) : this(
            src,
            item,
            pos,
            new FuncOf<IEnumerable<T>, T>(() => fallback))
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="pos">position</param>
        /// <param name="item">item to start with</param>
        public Sibling(IEnumerator<T> src, T item, int pos)
        :
            this(
                src,
                item,
                pos,
                new FuncOf<IEnumerable<T>, T>(
                    (itr) =>
                    {
                        throw new IOException(
                            new Formatted(
                                "Enumerator doesn't have a neighbour at position {0}",
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
        /// <param name="item">item to start with</param>
        public Sibling(
            IEnumerator<T> src,
            T item,
            int pos,
            IFunc<IEnumerable<T>, T> fbk)
        {
            this.pos = pos;
            this.src = src;
            this.needle = item;
            this.fallback = fbk;
        }

        /// <summary>
        /// Get the item.
        /// </summary>
        /// <returns>the item</returns>
        public T Value()
        {
            T ret;
            try
            {
                new FailPrecise(
                    new FailWhen(!this.src.MoveNext()),
                    new IOException("cannot get neighbours because enumerable is empty")).Go();

                int cur;

                //Find the needle index
                for (cur = 0; this.src.Current.CompareTo(this.needle) != 0; cur++)
                {
                    if (!this.src.MoveNext()) throw new IOException("cannot get neighbour because item is not in the enumerable.");
                }

                var idx = cur + this.pos;
                if (idx < 0) throw new ArgumentOutOfRangeException("position", "cannot get neighbour because position is not in range of the enumerable");

                this.src.Reset();
                for (cur = 0; cur <= idx; cur++)
                {
                    if (!this.src.MoveNext()) throw new ArgumentOutOfRangeException("position", "cannot get neighbour because position is not in range of the enumerable");
                }
                ret = this.src.Current;
            }
            catch (Exception)
            {
                ret = this.fallback.Invoke(new ManyOf<T>(this.src));
            }
            return ret;

        }
    }

    /// <summary>
    /// Element from position, starting with given item in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
    /// </summary>
    public static class Sibling
    {
        /// <summary>
        /// Right neighbour of a given item with a fallback value.
        /// </summary>
        /// <param name="src">the enumerator</param>
        /// <param name="item">the item to start with</param>
        /// <param name="fallback">the fallback to return if fails</param>
        public static Sibling<T> New<T>(IEnumerator<T> src, T item, T fallback)
            where T : IComparable<T>
            => new Sibling<T>(src, item, fallback);

        /// <summary>
        /// Right neighbour of a given item with a fallback value.
        /// </summary>
        /// <param name="src">the enumerator</param>
        /// <param name="item">the item to start with</param>
        /// <param name="pos">position of the neighbour</param>
        /// <param name="fallback">the fallback to return if fails</param>
        public static Sibling<T> New<T>(IEnumerator<T> src, T item, int pos, T fallback)
            where T : IComparable<T>
            => new Sibling<T>(src, item, pos, fallback);


        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="pos">position</param>
        /// <param name="item">item to start with</param>
        public static Sibling<T> New<T>(IEnumerator<T> src, T item, int pos)
            where T : IComparable<T>
            => new Sibling<T>(src, item, pos);

        /// <summary>
        /// Element at position in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="src">source <see cref="IEnumerable{T}"/></param>
        /// <param name="fbk">fallback function</param>
        /// <param name="pos">position</param>
        /// <param name="item">item to start with</param>
        public static Sibling<T> New<T>(
            IEnumerator<T> src,
            T item,
            int pos,
            IFunc<IEnumerable<T>, T> fbk)
            where T : IComparable<T>
            => new Sibling<T>(src, item, pos, fbk);
        
    }
}
