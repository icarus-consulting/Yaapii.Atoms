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
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
    /// </summary>
    /// <typeparam name="InAndOut">input and output type</typeparam>
    /// <typeparam name="Element">type of elements in a list to reduce</typeparam>
    public sealed class Reduced<InAndOut, Element> : IScalar<InAndOut>
    {
        private readonly IEnumerable<Element> enumerable;
        private readonly InAndOut input;
        private readonly IBiFunc<InAndOut , Element, InAndOut> func;

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="toReduce">enumerable to reduce</param>
        /// <param name="input">input for the reducing function</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<Element> toReduce, InAndOut input, Func<InAndOut, Element, InAndOut> fnc) : this(
            toReduce, input, new BiFuncOf<InAndOut, Element, InAndOut>((arg1, arg2) => fnc.Invoke(arg1, arg2)))
        { }

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="toReduce">enumerable to reduce</param>
        /// <param name="input">input for the reducing function</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<Element> toReduce, InAndOut input, IBiFunc<InAndOut, Element, InAndOut> fnc)
        {
            this.enumerable = toReduce;
            this.input = input;
            this.func = fnc;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public InAndOut Value()
        {
            InAndOut memo = this.input;
            foreach (Element item in this.enumerable)
            {
                memo = this.func.Invoke(memo, item);
            }
            return memo;
        }

    }
}
