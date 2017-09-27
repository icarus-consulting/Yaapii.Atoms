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
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Proc that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class UncheckedProc<X> : IProc<X>
    {
        private readonly IProc<X> _proc;

        /// <summary>
        /// Func that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="prc"></param>
        public UncheckedProc(IProc<X> prc)
        {
            this._proc = prc;
        }

        /// <summary>
        /// Execute procedure with given input.
        /// </summary>
        /// <param name="input"></param>
        public void Exec(X input)
        {
            new UncheckedFunc<X, Boolean>(new FuncOf<X, Boolean>(ipt => { this._proc.Exec(ipt); return true; })).Invoke(input);
        }

    }
}
