// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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

using System.Collections;
using System.Collections.Concurrent;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// An ArrayList converted to a IList&lt;object&gt;
    /// </summary>
    public sealed class ArrayListAsCollection : CollectionEnvelope<object>
    {
        /// <summary>
        /// A ArrayList converted to IList&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ArrayListAsCollection(ArrayList src) : base(() =>
            {
                var blocking = new BlockingCollection<object>();
                foreach (var lst in src)
                {
                    new Each<object>(item => blocking.Add(item), lst).Invoke();
                }
                return blocking.GetConsumingEnumerable().GetEnumerator();
            },
            false
        )
        { }
    }
}
