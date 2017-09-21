using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="int"/> of a text.
    /// </summary>
    public sealed class IntOf : IScalar<Int32>
    {
        private readonly IScalar<int> _val;

        /// <summary>
        /// A float out of a <see cref="string"/> using invariant culture.
        /// </summary>
        /// <param name="str">a int as a string</param>
        public IntOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using invariant culture.
        /// </summary>
        /// <param name="str">a int as a text</param>
        public IntOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A float out of a <see cref="string"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="str">a int as a string</param>
        /// <param name="culture">culture of the string</param>
        public IntOf(String str, CultureInfo culture) : this(new TextOf(str), culture)
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="str">a int as a string</param>
        /// <param name="culture">culture of the string</param>
        public IntOf(IText text, CultureInfo culture) : this(new ScalarOf<int>(() => Convert.ToInt32(text.AsString(), culture.NumberFormat)))
        { }

        public IntOf(IScalar<int> value)
        {
            _val = value;
        }

        public Int32 Value()
        {
            return _val.Value();
        }
    }
}
