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

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that is threadsafe.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class SyncFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object _lck;

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public SyncFunc(Func<In, Out> fnc) : this(new FuncOf<In, Out>((X) => fnc(X)))
        { }

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public SyncFunc(IFunc<In, Out> fnc): this(fnc, fnc)
        { }

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        /// <param name="lck">object that will be locked</param>
        public SyncFunc(IFunc<In, Out> fnc, object lck)
        {
            this._func = fnc;
            this._lck = lck;
        }

        /// <summary>
        /// Invoke function with given input and retrieve output.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Out Invoke(In input)
        {
            lock (this._lck)
            {
                return this._func.Invoke(input);
            }
        }

    }
}
