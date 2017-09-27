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
using System.IO;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Proc that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class IoCheckedProc<X> : IProc<X>
    {
        /// <summary>
        /// original proc
        /// </summary>
        private readonly IProc<X> _proc;

        /// <summary>
        /// Proc that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="prc">procedure to execute</param>
        public IoCheckedProc(IProc<X> prc)
        {
            this._proc = prc;
        }

        /// <summary>
        /// Execute the proc with the input argument.
        /// </summary>
        /// <param name="input">the input argument</param>
        public void Exec(X input)
        {
            new IoCheckedFunc<X, Boolean>(
                new FuncOf<X, Boolean>((arg) =>
                 {
                     this._proc.Exec(arg);
                     return true;
                 })
            ).Invoke(input);
        }

    }
}
