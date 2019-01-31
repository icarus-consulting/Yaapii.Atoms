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
using System.Text;

#pragma warning disable NoProperties // No Properties
#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> limited to an item maximum.
    /// </summary>
    /// <typeparam name="T">type of the enumerator content</typeparam>
    public sealed class LimitedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly int _limit;
        private int _consumed;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> limited to an item maximum.
        /// </summary>
        /// <param name="enumerator">enumerator to limit</param>
        /// <param name="limit">maximum item count</param>
        public LimitedEnumerator(IEnumerator<T> enumerator, int limit)
        {
            this._enumerator = enumerator;
            this._limit = limit;
            this._consumed = 0;
        }

        public Boolean MoveNext()
        {
            return this._consumed++ < this._limit && this._enumerator.MoveNext();
        }

        public void Reset()
        {
            this._enumerator.Reset();
            this._consumed = 0;
        }

        public void Dispose()
        { }

        public T Current
        {
            get
            {
                return this._enumerator.Current;
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();
    }
}
#pragma warning restore NoProperties // No Properties
#pragma warning restore Immutability // Fields are readonly or constant