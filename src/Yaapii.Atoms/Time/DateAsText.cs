using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Time
{
    /// <summary>
    /// A date formatted as a text
    /// </summary>
    public sealed class DateAsText : IText
    {
        private readonly IScalar<string> _formatted;

        /// <summary>
        /// Current Datetime as ISO
        /// </summary>
        public DateAsText() : this(new ScalarOf<DateTime>(() => DateTime.Now))
        { }

        /// <summary>
        /// A date formatted as ISO
        /// </summary>
        /// <param name="date"></param>
        public DateAsText(DateTime date) : this(new ScalarOf<DateTime>(date), "o")
        { }

        /// <summary>
        /// A date formatted as ISO
        /// </summary>
        /// <param name="date"></param>
        public DateAsText(IScalar<DateTime> date) : this(date, "o")
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(DateTime date, string format) : this(new ScalarOf<DateTime>(date), new TextOf(format), CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(IScalar<DateTime> date, string format) : this(date, new TextOf(format), CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(DateTime date, IText format) : this(new ScalarOf<DateTime>(date), format, CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(IScalar<DateTime> date, IText format) : this(date, format, CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted as <see cref="IText"/> by using a <see cref="IFormatProvider"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        /// <param name="provider">a format provider</param>
        public DateAsText(IScalar<DateTime> date, IText format, IFormatProvider provider)
        {
            this._formatted =
                new ScalarOf<string>(
                    () => date.Value().ToString(format.AsString(), provider));
        }

        /// <summary>
        /// The formatted <see cref="DateTime"/>
        /// </summary>
        /// <returns></returns>
        public string AsString()
        {
            return _formatted.Value();
        }

        /// <summary>
        /// Equal to another <see cref="IText"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IText other)
        {
            return _formatted.Value().Equals(other.AsString());
        }
    }
}
