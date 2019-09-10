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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Makes a collection that iterates only once.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sticky<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="items">source items</param>
        public Sticky(params T[] items) : this(new Many.Of<T>(items))
        { }

        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="items">source items</param>
        public Sticky(IEnumerator<T> items) : this(new Many.Of<T>(items))
        { }

        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="items">source items</param>
        public Sticky(IEnumerable<T> items) : this(new CollectionOf<T>(items))
        { }

        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="list">list of source items</param>
        public Sticky(ICollection<T> list) : base(
                new Scalar.Sticky<ICollection<T>>( //Make a sticky scalar which copies the items once and returns them always.
                    () =>
                    {
                        var temp = new List<T>(list.Count);
                        foreach(var item in list)
                        {
                            temp.Add(item);
                        }
                        return new CollectionOf<T>(temp);
                    }
            ))
        { }
    }
}
