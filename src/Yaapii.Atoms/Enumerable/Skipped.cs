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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Skipped<T> : EnumerableEnvelope<T>
    {
        /// <summary>
        /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerable">enumerable to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public Skipped(IEnumerable<T> enumerable, int skip) : base(new ScalarOf<IEnumerable<T>>(
            () =>
            new EnumerableOf<T>(
                new SkippedEnumerator<T>(enumerable.GetEnumerator(), skip))))
        { }
    }
}
#pragma warning restore NoGetOrSet // No Statics