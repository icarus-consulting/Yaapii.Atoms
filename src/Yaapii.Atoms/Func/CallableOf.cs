﻿// MIT License
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
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Misc;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that has only output.
    /// </summary>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class FuncOf<Out> : IFunc<Out>
    {
        /// <summary>
        /// func that will be called
        /// </summary>
        private readonly IFunc<bool, Out> _func;

        /// <summary>
        /// Function that has only output.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public FuncOf(System.Func<Out> fnc)
        {
            this._func = new FuncOf<bool, Out>(() => fnc.Invoke());
        }

        /// <summary>
        /// Call function and retrieve output.
        /// </summary>
        /// <returns>the output</returns>
        public Out Invoke()
        {
            return this._func.Invoke(true);
        }
    }
}