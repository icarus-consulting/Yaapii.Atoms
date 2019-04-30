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
using Yaapii.Atoms.Enumerator;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Lookup if an item is in a enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Contains<T> : IScalar<bool>
    {
        private readonly IEnumerable<T> _items;
        private readonly Func<T, bool> _match;

        /// <summary>
        /// Lookup if an item is in a enumerable by calling .Equals(...) of the item.
        /// </summary>
        /// <param name="item">item to lookup</param>
        /// <param name="src">enumerable to test</param>
        public Contains(IEnumerable<T> src, T item) : this(
            src,
            (cdd) => cdd.Equals(item))
        { }

        /// <summary>
        /// Lookup if any item matches the given function
        /// </summary>
        /// <param name="items">enumerable to search through</param>
        /// <param name="match">check to perform on each item</param>
        public Contains(IEnumerable<T> items, Func<T, bool> match)
        {
            _match = match;
            _items = items;
        }

        /// <summary>
        /// see if the item is in the enumerable.
        /// </summary>
        /// <returns>true if item is in the enumerable</returns>
        public bool Value()
        {
            return new Enumerator.Contains<T>(_items.GetEnumerator(), _match).Value();
        }
    }
}
