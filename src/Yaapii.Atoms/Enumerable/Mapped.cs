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
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Func;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given function.
    /// </summary>
    /// <typeparam name="In">type of input elements</typeparam>
    /// <typeparam name="Out">type of mapped elements</typeparam>
    public sealed class Mapped<In, Out> : IEnumerable<Out>
    {
        private readonly IEnumerable<In> _enumerable;
        private readonly IBiFunc<In, int, Out> _func;

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IFunc<In, Out> fnc, params In[] src) : this(new EnumerableOf<In>(src), fnc)
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IBiFunc<In, int, Out> fnc, params In[] src) : this(new EnumerableOf<In>(src), fnc)
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IEnumerable<In> src, Func<In, Out> fnc) : this(
            src,
            (In1, In2) => fnc.Invoke(In1))
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IEnumerable<In> src, Func<In, int, Out> fnc) : this(
            src,
            new BiFuncOf<In, int, Out>(fnc))
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IEnumerable<In> src, IFunc<In, Out> fnc) : this(src, new BiFuncOf<In, int, Out>((In1, In2) => fnc.Invoke(In1)))
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IBiFunc{In, Index, Out}"/> function with index.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IEnumerable<In> src, IBiFunc<In, int, Out> fnc)
        {
            this._enumerable = src;
            this._func = fnc;
        }

        public IEnumerator<Out> GetEnumerator()
        {
            return new MappedEnumerator<In, Out>(
                this._enumerable.GetEnumerator(), this._func
            );
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics
