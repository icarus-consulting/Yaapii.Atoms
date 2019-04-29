// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A collection which is threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SyncCollection<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SyncCollection() : this(new object())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot"></param>
        public SyncCollection(object syncRoot) : this(syncRoot, new CollectionOf<T>())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to make collection from</param>
        public SyncCollection(params T[] items) : this(new CollectionOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="col">Collection to sync</param>
        public SyncCollection(ICollection<T> col) : this(col, col)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot">root object to sync</param>
        /// <param name="col"></param>
        public SyncCollection(object syncRoot, ICollection<T> col) : base(
            new Sync<ICollection<T>>(
                new ScalarOf<ICollection<T>>(() =>
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
                }
        )))
        { }
    }
}