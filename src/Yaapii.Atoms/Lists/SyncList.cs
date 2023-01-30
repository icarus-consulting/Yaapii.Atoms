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

using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A list which is threadsafe.
    /// </summary>
    public sealed class SyncList<T> : ListEnvelope<T>
    {
        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public SyncList() : this(new object())
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public SyncList(object syncRoot) : this(
            syncRoot,
            new ListOf<T>()
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public SyncList(params T[] items) : this(
            new ListOf<T>(items)
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public SyncList(IEnumerable<T> items) : this(
            new ListOf<T>(items)
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public SyncList(IEnumerator<T> items) : this(
            new ListOf<T>(items)
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">Items to make list from</param>
        public SyncList(ICollection<T> lst) : this(
            new ListOf<T>(lst)
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">List to sync</param>
        public SyncList(IList<T> lst) : this(
            lst,
            lst
        )
        { }

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="syncRoot">Root object to sync</param>
        public SyncList(object syncRoot, IList<T> col) : base(
            new Sync<IEnumerable<T>>(
                new Live<IEnumerable<T>>(() =>
                {
                    lock (syncRoot)
                    {
                        var tmp = new List<T>();
                        foreach (var item in col)
                        {
                            tmp.Add(item);
                        }
                        return tmp;
                    }
                })
            ),
            false
        )
        { }
    }

    public static class SyncList
    {
        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public static IList<T> New<T>()
            => new SyncList<T>();

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        public static IList<T> New<T>(object syncRoot)
            => new SyncList<T>(syncRoot);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public static IList<T> New<T>(params T[] items)
            => new SyncList<T>(items);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public static IList<T> New<T>(IEnumerable<T> items)
            => new SyncList<T>(items);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="items">Items to make list from</param>
        public static IList<T> New<T>(IEnumerator<T> items)
            => new SyncList<T>(items);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">Items to make list from</param>
        public static IList<T> New<T>(ICollection<T> lst)
            => new SyncList<T>(lst);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="lst">List to sync</param>
        public static IList<T> New<T>(IList<T> lst)
            => new SyncList<T>(lst);

        /// <summary>
        /// A list which is threadsafe.
        /// </summary>
        /// <param name="syncRoot">Root object to sync</param>
        public static IList<T> New<T>(object syncRoot, IList<T> col)
            => new SyncList<T>(syncRoot, col);
    }
}
