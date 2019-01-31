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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A ternary operation.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class Ternary<In, Out> : IScalar<Out>
    {
        private readonly IScalar<Boolean> _condition;
        private readonly IScalar<Out> _consequent;
        private readonly IScalar<Out> _alternative;

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
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(In input, IFunc<In, Boolean> cnd,
            IFunc<In, Out> cons, IFunc<In, Out> alter)
        :
            this(
                new ScalarOf<bool>(() => cnd.Invoke(input)),
                new ScalarOf<Out>(() => cons.Invoke(input)),
                new ScalarOf<Out>(() => alter.Invoke(input))
            )
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(Boolean cnd, Out cons, Out alter) :
            this(new ScalarOf<Boolean>(cnd), cons, alter)
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(IScalar<Boolean> cnd, Out cons, Out alter) :
            this(cnd, new ScalarOf<Out>(cons), new ScalarOf<Out>(alter))
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(IScalar<Boolean> cnd, IScalar<Out> cons, IScalar<Out> alter)
        {
            this._condition = cnd;
            this._consequent = cons;
            this._alternative = alter;
        }

        /// <summary>
        /// Get the value
        /// </summary>
        /// <returns></returns>
        public Out Value()
        {
            IScalar<Out> result;
            if (this._condition.Value())
            {
                result = this._consequent;
            }
            else
            {
                result = this._alternative;
            }
            return result.Value();
        }
    }
}
