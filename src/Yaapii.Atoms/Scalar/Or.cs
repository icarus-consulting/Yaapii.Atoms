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
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// <see cref="IScalar{T}"/> that is a logical disjunction.
    /// </summary>
    public sealed class Or<In> : IScalar<Boolean>
    {
        private readonly IEnumerable<IScalar<Boolean>> origin;

        /// <summary>
        /// Logical or. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(Action<In> proc, params In[] src) : this(new ActionOf<In>(input => proc.Invoke(input)), src)
        { }

        /// <summary>
        /// Logical or. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(Action<In> proc, IEnumerable<In> src) : this(new ActionOf<In>(ipt => proc.Invoke(ipt)), src)
        { }

        /// <summary>
        /// Logical or. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IAction<In> proc, params In[] src) : this(
            proc, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// Logical or. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IAction<In> proc, IEnumerable<In> src) : this(new FuncOf<In, bool>(input => { proc.Invoke(input); return true; }), src)
        { }

        /// <summary>
        /// Logical or. Returns true if all calls to <see cref="Func{In, Out}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(System.Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// Logical or. Returns true if all calls to <see cref="IFunc{In, Out}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IFunc<In, Boolean> func, params In[] src) : this(func, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// Logical or. Returns true if all calls to <see cref="IFunc{In, Out}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IFunc<In, Boolean> func, IEnumerable<In> src) :
            this(
                new Enumerable.Mapped<In, IScalar<Boolean>>(
                    new FuncOf<In, IScalar<Boolean>>((item) =>
                        new ScalarOf<Boolean>(func.Invoke(item))),
                    src
                )
            )
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">functions returning scalars to chain with or</param>
        public Or(params Func<Boolean>[] scalars) : this(
            new Enumerable.Mapped<Func<bool>, IScalar<bool>>(
                fnc => new ScalarOf<bool>(fnc),
                scalars))
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">functions returning scalars to chain with or</param>
        public Or(params IFunc<Boolean>[] scalars) : this(
            new Enumerable.Mapped<IFunc<bool>, IScalar<bool>>(
                fnc => new ScalarOf<bool>(fnc),
                scalars
            )
        )
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">scalars to chain with or</param>        
        public Or(params IScalar<Boolean>[] scalars) : this(new EnumerableOf<IScalar<bool>>(scalars))
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">scalars to chain with or</param>
        public Or(IEnumerable<IScalar<Boolean>> scalars)
        {
            this.origin = scalars;
        }

        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public Boolean Value()
        {
            bool result = false;
            foreach (IScalar<Boolean> item in this.origin)
            {
                if (item.Value())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

    }
}
