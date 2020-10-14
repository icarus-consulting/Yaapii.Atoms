// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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

#pragma warning disable NoProperties

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// Enumerator that only gives the distinct elements of multiple enumerators.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Distinct<T> : IEnumerator<T>
    {
        private readonly IEnumerable<IEnumerator<T>> originals;
        private readonly Queue<IEnumerator<T>> buffer = new Queue<IEnumerator<T>>();
        private readonly List<T> hits = new List<T>();

        /// <summary>
        /// Enumerator that only gives the distinct elements of multiple enumerators.
        /// </summary>
        /// <param name="enumerators"></param>
        public Distinct(IEnumerable<IEnumerator<T>> enumerators)
        {
            originals = enumerators;
            buffer = new Queue<IEnumerator<T>>(enumerators);
        }

        /// <summary>
        /// Current element
        /// </summary>
        public T Current
        {
            get
            {
                return this.buffer.Peek().Current;
            }
        }

        object IEnumerator.Current => Current;

        /// <summary>
        /// Move to next element
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            //skip all entries that are already known
            SkipKnown();
            bool cnt = buffer.Count > 0;
            if (!cnt)
            {
                this.Reset();
            }
            return cnt;
        }

        /// <summary>
        /// Reset this enumerator
        /// </summary>
        public void Reset()
        {
            this.buffer.Clear();
            foreach (var e in this.originals)
            {
                this.buffer.Enqueue(e);
            }
            this.hits.Clear();
        }

        private void SkipKnown()
        {
            while (this.buffer.Count > 0)
            {
                while (this.buffer.Count > 0 && !this.buffer.Peek().MoveNext())
                {
                    this.buffer.Dequeue();
                }

                if (buffer.Count > 0 && !this.hits.Contains(this.buffer.Peek().Current))
                {
                    this.hits.Add(this.buffer.Peek().Current);
                    break;
                }
            }
        }

        public void Dispose()
        { }
    }
}

