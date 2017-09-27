// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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

#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable NoProperties // No Properties
#pragma warning disable CS1591
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
    /// </summary>
    /// <typeparam name="T">type of items in enumerable</typeparam>
    public sealed class SkippedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly int _skip;
        private int _left;

        /// <summary>
        /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerator"><see cref="IEnumerator{T}"/> to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public SkippedEnumerator(IEnumerator<T> enumerator, int skip)
        {
            this._enumerator = enumerator;
            this._skip = skip;
            this._left = this._skip;
        }

        public Boolean MoveNext()
        {
            while (this._left > 0 && this._enumerator.MoveNext())
            {
                --this._left;
            }
            return this._enumerator.MoveNext();
        }

        public void Reset()
        {
            this._left = this._skip;
            this._enumerator.Reset();
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

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }
}
#pragma warning restore Immutability // Fields are readonly or constant
#pragma warning restore NoProperties // No Properties