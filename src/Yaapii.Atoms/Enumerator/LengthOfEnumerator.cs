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
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// Length of an <see cref="IEnumerator"/>
    /// </summary>
    public sealed class LengthOfEnumerator : IScalar<Int32>
    {
        private readonly IEnumerator _enumerator;

        /// <summary>
        /// Length of an <see cref="IEnumerator"/>
        /// </summary>
        /// <param name="items">enumerator to count</param>
        public LengthOfEnumerator(IEnumerator items)
        {
            this._enumerator = items;
        }

        /// <summary>
        /// Get the length.
        /// </summary>
        /// <returns>the length</returns>
        public Int32 Value()
        {
            int size = 0;
            while (this._enumerator.MoveNext())
            {
                ++size;
            }
            return size;
        }
    }
}
