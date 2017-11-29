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
using System.IO;
using System.Text;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// Lookup if an item is in a enumerable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ContainsEnumerator<T> : IScalar<bool>
        where T: IComparable<T>
    {
        private readonly Func<T, bool> _match;
        private readonly IEnumerator<T> _src;

        /// <summary>
        /// Lookup the item in the src.
        /// </summary>
        /// <param name="src">src enumerable</param>
        /// <param name="match">lookup item</param>
        public ContainsEnumerator(IEnumerator<T> src, Func<T,bool> match)
        {
            _match = match;
            _src = src;
        }

        /// <summary>
        /// Determine if the item is in the enumerable.
        /// </summary>
        /// <returns>true if item is present in enumerable.</returns>
        public bool Value()
        {
            var contains = _src.MoveNext();
            for (var cur = 0; !_match.Invoke(this._src.Current); cur++)
            {
                if (!this._src.MoveNext())
                {
                    contains = false;
                    break;
                }
            }

            return contains;
        }
    }
}
