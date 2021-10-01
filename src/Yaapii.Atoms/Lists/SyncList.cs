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

using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A list which is threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SyncList<T> : ListEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SyncList() : this(new object())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot"></param>
        public SyncList(object syncRoot) : this(syncRoot, new ListOf<T>())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to make collection from</param>
        public SyncList(params T[] items) : this(new ListOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lst">Collection to sync</param>
        public SyncList(IList<T> lst) : this(lst, lst)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot">root object to sync</param>
        /// <param name="col"></param>
        public SyncList(object syncRoot, IList<T> col) : base(
            new Scalar.Sync<IEnumerable<T>>(
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
}
