// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Enumerable partitioned by a given size.
    /// <para>Is a IEnumerable</para>
    /// </summary>
    public sealed class Partitioned<T> : IEnumerable<IEnumerable<T>>
    {
        private readonly int size;
        private readonly IEnumerable<T> items;

        /// <summary>
        /// Enumerable partitioned by a given size.
        /// </summary>
        public Partitioned(int size, IEnumerable<T> items)
        {
            this.size = size;
            this.items = items;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            var source = this.items.GetEnumerator();
            while(source.MoveNext())
            {
                yield return Partition(source);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Partition(IEnumerator<T> source)
        {
            var taken = 1;
            yield return source.Current;
            while(taken < this.size && source.MoveNext())
            {
                taken++;
                yield return source.Current;
            }
        }
    }

    /// <summary>
    /// Enumerable partitioned by a given size.
    /// <para>Is a IEnumerable</para>
    /// </summary>
    public static class Partitioned
    {
        /// <summary>
        /// Enumerable partitioned by a given size.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> New<T>(int size, IEnumerable<T> items) => new Partitioned<T>(size, items);
    }
}
