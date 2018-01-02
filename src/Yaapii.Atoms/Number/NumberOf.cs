using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
            new TextOf(text),
            new ScalarOf<IFormatProvider>(() => 
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                }
            )
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        public NumberOf(string text) : this(
            new TextOf(text), 
            new ScalarOf<IFormatProvider>(
                () => NumberFormatInfo.CurrentInfo
            )
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string text, IFormatProvider provider) : this(
            new TextOf(text), 
            new ScalarOf<IFormatProvider>(provider)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="numberFormat">a number format provider</param>
        public NumberOf(IText text, IScalar<IFormatProvider> numberFormat)
        {
            _lng = new StickyScalar<long>(() => Convert.ToInt64(text.AsString(), numberFormat.Value()));
            _itg = new StickyScalar<int>(() => Convert.ToInt32(text.AsString(), numberFormat.Value()));
            _flt = new StickyScalar<float>(() => Convert.ToSingle(text.AsString(), numberFormat.Value()));
            _dbl = new StickyScalar<double>(() => Convert.ToDouble(text.AsString(), numberFormat.Value()));
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
    }
}
