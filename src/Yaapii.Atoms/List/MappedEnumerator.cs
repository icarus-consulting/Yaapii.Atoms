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
using Yaapii.Atoms.Func;

#pragma warning disable NoProperties // No Properties
#pragma warning disable CS1591

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given function.
    /// </summary>
    /// <typeparam name="In">type of items</typeparam>
    /// <typeparam name="Out">type of mapped items</typeparam>
    public sealed class MappedEnumerator<In, Out> : IEnumerator<Out>
    {
        private readonly IEnumerator<In> _enumerator;
        private readonly Func<In, Out> _func;

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fnc">mapping function</param>
        public MappedEnumerator(IEnumerator<In> src, IFunc<In, Out> fnc) : this(src,
            input => fnc.Invoke(input))
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fnc">mapping function</param>
        public MappedEnumerator(IEnumerator<In> src, Func<In, Out> fnc)
        {
            this._enumerator = src;
            this._func = fnc;
        }

        public Boolean MoveNext()
        {
            return this._enumerator.MoveNext();
        }

        public Out Current
        {
            get
            {
                return this._func.Invoke(this._enumerator.Current);
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Reset()
        {
            this._enumerator.Reset();
        }

        public void Dispose()
        { }
    }
}
#pragma warning restore NoProperties // No Properties