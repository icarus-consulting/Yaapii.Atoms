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
    public partial class Many
    {
        /// <summary>
        /// Envelope for Enumerable.
        /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class LiveEnvelope : IEnumerable<string>
        {
            /// <summary>
            /// Build enumerable.
            /// </summary>
            private readonly Lazy<IEnumerable<string>> origin;

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public LiveEnvelope(IScalar<IEnumerable<string>> fnc) : this(() => fnc.Value())
            { }

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public LiveEnvelope(Func<IEnumerable<string>> origin)
            {
                this.origin = new Lazy<IEnumerable<string>>(() => origin());
            }

            /// <summary>
            /// Enumerator for this envelope.
            /// </summary>
            /// <returns>The enumerator</returns>
            public IEnumerator<string> GetEnumerator()
            {
                return this.origin.Value.GetEnumerator();
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

        /// <summary>
        /// Envelope for Enumerable.
        /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class LiveEnvelope<T> : IEnumerable<T>
        {
            /// <summary>
            /// Build enumerable.
            /// </summary>
            private readonly Func<IEnumerable<T>> origin;

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public LiveEnvelope(IScalar<IEnumerable<T>> fnc) : this(() => fnc.Value())
            { }

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public LiveEnvelope(Func<IEnumerable<T>> origin)
            {
                this.origin = () => origin();
            }

            /// <summary>
            /// Enumerator for this envelope.
            /// </summary>
            /// <returns>The enumerator</returns>
            public IEnumerator<T> GetEnumerator()
            {
                return this.origin().GetEnumerator();
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
}
