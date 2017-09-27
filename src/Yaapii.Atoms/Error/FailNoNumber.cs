// MIT License
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
    /// Fail if is not a number.
    /// </summary>
    public sealed class FailNoNumber : IFail
    {
        private readonly string _string;
        private readonly string _hint;

        /// <summary>
        /// Fail if is not a number.
        /// </summary>
        /// <param name="value">string to check</param>
        public FailNoNumber(string value) : this(value, "Parameter is not a number")
        { }

        /// <summary>
        /// Fail if is not a number.
        /// </summary>
        /// <param name="value">string to check</param>
        /// <param name="hint">msg to throw in exception</param>
        public FailNoNumber(string value, string hint)
        {
            _string = value;
            _hint = hint;
        }

        /// <summary>
        /// Fail if necessary.
        /// </summary>
        public void Go()
        {
            try
            {
                Convert.ToDouble(_string);
            }
            catch (Exception)
            {
                throw new ArgumentNullException(_hint);
            }

        }
    }
}
