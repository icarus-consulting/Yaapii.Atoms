// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.Linq;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="X">type of items in enumerable</typeparam>
    public sealed class Reversed<X> : IEnumerable<X>
    {
        private readonly IEnumerable<X> src;
        private readonly Ternary<X> result;

        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public Reversed(IEnumerable<X> src, bool live = false)
        {
            this.src = src;
            this.result =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.New(Produced),
                    live
                );
        }

        public IEnumerator<X> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<X> Produced()
        {
            foreach(var item in this.src.Reverse())
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class Reversed
    {
        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> src) => new Reversed<T>(src);
    }
}
