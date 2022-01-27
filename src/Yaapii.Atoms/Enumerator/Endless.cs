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

using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class Endless<T> : IEnumerator<T>
    {
        /// <summary>
        /// element to repeat
        /// </summary>
        private readonly IScalar<T> element;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public Endless(T elm) : this(new Live<T>(elm))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">scalar of element to repeat</param>
        public Endless(IScalar<T> elm)
        {
            this.element = elm;
        }

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        { }

        public void Dispose()
        { }

        public T Current
        {
            get
            {
                return this.element.Value();
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();
    }

    public static class Endless
    {

            /// <summary>
            /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
            /// </summary>
            /// <param name="elm">element to repeat</param>
            public static Endless<T> New<T>(T elm) =>
                new Endless<T>(elm);

            /// <summary>
            /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
            /// </summary>
            /// <param name="elm">scalar of element to repeat</param>
            public static IEnumerator<T> New<T>(IScalar<T> elm) =>
                new Endless<T>(elm);
        }
}
