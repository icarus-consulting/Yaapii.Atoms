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

using System;
using System.Globalization;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="int"/> of a text.
    /// </summary>
    public sealed class IntOf : IScalar<Int32>
    {
        private readonly ScalarOf<int> val;

        /// <summary>
        /// A int out of a <see cref="string"/> using invariant culture.
        /// </summary>
        /// <param name="str">a int as a string</param>
        public IntOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A int out of a <see cref="IText"/> using invariant culture.
        /// </summary>
        /// <param name="text">a int as a text</param>
        public IntOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A int out of a <see cref="string"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="str">a int as a string</param>
        /// <param name="culture">culture of the string</param>
        public IntOf(String str, CultureInfo culture) : this(new TextOf(str), culture)
        { }

        /// <summary>
        /// A int out of a <see cref="IText"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="text">a int as a string</param>
        /// <param name="culture">culture of the string</param>
        public IntOf(IText text, CultureInfo culture) : this(new ScalarOf<int>(() => Convert.ToInt32(text.AsString(), culture.NumberFormat)))
        { }

        /// <summary>
        /// A int out of a scalar.
        /// </summary>
        /// <param name="value">the scalar returning the float</param>
        private IntOf(IScalar<int> value)
        {
            val = new ScalarOf<int>(value);
        }

        /// <summary>
        /// Get the int.
        /// </summary>
        /// <returns>the int</returns>
        public Int32 Value()
        {
            return val.Value();
        }
    }
}
