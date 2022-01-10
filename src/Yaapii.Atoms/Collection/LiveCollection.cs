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
    /// Envelope for collections. 
    /// It accepts a scalar and makes readonly Collection from it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LiveCollection<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Makes a collection from an array
        /// </summary>
        /// <param name="array"></param>
        public LiveCollection(params T[] array) : this(new LiveMany<T>(array))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public LiveCollection(IEnumerator<T> src) : this(new ManyOf<T>(src))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public LiveCollection(IEnumerable<T> src) : base(
            () => src.GetEnumerator(),
            true
        )
        { }
    }
}
