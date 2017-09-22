/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Func that caches the result and returns from cache.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class StickyFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Dictionary<In, Out> _cache;

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(System.Func<In, Out> fnc) : 
            this(new FuncOf<In, Out>((X) => fnc(X)))
        { }

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(IFunc<In, Out> fnc)
        {
            this._func = fnc;
            this._cache = new Dictionary<In, Out>();
        }

        public Out Invoke(In input)
        {
            if (!this._cache.ContainsKey(input))
            {
                this._cache.Add(input, this._func.Invoke(input));
            }
            return this._cache[input];
        }

    }
}
