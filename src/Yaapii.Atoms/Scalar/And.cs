// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
// modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary> Logical and. Returns true if all contents return true. </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class And<In> : ScalarEnvelope<Boolean>
    {
        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), new ManyOf<In>(src))
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(Func<In, bool> func, IEnumerable<In> src) : this(new FuncOf<In, bool>(func), src)
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(IFunc<In, Boolean> func, params In[] src) : this(func, new ManyOf<In>(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(IFunc<In, Boolean> func, IEnumerable<In> src) :
            this(
                new Enumerable.Mapped<In, IScalar<Boolean>>(
                    new FuncOf<In, IScalar<Boolean>>((item) =>
                        new Live<Boolean>(func.Invoke(item))),
                    src
                )
            )
        { }

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="functions"> Functions wich will executed with given input value </param>
        public And(In value, params Func<In, bool>[] functions)
            : this(tValue => new And(new Mapped<Func<In, bool>, bool>(tFunc => tFunc.Invoke(tValue), functions)).Value(), value)
        { }

        /// <summary></summary>
        /// <param name="src"></param>
        private And(IEnumerable<IScalar<Boolean>> src)
            : base(() =>
            {
                Boolean result = true;
                foreach (IScalar<Boolean> item in src)
                {
                    if (!item.Value())
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            })
        { }
    }

    /// <summary> Logical and. Returns true if all contents return true. </summary>
    public sealed class And : ScalarEnvelope<bool>
    {
        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public And(params Func<bool>[] funcs) : this(new ManyOf<System.Func<bool>>(funcs))
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public And(ManyOf<Func<bool>> funcs) : this(
            new Mapped<Func<bool>, IScalar<bool>>(
                func => new Live<bool>(func),
                funcs))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params IScalar<Boolean>[] src) : this(
            new ManyOf<IScalar<Boolean>>(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params bool[] src) : this(
            new Mapped<bool, IScalar<bool>>(
                tBool => new Live<bool>(tBool),
                src))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<bool> src) : this(
            new Mapped<bool, IScalar<bool>>(
                tBool => new Live<bool>(tBool),
                src))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<IScalar<Boolean>> src)
            : base(() =>
            {
                Boolean result = true;
                foreach (IScalar<Boolean> item in src)
                {
                    if (!item.Value())
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            })
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(Func<In, bool> func, params In[] src)
            => new And<In>(func, src);

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(Func<In, bool> func, IEnumerable<In> src)
            => new And<In>(func, src);

        /// <summary> Logical and. Returns true if all calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(IFunc<In, Boolean> func, params In[] src)
            => new And<In>(func, src);

        /// <summary> ctor </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(IFunc<In, Boolean> func, IEnumerable<In> src)
            => new And<In>(func, src);

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="functions"> Functions wich will executed with given input value </param>
        public static IScalar<bool> New<In>(In value, params Func<In, bool>[] functions)
            => new And<In>(value, functions);
    }
}
