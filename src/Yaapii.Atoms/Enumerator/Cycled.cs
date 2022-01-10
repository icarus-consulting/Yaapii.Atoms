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

using System.Collections;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class Cycled<T> : IEnumerator<T>
    {
        /// <summary>
        /// enum to cycle
        /// </summary>
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Queue<IEnumerator<T>> buffer = new Queue<IEnumerator<T>>();

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
        /// </summary>
        /// <param name="enumerable">enum to cycle</param>
        public Cycled(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        public bool MoveNext()
        {
            if (this.buffer.Count == 0 || !this.buffer.Peek().MoveNext())
            {
                this.buffer.Clear();
                this.buffer.Enqueue(this.enumerable.GetEnumerator());
                return this.buffer.Peek().MoveNext();
            }
            return true;
        }

        public void Reset()
        {
            this.buffer.Clear();
        }

        public void Dispose()
        {

        }

        public T Current
        {
            get
            {
                return this.buffer.Peek().Current;
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
