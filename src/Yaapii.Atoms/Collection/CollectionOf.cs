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

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Collection out of other things. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CollectionOf<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// A collection from an array
        /// </summary>
        /// <param name="array"></param>
        public CollectionOf(params T[] array) : this(new LiveMany<T>(array))
        { }

        /// <summary>
        /// A collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public CollectionOf(IEnumerator<T> src) : this(new ManyOf<T>(src))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public CollectionOf(IEnumerable<T> src) : base(
            () =>
            {
                ICollection<T> list = new LinkedList<T>();
                foreach (T item in src)
                {
                    list.Add(item);
                }
                return list;
            },
            false
        )
        { }
    }

    public static class CollectionOf
    {
        public static ICollection<T> New<T>(params T[] array) => new CollectionOf<T>(array);

        public static ICollection<T> New<T>(IEnumerator<T> src) => new CollectionOf<T>(src);

        public static ICollection<T> New<T>(IEnumerable<T> src) => new CollectionOf<T>(src);

    }
}
