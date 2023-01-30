// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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

using System.Collections.Generic;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Comparer comparing two KeyValuePairs by key
    /// </summary>
    /// <typeparam name="Key">Key Type</typeparam>
    /// <typeparam name="Value">Value Type</typeparam>
    internal sealed class KeyComparer<Key, Value> : IComparer<KeyValuePair<Key, Value>>
    {
        private readonly IComparer<Key> cmp;

        /// <summary>
        /// Comparer comparing two KeyValuePairs by key
        /// </summary>
        /// <param name="cmp">Comparer compairing the key type</param>
        public KeyComparer(IComparer<Key> cmp)
        {
            this.cmp = cmp;
        }

        public int Compare(KeyValuePair<Key, Value> x, KeyValuePair<Key, Value> y)
        {
            return this.cmp.Compare(x.Key, y.Key);
        }
    }
}
