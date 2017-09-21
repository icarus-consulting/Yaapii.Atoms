using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Text as long
    /// </summary>
    public sealed class LongOf : IScalar<long>
    {
        private readonly IScalar<long> _val;

        /// <summary>
        /// A long out of a <see cref="string"/> using invariant culture.
        /// </summary>
        /// <param name="str">a long as a string</param>
        public LongOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A long out of a <see cref="IText"/> using invariant culture.
        /// </summary>
        /// <param name="str">a long as a text</param>
        public LongOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A long out of a <see cref="string"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="str">a long as a string</param>
        /// <param name="culture">culture of the string</param>
        public LongOf(String str, CultureInfo culture) : this(new TextOf(str), culture)
        { }

        /// <summary>
        /// A long out of a <see cref="string"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="text">a string as a text</param>
        /// <param name="culture">culture of the text</param>
        public LongOf(IText text, CultureInfo culture) : this(new ScalarOf<long>(() => Convert.ToInt64(text.AsString(), culture.NumberFormat)))
        { }

        /// <summary>
        /// A long out of encapsulating <see cref="IScalar{T}"/>
        /// </summary>
        /// <param name="value">a scalar of the number</param>
        public LongOf(IScalar<long> value)
        {
            _val = value;
        }

        public long Value()
        {
            return _val.Value();
        }
    }
}
