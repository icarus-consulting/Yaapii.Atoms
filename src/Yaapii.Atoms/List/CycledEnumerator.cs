/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class CycledEnumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// enum to cycle
        /// </summary>
        private readonly IEnumerable<T> _enumerable;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Queue<IEnumerator<T>> _buffer = new Queue<IEnumerator<T>>();

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
        /// </summary>
        /// <param name="enumerable">enum to cycle</param>
        public CycledEnumerator(IEnumerable<T> enumerable)
        {
            this._enumerable = enumerable;
        }

        public bool MoveNext()
        {
            if (this._buffer.Count == 0 || !this._buffer.Peek().MoveNext())
            {
                this._buffer.Clear();
                this._buffer.Enqueue(this._enumerable.GetEnumerator());
                return this._buffer.Peek().MoveNext();
            }
            return true;
        }

        public void Reset()
        {
            this._buffer.Clear();
        }

        public void Dispose()
        {
            
        }

        public T Current
        {
            get
            {
                return this._buffer.Peek().Current;
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
#pragma warning restore NoProperties // No Properties