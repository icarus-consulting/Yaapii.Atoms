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

#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable NoProperties // No Properties
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
    /// </summary>
    /// <typeparam name="T">type of element to repeat</typeparam>
    public sealed class Repeated<T> : IEnumerator<T>
    {
        private readonly IScalar<T> element;
        private int left;
        private int max;

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public Repeated(T elm, int max) : this(new Live<T>(elm), max)
        { }

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public Repeated(IScalar<T> elm, int max)
        {
            this.element = elm;
            this.max = max;
            this.left = max;
        }

        public Boolean MoveNext()
        {
            if (this.max < 0)
            {
                throw new ArgumentException($"The amount of repeats must be >= 0 but is {this.max}");
            }
            if (this.left == 0) return false;
            --this.left;
            return true;
        }

        public void Reset()
        {
            this.left = this.max;
        }

        public void Dispose()
        {

        }

        public T Current
        {
            get
            {
                if (this.max < 0)
                {
                    throw new ArgumentException($"The amount of repeats must be >= 0 but is {this.max}");
                }
                return this.element.Value();
            }
        }

        object IEnumerator.Current => Current;
    }

    /// <summary>
    /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
    /// </summary>
    public static class Repeated
    {
        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public static IEnumerator<T> New<T>(T elm, int max) => new Repeated<T>(elm, max);

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public static IEnumerator<T> New<T>(IScalar<T> elm, int max) => new Repeated<T>(elm, max);
    }
}
