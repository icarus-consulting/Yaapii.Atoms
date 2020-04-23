// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Number
{
    /// <summary>
    /// A parsed number
    /// </summary>
    public sealed class NumberOf : NumberEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="blockSeperator">seperator for blocks, for example 1.000</param>
        /// <param name="decimalSeperator">seperator for floating point numbers, for example 16,235 </param>
        public NumberOf(string text, string decimalSeperator, string blockSeperator) : base(
            new ScalarOf<double>(() =>
                Convert.ToDouble(
                    text,
                    new NumberFormatInfo()
                    {
                        NumberDecimalSeparator = decimalSeperator,
                        NumberGroupSeparator = blockSeperator
                    }
                )
            ),
            new ScalarOf<int>(() =>
                Convert.ToInt32(
                    text,
                    new NumberFormatInfo()
                    {
                        NumberDecimalSeparator = decimalSeperator,
                        NumberGroupSeparator = blockSeperator
                    }
                )
            ),
            new ScalarOf<long>(() =>
                Convert.ToInt64(
                    text,
                    new NumberFormatInfo()
                    {
                        NumberDecimalSeparator = decimalSeperator,
                        NumberGroupSeparator = blockSeperator
                    }
                )
            ),
            new ScalarOf<float>(() =>
                (float)Convert.ToDecimal(
                    text,
                    new NumberFormatInfo()
                    {
                        NumberDecimalSeparator = decimalSeperator,
                        NumberGroupSeparator = blockSeperator
                    }
                )
            )
        )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        public NumberOf(string str) : this(str, new ScalarOf<IFormatProvider>(() => CultureInfo.InvariantCulture))
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string str, IFormatProvider provider) : this(str, new ScalarOf<IFormatProvider>(provider))
        { }

        /// <summary>
        /// A <see cref="string"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string str, IScalar<IFormatProvider> provider) : base(
            new ScalarOf<double>(() =>
            {
                try
                {
                    return Convert.ToDouble(str, provider.Value());
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", str).AsString());
                }
            }),
            new ScalarOf<int>(() =>
            {
                try
                {
                    return Convert.ToInt32(str, provider.Value());
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", str).AsString());
                }
            }),
            new ScalarOf<long>(() =>
            {
                try
                {
                    return Convert.ToInt64(str, provider.Value());
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", str).AsString());
                }
            }),
            new ScalarOf<float>(() =>
            {
                try
                {
                    return Convert.ToSingle(str, provider.Value());
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", str).AsString());
                }
            })
        )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="integer">The integer</param>
        public NumberOf(int integer) : base(integer)
        { }

        /// <summary>
        /// A <see cref="double"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="dbl">The double</param>
        public NumberOf(double dbl) : base(dbl)
        { }

        /// <summary>
        /// A <see cref="long"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng">The long</param>
        public NumberOf(long lng) : base(lng)
        { }

        /// <summary>
        /// A <see cref="float"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="flt">The float</param>
        public NumberOf(float flt) : base(flt)
        { }

        public NumberOf(IScalar<long> lng, IScalar<int> itg, IScalar<float> flt, IScalar<double> dbl) : base(
            dbl, itg, lng, flt
        )
        { }
    }
}
