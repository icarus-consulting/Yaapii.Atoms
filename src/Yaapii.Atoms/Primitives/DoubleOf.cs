using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Scalar
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
        /// <param name="str">a double as a text</param>
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
        /// <param name="str">a double as a text</param>
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

        public Double Value()
        {
            return _val.Value();
        }
    }
}
