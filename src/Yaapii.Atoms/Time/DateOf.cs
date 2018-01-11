using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Time
{
    /// <summary>
    /// A date out of a text
    /// </summary>
    public sealed class DateOf : IScalar<DateTime>
    {
        private readonly IScalar<DateTime> _date;

        /// <summary>
        /// A date parsed using a using <see cref="CultureInfo.InvariantCulture"/>
        /// </summary>
        /// <param name="date">the date as text</param>
        public DateOf(string date) : this(date, CultureInfo.InvariantCulture, "yyyy-MM-ddTHH:mm:ss.fffffffZ", "yyyy-MM-ddTHH:mm:ss.fffffffzzz")
        { }

        /// <summary>
        /// A date parsed using a pattern and a formatprovider
        /// </summary>
        /// <param name="date">the date as text</param>
        /// <param name="patterns"></param>
        public DateOf(string date, params string[] patterns) : this(date, CultureInfo.InvariantCulture, patterns)
        { }

        /// <summary>
        /// A date parsed using a pattern and a formatprovider
        /// </summary>
        /// <param name="date">the date as text</param>
        /// <param name="patterns"></param>
        /// <param name="provider"></param>
        public DateOf(string date, IFormatProvider provider, params string[] patterns) : this(
            new ScalarOf<DateTime>(() =>
            {
                return DateTime.ParseExact(date, patterns, provider, DateTimeStyles.AssumeUniversal);
            }))
        { }

        /// <summary>
        /// A parsed <see cref="DateTime"/>
        /// </summary>
        /// <param name="date">the date as text</param>
        public DateOf(IText date) : this(date, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A date parsed using a formatprovider
        /// </summary>
        /// <param name="date">the date as text</param>
        /// <param name="dateFormat">format provider</param>
        public DateOf(IText date, IFormatProvider dateFormat) : this(
            new ScalarOf<DateTime>(
                () =>
                    DateTime.Parse(
                        date.AsString(),
                        dateFormat
                    )
                )
            )
        { }

        /// <summary>
        /// A date
        /// </summary>
        /// <param name="date"></param>
        public DateOf(IScalar<DateTime> date)
        {
            this._date = date;
        }

        /// <summary>
        /// The parsed Date
        /// </summary>
        /// <returns></returns>
        public DateTime Value()
        {
            return this._date.Value();
        }
    }
}
