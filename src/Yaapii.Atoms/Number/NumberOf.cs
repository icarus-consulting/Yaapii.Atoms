﻿// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Texts;

namespace Yaapii.Atoms.Number
{
    /// <summary>
    /// A parsed number
    /// </summary>
    public sealed class NumberOf : INumber
    {
        private readonly IScalar<long> _lng;
        private readonly IScalar<float> _flt;
        private readonly IScalar<int> _itg;
        private readonly IScalar<double> _dbl;

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="blockSeperator">seperator for blocks, for example 1.000</param>
        /// <param name="decimalSeperator">seperator for floating point numbers, for example 16,235 </param>
        public NumberOf(string text, string decimalSeperator, string blockSeperator) : this(
            new Sticky<long>(() => Convert.ToInt64(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new Sticky<int>(() => Convert.ToInt32(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new Sticky<float>(() => (float)Convert.ToDecimal(text, new NumberFormatInfo()
            {
                NumberDecimalSeparator = decimalSeperator,
                NumberGroupSeparator = blockSeperator
            })),
            new Sticky<double>(() => Convert.ToDouble(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                }))
        )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        public NumberOf(string str) : this(str, new Sticky<IFormatProvider>(() => CultureInfo.InvariantCulture))
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string str, IFormatProvider provider) : this(str, new Sticky<IFormatProvider>(provider))
        { }

        /// <summary>
        /// A <see cref="string"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string str, IScalar<IFormatProvider> provider) : this(
            new LiveScalar<long>(
                () =>
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
            new Sticky<int>(
                () =>
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
            new Sticky<float>(
                () =>
                {
                    try
                    {
                        return Convert.ToSingle(str, provider.Value());
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException(new Formatted("'{0}' is not a number.", str).AsString());
                    }
                }),
            new Sticky<double>(
                () =>
                {
                    try
                    {
                        return Convert.ToDouble(str, provider.Value());
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
        public NumberOf(int integer) : this(
            new Sticky<long>(() => Convert.ToInt64(integer)),
            new Sticky<int>(integer),
            new Sticky<float>(() => Convert.ToSingle(integer)),
            new Sticky<double>(() => Convert.ToDouble(integer))
        )
        { }

        /// <summary>
        /// A <see cref="double"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="dbl">The double</param>
        public NumberOf(double dbl) : this(
            new Sticky<long>(() => Convert.ToInt64(dbl)),
            new Sticky<int>(() => Convert.ToInt32(dbl)),
            new Sticky<float>(() => Convert.ToSingle(dbl)),
            new Sticky<double>(dbl)
            )
        { }

        /// <summary>
        /// A <see cref="long"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng">The long</param>
        public NumberOf(long lng) : this(
            new Sticky<long>(() => lng),
            new Sticky<int>(() => Convert.ToInt32(lng)),
            new Sticky<float>(() => Convert.ToSingle(lng)),
            new Sticky<double>(() => Convert.ToDouble(lng))
            )
        { }

        /// <summary>
        /// A <see cref="float"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="flt">The float</param>
        public NumberOf(float flt) : this(
            new Sticky<long>(() => Convert.ToInt64(flt)),
            new Sticky<int>(() => Convert.ToInt32(flt)),
            new Sticky<float>(flt),
            new Sticky<double>(() => Convert.ToDouble(flt))
            )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="itg"></param>
        /// <param name="flt"></param>
        /// <param name="dbl"></param>
        public NumberOf(IScalar<long> lng, IScalar<int> itg, IScalar<float> flt, IScalar<double> dbl)
        {
            _lng = lng;
            _itg = itg;
            _flt = flt;
            _dbl = dbl;
        }

        /// <summary>
        /// Number as double representation
        /// Precision: ±5.0e−324 to ±1.7e308	(15-16 digits)
        /// </summary>
        /// <returns></returns>
        public double AsDouble()
        {
            return _dbl.Value();
        }

        /// <summary>
        /// Number as float representation
        /// Precision: 	±1.5e−45 to ±3.4e38	    (7 digits)
        /// </summary>
        /// <returns></returns>
        public float AsFloat()
        {
            return _flt.Value();
        }

        /// <summary>
        /// Number as integer representation
        /// Range -2,147,483,648 to 2,147,483,647	(Signed 32-bit integer)
        /// </summary>
        /// <returns></returns>
        public int AsInt()
        {
            return _itg.Value();
        }

        /// <summary>
        /// Number as long representation
        /// Range -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	(Signed 64-bit integer)
        /// </summary>
        /// <returns></returns>
        public long AsLong()
        {
            return _lng.Value();
        }

        private ArgumentException ArgError(IText txt)
        {
            return
                new ArgumentException(
                    new Formatted("'{0}' is not a number.", txt.AsString()
                ).AsString()
            );
        }
    }
}
