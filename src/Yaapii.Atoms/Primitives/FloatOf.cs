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

#pragma warning disable CS1591

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A float out of text.
    /// </summary>
    public sealed class FloatOf : IScalar<float>
    {
        private readonly ScalarOf<float> val;

        /// <summary>
        /// A float out of a <see cref="string"/> using invariant culture.
        /// </summary>
        /// <param name="str">a float as a string</param>
        public FloatOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using invariant culture.
        /// </summary>
        /// <param name="text">a float as a text</param>
        public FloatOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A float out of a <see cref="string"/>.
        /// </summary>
        /// <param name="str">a float as a string</param>
        /// <param name="culture">culture of the string</param>
        public FloatOf(String str, CultureInfo culture) : this(new TextOf(str), culture)
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="text">a float as a text</param>
        /// <param name="culture">a culture of the string</param>
        public FloatOf(IText text, CultureInfo culture) : this(new Live<float>(() => float.Parse(text.AsString(), culture.NumberFormat)))
        { }

        public FloatOf(IScalar<float> value)
        {
            val = new ScalarOf<float>(value);
        }

        /// <summary>
        /// Get the float.
        /// </summary>
        /// <returns>the float</returns>
        public float Value()
        {
            return val.Value();
        }
    }
}
