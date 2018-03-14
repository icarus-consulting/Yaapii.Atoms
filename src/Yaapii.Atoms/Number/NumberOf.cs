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
using System.Globalization;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

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
            new ScalarOf<long>(() => Convert.ToInt64(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new ScalarOf<int>(() => Convert.ToInt32(
                text, 
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(text, new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new ScalarOf<double>(() => Convert.ToDouble(
                text, 
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                }))
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        public NumberOf(string text) : this(
            new ScalarOf<long>(() => Convert.ToInt64(text, CultureInfo.InvariantCulture)),
            new ScalarOf<int>(() => Convert.ToInt32(text, CultureInfo.InvariantCulture)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(text, CultureInfo.InvariantCulture)),
            new ScalarOf<double>(() => Convert.ToDouble(text, CultureInfo.InvariantCulture))
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string text, IFormatProvider provider) : this(
            new ScalarOf<long>(() => Convert.ToInt64(text, provider)),
            new ScalarOf<int>(() => Convert.ToInt32(text, provider)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(text, provider)),
            new ScalarOf<double>(() => Convert.ToDouble(text, provider))
        )
        { }


        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="integer">The integer</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(int integer, IFormatProvider provider) : this(
            new ScalarOf<long>(() => Convert.ToInt64(integer, provider)),
            new ScalarOf<int>(() => Convert.ToInt32(integer, provider)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(integer, provider)),
            new ScalarOf<double>(() => Convert.ToDouble(integer, provider))
            )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="integer">The integer</param>
        public NumberOf(int integer) : this(
            new ScalarOf<long>(() => Convert.ToInt64(integer, CultureInfo.InvariantCulture)),
            new ScalarOf<int>(() => Convert.ToInt32(integer, CultureInfo.InvariantCulture)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(integer, CultureInfo.InvariantCulture)),
            new ScalarOf<double>(() => Convert.ToDouble(integer, CultureInfo.InvariantCulture))
            )
           
        { }

        /// <summary>
        /// A <see cref="double"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="dbl">the double</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(double dbl, IFormatProvider provider) : this(
            new ScalarOf<long>(() => Convert.ToInt64(dbl, provider)),
            new ScalarOf<int>(() => Convert.ToInt32(dbl, provider)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(dbl, provider)),
            new ScalarOf<double>(() => Convert.ToDouble(dbl, provider))
            )
        { }

        /// <summary>
        /// A <see cref="double"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="dbl">the double</param>
        public NumberOf(double dbl) : this(
            new ScalarOf<long>(() => Convert.ToInt64(dbl, CultureInfo.InvariantCulture)),
            new ScalarOf<int>(() => Convert.ToInt32(dbl, CultureInfo.InvariantCulture)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(dbl, CultureInfo.InvariantCulture)),
            new ScalarOf<double>(() => Convert.ToDouble(dbl, CultureInfo.InvariantCulture))
           )
        { }

        /// <summary>
        /// A <see cref="long"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng">the long</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(long lng, IFormatProvider provider) : this(
            new ScalarOf<long>(() => Convert.ToInt64(lng, provider)),
            new ScalarOf<int>(() => Convert.ToInt32(lng, provider)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(lng, provider)),
            new ScalarOf<double>(() => Convert.ToDouble(lng, provider)))
        { }

        /// <summary>
        /// A <see cref="long"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberOf(long lng) : this(
            new ScalarOf<long>(() => Convert.ToInt64(lng, CultureInfo.InvariantCulture)),
            new ScalarOf<int>(() => Convert.ToInt32(lng, CultureInfo.InvariantCulture)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(lng, CultureInfo.InvariantCulture)),
            new ScalarOf<double>(() => Convert.ToDouble(lng, CultureInfo.InvariantCulture))
            )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="flt">The float</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(float flt, IFormatProvider provider) : this(
            new ScalarOf<long>(() => Convert.ToInt64(flt, provider)),
            new ScalarOf<int>(() => Convert.ToInt32(flt, provider)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(flt, provider)),
            new ScalarOf<double>(() => Convert.ToDouble(flt, provider)))
        { }


        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="flt">The float</param>
        public NumberOf(float flt) : this(
            new ScalarOf<long>(() => Convert.ToInt64(flt, CultureInfo.InvariantCulture)),
            new ScalarOf<int>(() => Convert.ToInt32(flt, CultureInfo.InvariantCulture)),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(flt, CultureInfo.InvariantCulture)),
            new ScalarOf<double>(() => Convert.ToDouble(flt, CultureInfo.InvariantCulture))
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
            _lng = lng; //new StickyScalar<long>(() => { try { return Convert.ToInt64(text.AsString(), numberFormat.Value()); } catch (FormatException) { throw ArgError(text); } });
            _itg = itg; //new StickyScalar<int>(() => { try { return Convert.ToInt32(text.AsString(), numberFormat.Value()); } catch (FormatException) { throw ArgError(text); } });
            _flt = flt; //new StickyScalar<float>(() => { try { return Convert.ToSingle(text.AsString(), numberFormat.Value()); } catch (FormatException) { throw ArgError(text); } });
            _dbl = dbl; //new StickyScalar<double>(() => { try { return Convert.ToDouble(text.AsString(), numberFormat.Value()); } catch (FormatException) { throw ArgError(text); } });
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
                    new FormattedText("'{0}' is not a number.", txt.AsString()
                ).AsString()
            );
        }
    }
}
