using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A float out of text.
    /// </summary>
    public sealed class FloatOf : IScalar<float>
    {
        private readonly IScalar<float> _val;

        /// <summary>
        /// A float out of a <see cref="string"/> using invariant culture.
        /// </summary>
        /// <param name="str">a float as a string</param>
        public FloatOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using invariant culture.
        /// </summary>
        /// <param name="str">a float as a text</param>
        public FloatOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A float out of a <see cref="string"/>.
        /// </summary>
        /// <param name="str">a float as a string</param>
        public FloatOf(String str, CultureInfo culture) : this(new TextOf(str), culture)
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="str">a float as a text</param>
        /// <param name="culture">a culture of the string</param>
        public FloatOf(IText text, CultureInfo culture) : this(new ScalarOf<float>(() => float.Parse(text.AsString(), culture.NumberFormat)))
        { }

        public FloatOf(IScalar<float> value)
        {
            _val = value;
        }

        public float Value()
        {
            return _val.Value();
        }
    }
}
