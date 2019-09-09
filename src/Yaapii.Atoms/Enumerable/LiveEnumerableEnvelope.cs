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

using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Envelope for Enumerable.
    /// You must understand that this enumerable is built live
    /// every time when a enumerator is requested.
    /// If you do not want this, use <see cref="EnumerableOf{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LiveEnumerableEnvelope<T> : IEnumerable<T>
    {
        /// <summary>
        /// The source enumerable.
        /// </summary>
        private readonly IScalar<IEnumerable<T>> origin;

        /// <summary>
        /// Envelope for Enumerables.
        /// </summary>
        public LiveEnumerableEnvelope(Func<IEnumerable<T>> fnc) : this(
            new ScalarOf<IEnumerable<T>>(fnc))
        { }

        /// <summary>
        /// Envelope for Enumerables.
        /// </summary>
        public LiveEnumerableEnvelope(IScalar<IEnumerable<T>> sc)
        {
            this.origin = sc;
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.origin.Value().GetEnumerator();
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
