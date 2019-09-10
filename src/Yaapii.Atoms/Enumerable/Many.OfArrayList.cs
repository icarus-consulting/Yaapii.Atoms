﻿// MIT License
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    public partial class Many
    {
        /// <summary>
        /// A <see cref="ArrayList"/> converted to IEnumerable&lt;object&gt;
        /// </summary>
        public sealed class OfArrayList : Envelope<object>
        {
            /// <summary>
            /// A ArrayList converted to IEnumerable&lt;object&gt;
            /// </summary>
            /// <param name="src">source ArrayList</param>
            public OfArrayList(ArrayList src) : base(() =>
                {
                    var blocking = new BlockingCollection<object>();
                    foreach (var item in src)
                    {
                        blocking.Add(item);
                    }
                    return blocking;
                }
            )
            { }
        }

        /// <summary>
        /// A <see cref="ArrayList"/> converted to IEnumerable&lt;T&gt;
        /// </summary>
        public sealed class OfArrayList<T> : Envelope<T>
        {
            /// <summary>
            /// A ArrayList converted to IEnumerable&lt;object&gt;
            /// </summary>
            /// <param name="src">source ArrayList</param>
            public OfArrayList(ArrayList src) : base(() =>
                {
                    var blocking = new BlockingCollection<T>();
                    foreach (var item in src)
                    {
                        blocking.Add((T)item);
                    }

                    return blocking;
                }
            )
            { }
        }
    }
}