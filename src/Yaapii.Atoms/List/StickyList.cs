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

#pragma warning disable CS1591
#pragma warning disable MaxPublicMethodCount // a public methods count maximum
#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable NoProperties // No Properties
#pragma warning disable MaxClassLength // Class length max


namespace Yaapii.Atoms.Lists
{
    /// <summary>
    /// A <see cref="List{X}"/> which returns the same items from a cache, always.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    [Obsolete("This class is obsolete and will be removed in future versions. Use List.Of")]
    public sealed class StickyList<X> : List.Envelope<X>
    {
        /// <summary>
        /// A <see cref="List{X}"/> which returns the same items from a cache, always.
        /// </summary>
        /// <param name="items">items to cache</param>
        public StickyList(params X[] items) :
            this(new List<X>(items))
        { }

        /// <summary>
        /// A <see cref="List{X}"/> which returns the same items from a cache, always.
        /// </summary>
        /// <param name="list">list to cache</param>
        public StickyList(IList<X> list) : base(
            () => list, false
        )
        { }
    }
}