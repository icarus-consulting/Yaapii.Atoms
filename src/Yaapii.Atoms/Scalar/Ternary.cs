// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A ternary operation.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class Ternary<In, Out> : ScalarEnvelope<Out>
    {
        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(In input, System.Func<In, Boolean> condition, System.Func<In, Out> consequent, System.Func<In, Out> alternative)
        :
            this(input,
                new FuncOf<In, Boolean>(condition),
                new FuncOf<In, Out>(consequent),
                new FuncOf<In, Out>(alternative))
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(In input, IFunc<In, Boolean> condition, IFunc<In, Out> consequent, IFunc<In, Out> alternative)
            : this(
                new Live<bool>(() => condition.Invoke(input)),
                new Live<Out>(() => consequent.Invoke(input)),
                new Live<Out>(() => alternative.Invoke(input))
            )
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(Boolean condition, Out consequent, Out alternative)
            : this(new Live<Boolean>(condition), consequent, alternative)
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(IScalar<Boolean> condition, Out consequent, Out alternative)
            : this(condition, new Live<Out>(consequent), new Live<Out>(alternative))
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(IScalar<Boolean> condition, IScalar<Out> consequent, IScalar<Out> alternative)
            : base(() =>
            {
                IScalar<Out> result;
                if (condition.Value())
                {
                    result = consequent;
                }
                else
                {
                    result = alternative;
                }
                return result.Value();
            })
        { }
    }
}
