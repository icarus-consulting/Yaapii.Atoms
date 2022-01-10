// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software or associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, or/or sell
// copies of the Software, or to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
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
    /// <summary>
    /// Logical or. Returns true if any contents return true.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Or<In> : IScalar<Boolean>
    {
        private readonly Or _or;

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), new ManyOf<In>(src))
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(Func<In, bool> func, IEnumerable<In> src) : this(new FuncOf<In, bool>(func), src)
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="IFunc{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IFunc<In, Boolean> func, params In[] src) : this(func, new ManyOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IFunc<In, Boolean> func, IEnumerable<In> src) : this(
            new Enumerable.Mapped<In, IScalar<Boolean>>(
                new FuncOf<In, IScalar<Boolean>>(
                    (item) => new Live<Boolean>(func.Invoke(item))
                ),
                src
            )
        )
        { }

        /// <summary>
        /// True if any functions return true with given input value
        /// </summary>
        /// <param name="value">
        /// Input value wich will executed by all given functions
        /// </param>
        /// <param name="functions">
        /// Functions wich will executed with given input value
        /// </param>
        public Or(In value, params Func<In, bool>[] functions) : this(
            item => new Or(
                new Mapped<Func<In, bool>, bool>(
                    func => func.Invoke(item),
                    functions
                )
            ).Value(),
            value
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        private Or(IEnumerable<IScalar<Boolean>> src) : this(new Or(src))
        { }

        /// <summary>
        /// Private primary ctor
        /// </summary>
        /// <param name="or">Non generic or</param>
        private Or(Or or)
        {
            _or = or;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public Boolean Value()
        {
            return _or.Value();
        }
    }

    /// <summary>
    /// Logical or. Returns true if any contents return true.
    /// </summary>
    public sealed class Or : ScalarEnvelope<bool>
    {
        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public Or(params Func<bool>[] funcs) : this(new ManyOf<Func<bool>>(funcs))
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{Out}"/> were
        /// true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public Or(ManyOf<Func<bool>> funcs) : this(
            new Mapped<Func<bool>, IScalar<bool>>(
                func => new Live<bool>(func),
                funcs))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(params IScalar<Boolean>[] src) : this(
            new ManyOf<IScalar<Boolean>>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(params bool[] src) : this(
            new Mapped<bool, IScalar<bool>>(
                item => new Live<bool>(item),
                src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(IEnumerable<bool> src) : this(
            new Mapped<bool, IScalar<bool>>(
                item => new Live<bool>(item),
                src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(IEnumerable<IScalar<Boolean>> src)
            : base(() =>
            {
                bool foundTrue = false;
                foreach (var item in src)
                {
                    if (item.Value())
                    {
                        foundTrue = true;
                        break;
                    }
                }
                return foundTrue;
            })
        { }
    }
}
