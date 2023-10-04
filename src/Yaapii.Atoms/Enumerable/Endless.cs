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

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="System.Collections.Generic.IEnumerable{T}"/> that repeats one element infinitely.
    /// </summary>
    /// <typeparam name="T">type of the elements</typeparam>
    public sealed class Endless<T> : System.Collections.Generic.IEnumerable<T>
    {
        private readonly T elm;

        /// <summary>
        /// A <see cref="IEnumerable"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public Endless(T elm)
        {
            this.elm = elm;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                yield return this.elm;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class Endless
    {
        /// <summary>
        /// A <see cref="IEnumerable"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public static System.Collections.Generic.IEnumerable<T> New<T>(T elm) => new Endless<T>(elm);
    }
}
