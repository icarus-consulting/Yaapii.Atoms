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

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// An enumerator that logs the object T when it is moved.
    /// </summary>
    public sealed class LoggingEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> origin;
        private readonly Action<T> log;

        /// <summary>
        /// An enumerator that logs the object T when it is moved.
        /// </summary>
        public LoggingEnumerator(IEnumerator<T> origin, Action<T> log)
        {
            this.origin = origin;
            this.log = log;
        }

        public T Current => this.origin.Current;

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            this.origin.Dispose();
        }

        public bool MoveNext()
        {
            var moved = this.origin.MoveNext();
            if (moved)
            {
                this.log(Current);
            }
            return moved;
        }

        public void Reset()
        {
            this.origin.Reset();
        }
    }

    /// <summary>
    /// An enumerator that logs the object T when it is moved.
    /// </summary>
    public static class LoggingEnumerator
    {
        /// <summary>
        /// An enumerator that logs the object T when it is moved.
        /// </summary>
        public static LoggingEnumerator<T> New<T>(IEnumerator<T> origin, Action<T> log) =>
            new LoggingEnumerator<T>(origin, log);
    }
}
