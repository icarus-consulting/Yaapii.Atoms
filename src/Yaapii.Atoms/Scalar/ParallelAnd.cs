// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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

using System.Collections.Generic;
using System.Threading.Tasks;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Logical conjunction, in multiple threads. Returns true if all contents return true.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ParallelAnd<T> : IScalar<bool>
    {
        private System.Collections.Generic.IEnumerable<IScalar<bool>> iterable;

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(params IScalar<bool>[] src) : this(
            new ManyOf<IScalar<bool>>(src)
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public ParallelAnd(IAction<T> proc, params T[] src) : this(
            new FuncOf<T, bool>(proc, true), src
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public ParallelAnd(IFunc<T, bool> func, params T[] src) : this(
            func, new ManyOf<T>(src)
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public ParallelAnd(IAction<T> proc, System.Collections.Generic.IEnumerable<T> src) : this(
            new FuncOf<T, bool>(proc, true), src
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public ParallelAnd(IFunc<T, bool> func, System.Collections.Generic.IEnumerable<T> src) : this(
            new Mapped<T, IScalar<bool>>(
                i => new ScalarOf<bool>(func.Invoke(i)),
                src)
            )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(System.Collections.Generic.IEnumerable<IScalar<bool>> src)
        {
            this.iterable = src;
        }

        public bool Value()
        {
            var result = true;

            Parallel.ForEach(this.iterable, test =>
            {
                if (!test.Value())
                {
                    result = false;
                }
            });

            return result;
        }
    }

    public static class ParallelAnd
    {
        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="act"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IAction<T> act, params T[] src)
            => new ParallelAnd<T>(act, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IFunc<T, bool> func, params T[] src)
            => new ParallelAnd<T>(func, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IAction<T> proc, System.Collections.Generic.IEnumerable<T> src)
            => new ParallelAnd<T>(proc, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IFunc<T, bool> func, System.Collections.Generic.IEnumerable<T> src)
            => new ParallelAnd<T>(func, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(System.Collections.Generic.IEnumerable<IScalar<bool>> src)
            => new ParallelAnd<T>(src);
    }
}
