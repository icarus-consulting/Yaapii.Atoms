// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using System.Collections.Generic;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Mapped list
    /// </summary>
    /// <typeparam name="In">Type of source items</typeparam>
    /// <typeparam name="Out">Type of target items</typeparam>
    public sealed class Mapped<In, Out> : List.Envelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(IFunc<In, Out> fnc, IEnumerable<In> src) : this((input)=>fnc.Invoke(input), src)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> fnc, IEnumerator<In> src) : this(fnc, new List.Live<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> fnc, IEnumerable<In> src) : this(fnc, new List.Live<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> fnc, ICollection<In> src) : base(
            () =>
            new List.Live<Out>(
                  new Collection.Mapped<In, Out>(fnc, src)
            ),
            false
        )
        { }

}
}
