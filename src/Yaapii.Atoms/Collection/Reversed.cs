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
using System.Linq;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection
{
    ///
    /// Reversed collection.
    ///
    /// <para>Pay attention that sorting will happen on each operation
    /// with the collection. Every time you touch it, it will fetch the
    /// entire collection from the encapsulated object and reverse it. If you
    /// want to avoid that "side-effect", decorate it with
    /// <see cref="Sticky{T}"/></para>
    ///
    /// <para>There is no thread-safety guarantee.</para>
    ///
    public class Reversed<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src"></param>
        public Reversed(params T[] src) : this(new Many.Of<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public Reversed(IEnumerable<T> src) : this(new CollectionOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public Reversed(ICollection<T> src) : base(
            () => new CollectionOf<T>(
                    new LinkedList<T>(src).Reverse()))
        { }
    }
}
