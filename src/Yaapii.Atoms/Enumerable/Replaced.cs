// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
    /// </summary>
    /// <typeparam name="T">type of items in enumerable</typeparam>
    public sealed class Replaced<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> origin;
        private readonly IFunc<T, bool> condition;
        private readonly T replacement;
        private readonly Ternary<T> result;

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, Func<T, bool> condition, T replacement, bool live = false) : this(
            origin,
            new FuncOf<T, bool>(condition),
            replacement,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable"/> where an item at a given index is replaced.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="index">index at which to replace the item</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, int index, T replacement, bool live = false) : this(
            new Mapped<T, T>(
                (item, itemIndex) => itemIndex == index ? replacement : item,
                origin,
                live: true
            ),
            item => false,
            replacement,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, IFunc<T, bool> condition, T replacement, bool live = false)
        {
            this.origin = origin;
            this.condition = condition;
            this.replacement = replacement;
            this.result =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.New(Produced),
                    live
                );
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Produced()
        {
            var e = this.origin.GetEnumerator();

            while (e.MoveNext())
            {
                if (condition.Invoke(e.Current))
                {
                    yield return replacement;
                }
                else
                {
                    yield return e.Current;
                }
            }
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
    /// </summary>
    public static class Replaced
    {
        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, Func<T, bool> condition, T replacement, bool live = false) =>
            new Replaced<T>(origin, condition, replacement, live);

        /// <summary>
        /// A <see cref="IEnumerable"/> where an item at a given index is replaced.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="index">index at which to replace the item</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, int index, T replacement, bool live = false) =>
            new Replaced<T>(origin, index, replacement, live);

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, IFunc<T, bool> condition, T replacement, bool live = false) =>
            new Replaced<T>(origin, condition, replacement, live);
    }
}
