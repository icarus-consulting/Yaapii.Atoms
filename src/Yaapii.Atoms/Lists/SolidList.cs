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

using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A list that is both sticky and threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SolidList<T> : ListEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public SolidList(params T[] items) : this(new ManyOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public SolidList(IEnumerable<T> items) : base(() =>
            new SyncList<T>(
                new ListOf<T>(items)
            ),
            false
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerator">items to decorate</param>
        public SolidList(IEnumerator<T> enumerator) : base(() =>
            new SyncList<T>(
                new ListOf<T>(enumerator)
            ),
            false
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">list to decorate</param>
        public SolidList(ICollection<T> list) : base(() =>
            new SyncList<T>(
                new ListOf<T>(list)
            ),
            false
        )
        { }
    }

    public static class SolidList
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public static IList<T> New<T>(params T[] items)
            => new SolidList<T>(items);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to decorate</param>
        public static IList<T> New<T>(IEnumerable<T> items)
            => new SolidList<T>(items);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerator">items to decorate</param>
        public static IList<T> New<T>(IEnumerator<T> enumerator)
            => new SolidList<T>(enumerator);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">list to decorate</param>
        public static IList<T> New<T>(ICollection<T> list)
            => new SolidList<T>(list);
    }
}
