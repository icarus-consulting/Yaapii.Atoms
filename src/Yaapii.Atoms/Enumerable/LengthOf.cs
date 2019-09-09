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
using System.Collections;
using Yaapii.Atoms.Enumerator;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Length of an <see cref="IEnumerable"/>.
    /// Important: You must understand that this object will iterate over the passed items 
    /// every time when you call .Value(). It is recommended to use a StickyScalar, 
    /// if you want to re-use its value.
    /// </summary>
    public sealed class LengthOf : IScalar<Int32>
    {
        private readonly IEnumerable items;

        /// <summary>
        /// Length of an <see cref="IEnumerable"/>
        /// </summary>
        /// <param name="items">the enumerable</param>
        public LengthOf(IEnumerable items)
        {
            this.items = items;
        }

        /// <summary>
        /// Get the length.
        /// </summary>
        /// <returns>the length</returns>
        public Int32 Value()
        {
            return new Enumerator.LengthOf(this.items.GetEnumerator()).Value();
        }

    }
}
