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
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Misc;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that has input and output
    /// </summary>
    /// <typeparam name="X">input</typeparam>
    /// <typeparam name="Y">output</typeparam>
    public sealed class FuncOf<X, Y> : IFunc<X, Y>
    {
        private readonly System.Func<X, Y> _func;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="callable"></param>
        public FuncOf(System.Func<Y> callable) : this(input => callable.Invoke())
        { }

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="proc">procedure to execute</param>
        /// <param name="result"></param>
        public FuncOf(IProc<X> proc, Y result) : this(
                input =>
                {
                    proc.Exec(input);
                    return result;
                }
        )
        { }

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="func">function to execute</param>
        public FuncOf(System.Func<X, Y> func)
        {
            _func = func;
        }

        /// <summary>
        /// Generate the Output from the input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Y Invoke(X input)
        {
            return _func.Invoke(input);
        }
    }
}
