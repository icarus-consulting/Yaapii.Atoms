// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
using System.Diagnostics;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Enumerable that logs object T when it is iterated.
    /// T is logged right after the underlying enumerator is moved.
    /// </summary>
    public sealed class Logging<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> origin;
        private readonly Action<T> log;

        /// <summary>
        /// Enumerable that logs object T to debug console when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public Logging(IEnumerable<T> origin) : this(origin, (item) => Debug.WriteLine(item.ToString()))
        { }

        /// <summary>
        /// Enumerable that logs object T when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public Logging(IEnumerable<T> origin, Action<T> log)
        {
            this.origin = origin;
            this.log = log;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return
                new LoggingEnumerator<T>(
                    this.origin.GetEnumerator(),
                    this.log
                );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// Enumerable that logs object T when it is iterated.
    /// T is logged right after the underlying enumerator is moved.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// Enumerable that logs object T to debug console when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public static Logging<T> New<T>(IEnumerable<T> origin) => new Logging<T>(origin);

        /// <summary>
        /// Enumerable that logs object T when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public static Logging<T> New<T>(IEnumerable<T> origin, Action<T> log) => new Logging<T>(origin, log);
    }
}
