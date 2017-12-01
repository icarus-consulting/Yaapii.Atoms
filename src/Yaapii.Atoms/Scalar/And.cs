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
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Logical and. Returns true if all contents return true.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class And<In> : IScalar<Boolean>
    {
        private readonly IEnumerable<IScalar<Boolean>> _enumerable;

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(Action<In> proc, params In[] src) : this(new ActionOf<In>(input => proc.Invoke(input)), src)
        { }

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(Action<In> proc, IEnumerable<In> src) : this(new ActionOf<In>(ipt => proc.Invoke(ipt)), src)
        { }

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IAction<In> proc, params In[] src) : this(
            proc, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IAction<In> proc, IEnumerable<In> src) : this(new FuncOf<In, bool>(input => { proc.Invoke(input); return true; }), src)
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="Func{In, Out}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(System.Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="IFunc{In, Out}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IFunc<In, Boolean> func, params In[] src) : this(func, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IFunc<In, Boolean> func, IEnumerable<In> src) :
            this(
                new Enumerable.Mapped<In, IScalar<Boolean>>(
                    src,
                    new FuncOf<In, IScalar<Boolean>>((item) => 
                        new ScalarOf<Boolean>(func.Invoke(item)))                    
                )
            )
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="Func{In, Out}" /> were true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public And(params System.Func<bool>[] funcs) : this(new EnumerableOf<System.Func<bool>>(funcs))
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="Func{Out}" /> were true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public And(EnumerableOf<System.Func<bool>> funcs) : this(
            new Enumerable.Mapped<System.Func<bool>, IScalar<bool>>(
                funcs,
                func => new ScalarOf<bool>(func)))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public And(params IScalar<Boolean>[] src) : this(
            new EnumerableOf<IScalar<Boolean>>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public And(IEnumerable<IScalar<Boolean>> src)
        {
            this._enumerable = src;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public Boolean Value()
        {
            Boolean result = true;
            foreach (IScalar<Boolean> item in this._enumerable)
            {
                if (!item.Value())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

    }
}
