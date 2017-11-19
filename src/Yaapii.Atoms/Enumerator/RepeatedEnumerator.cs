// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using System.Text;
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
    public sealed class RepeatedEnumerator<T> : IEnumerator<T>
    {
        private readonly IScalar<T> _element;
        private int _left;
        private int _max;

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public RepeatedEnumerator(T elm, int max) : this(new ScalarOf<T>(elm), max)
        { }

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public RepeatedEnumerator(IScalar<T> elm, int max)
        {
            this._element = elm;
            this._max = max;
            this._left = max;
        }

        public Boolean MoveNext()
        {
            if (this._left == 0) return false;
            --this._left;
            return true;
        }

        public void Reset()
        {
            this._left = this._max; 
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Current
        {
            get
            {
                return this._element.Value();
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();
    }
}
#pragma warning restore NoProperties // No Properties
#pragma warning restore Immutability // Fields are readonly or constant