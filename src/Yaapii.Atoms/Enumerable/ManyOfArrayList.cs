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
using System.Collections.Concurrent;
using System.Collections.Generic;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="ArrayList"/> converted to IEnumerable&lt;object&gt;
    /// </summary>
    public sealed class ManyOfArrayList : IEnumerable<object>
    {
        private readonly ArrayList src;

        /// <summary>
        /// A ArrayList converted to IEnumerable&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ManyOfArrayList(ArrayList src)
        {
            this.src = src;
        }

        public IEnumerator<object> GetEnumerator()
        {
            foreach(var item in this.src)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// A <see cref="ArrayList"/> converted to IEnumerable&lt;T&gt;
    /// </summary>
    public sealed class ManyOfArrayList<T> : IEnumerable<T>
    {
        private readonly ArrayList src;

        /// <summary>
        /// A ArrayList converted to IEnumerable&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ManyOfArrayList(ArrayList src)
        {
            this.src = src;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.src)
                yield return (T)item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
