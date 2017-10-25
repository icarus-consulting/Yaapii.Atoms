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
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A double out of text.
    /// </summary>
    public sealed class DoubleOf : IScalar<Double>
    {
        private readonly IScalar<Double> _val;

        /// <summary>
        /// A double out of <see cref="string"/>.
        /// </summary>
        /// <param name="str">a double as a string</param>
        public DoubleOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A double out of <see cref="IText"/>.
        /// </summary>
        /// <param name="text">a double as a text</param>
        public DoubleOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A double out of <see cref="string"/> using the given <see cref="Encoding"/>.
        /// </summary>
        /// <param name="str">a double as a string</param>
        /// <param name="culture">culture of the given string</param>
        public DoubleOf(String str, CultureInfo culture) : this(new TextOf(str), culture)
        { }

        /// <summary>
        /// A double out of <see cref="IText"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="text">a double as a text</param>
        /// <param name="culture">culture of the given text</param>
        public DoubleOf(IText text, CultureInfo culture) : this(new ScalarOf<Double>(() => Convert.ToDouble(text.AsString(), culture.NumberFormat)))
        { }

        /// <summary>
        /// A double out of a encapsulating <see cref="IScalar{T}"/>
        /// </summary>
        /// <param name="value">a scalar of the double to sum</param>
        public DoubleOf(IScalar<Double> value)
        {
            _val = value;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>value as double</returns>
        public Double Value()
        {
            return _val.Value();
        }
    }
}
