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
    /// Multiple <see cref="IEnumerator{T}"/> joined together.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Joined<T> : IEnumerator<T>
    {
        private readonly IEnumerable<IEnumerator<T>> list;
        private readonly Queue<IEnumerator<T>> buffer;

        /// <summary>
        /// Multiple <see cref="IEnumerator{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join together</param>
        public Joined(params IEnumerator<T>[] items) : this(new List<IEnumerator<T>>(items))
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerator{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join together</param>
        public Joined(IEnumerable<IEnumerator<T>> items)
        {
            this.list = items;
            this.buffer = new Queue<IEnumerator<T>>(items);
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

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            while (this.buffer.Count > 0 && !this.buffer.Peek().MoveNext())
            {
                this.buffer.Dequeue();
            }

            return this.buffer.Count > 0;
        }

        public void Reset()
        {
            this.buffer.Clear();
            var e = this.list.GetEnumerator();
            while (e.MoveNext())
            {
                this.buffer.Enqueue(e.Current);
            }

        }
    }

    /// <summary>
    /// Multiple <see cref="IEnumerator{T}"/> joined together.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public static class Joined
    {
        /// <summary>
        /// Multiple <see cref="IEnumerator{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join together</param>
        public static Joined<T> New<T>(params IEnumerator<T>[] items) => new Joined<T>(items);

        /// <summary>
        /// Multiple <see cref="IEnumerator{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join together</param>
        public static Joined<T> New<T>(IEnumerable<IEnumerator<T>> items) => new Joined<T>(items);
    }
}
