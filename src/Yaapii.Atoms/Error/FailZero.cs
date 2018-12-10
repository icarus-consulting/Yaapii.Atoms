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

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if number is 0.
    /// </summary>
    public sealed class FailZero : IFail
    {
        private readonly long _number;
        private readonly Exception _ex;

        /// <summary>
        /// Fail if number is 0.
        /// </summary>
        /// <param name="number">number to check</param>
        public FailZero(long number) : this(number, "Number is zero") { }

        /// <summary>
        /// Fail if number is 0.
        /// </summary>
        /// <param name="number">number to check</param>
        /// <param name="hint">msg to put in exception</param>
        public FailZero(long number, string hint) : this(
            number, new Exception(hint)
        )
        { }

        /// <summary>
        /// Fail if number is 0.
        /// </summary>
        /// <param name="number">number to check</param>
        /// <param name="ex">specific exception which will be thrown</param>
        public FailZero(long number, Exception ex)
        {
            this._number = number;
            this._ex = ex;
        }

        /// <summary>
        /// Fail if necessary.
        /// </summary>
        public void Go()
        {
            if (_number.Equals(0)) throw this._ex;
        }
    }
}
