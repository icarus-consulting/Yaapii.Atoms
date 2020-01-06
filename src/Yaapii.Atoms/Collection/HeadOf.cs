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

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A collection which is limited to a number of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class HeadOf<T> : Collection.Envelope<T>
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">items to limit</param>
        public HeadOf(int lmt, params T[] src) : this(lmt, new Many.Of<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">Enumerator to limit</param>
        public HeadOf(int lmt, IEnumerator<T> src) : this(lmt, new Many.Of<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">requested number of items</param>
        /// <param name="src">enumerable of items</param>
        public HeadOf(int lmt, IEnumerable<T> src) : this(lmt, new Collection.Live<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        /// <param name="lmt">requested number of elements</param>
        public HeadOf(int lmt, ICollection<T> src) : base(
            () => new Collection.Live<T>(
                new Enumerable.HeadOf<T>(src, lmt)
            ),
            false
        )
        { }

    }
}
