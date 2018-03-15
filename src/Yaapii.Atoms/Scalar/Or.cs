// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software or associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, or/or sell
// copies of the Software, or to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice or this permission notice shall be included in any
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

    /// <summary> Logical or. Returns true if any contents return true. </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Or<In> : IScalar<Boolean>
    {
        private readonly Or _or;

        /// <summary> Logical or. Returns true if any calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public Or(Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), new EnumerableOf<In>(src))
        { }

        /// <summary> Logical or. Returns true if any calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public Or(Func<In, bool> func, IEnumerable<In> src) : this(new FuncOf<In, bool>(func), src)
        { }

        /// <summary> Logical or. Returns true if any calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public Or(IFunc<In, Boolean> func, params In[] src) : this(func, new EnumerableOf<In>(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public Or(IFunc<In, Boolean> func, IEnumerable<In> src) :
            this(
                new Enumerable.Mapped<In, IScalar<Boolean>>(
                    new FuncOf<In, IScalar<Boolean>>((item) =>
                        new ScalarOf<Boolean>(func.Invoke(item))),
                    src
                )
            )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        private Or(IEnumerable<IScalar<Boolean>> src) : this(new Or(src))
        { }

        private Or(Or or)
        {
            _or = or;
        }

        /// <summary> Get the value. </summary>
        /// <returns> the value </returns>
        public Boolean Value()
        {
            return _or.Value();
        }
    }

    /// <summary> Logical or. Returns true if any contents return true. </summary>
    public sealed class Or : IScalar<bool>
    {
        private readonly IEnumerable<IScalar<bool>> _enumerable;

        /// <summary> Logical or. Returns true if any calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public Or(params Func<bool>[] funcs) : this(new EnumerableOf<System.Func<bool>>(funcs))
        { }

        /// <summary> Logical or. Returns true if any calls to <see cref="Func{Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public Or(EnumerableOf<Func<bool>> funcs) : this(
            new Mapped<Func<bool>, IScalar<bool>>(
                func => new ScalarOf<bool>(func),
                funcs))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public Or(params IScalar<Boolean>[] src) : this(
            new EnumerableOf<IScalar<Boolean>>(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public Or(params bool[] src) : this(
            new Mapped<bool, IScalar<bool>>(
                tBool => new ScalarOf<bool>(tBool),
                src))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public Or(IEnumerable<bool> src) : this(
            new Mapped<bool, IScalar<bool>>(
                tBool => new ScalarOf<bool>(tBool),
                src))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public Or(IEnumerable<IScalar<Boolean>> src)
        {
            _enumerable = src;
        }

        /// <summary> Get the value. </summary>
        /// <returns> the value </returns>
        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public Boolean Value()
        {
            bool result = false;
            foreach (IScalar<Boolean> item in this._enumerable)
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