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
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if enum is empty.
    /// </summary>
    /// <typeparam name="T">type of enum</typeparam>
    public sealed class FailEmpty<T> : IFail
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly string _hint;

        /// <summary>
        /// Fail if enum is empty.
        /// </summary>
        /// <param name="enumerable">enum to check</param>
        public FailEmpty(IEnumerable<T> enumerable) : this(enumerable, "Collection is empty") { }

        /// <summary>
        /// Fail if enum is empty.
        /// </summary>
        /// <param name="enumerable">enum to check</param>
        /// <param name="hint">msg to display in exception</param>
        public FailEmpty(IEnumerable<T> enumerable, string hint)
        {
            _enumerable = enumerable;
            _hint = hint;
        }

        /// <summary>
        /// Fail if necessary.
        /// </summary>
        public void Go()
        {
            if (!_enumerable.GetEnumerator().MoveNext()) throw new Exception(_hint + " is empty");
        }
    }
}
