// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Time
{
    /// <summary>
    /// A date out of a text
    /// </summary>
    public sealed class DateOf : IScalar<DateTime>
    {
        private readonly IScalar<DateTime> date;

        /// <summary>
        /// A date parsed using a using <see cref="CultureInfo.InvariantCulture"/>
        /// </summary>
        /// <param name="date">the date as text</param>
        public DateOf(string date) : this(date, CultureInfo.InvariantCulture, "yyyy-MM-ddTHH:mm:ss.fffffffZ", "yyyy-MM-ddTHH:mm:ss.fffffffzzz", "ddd, dd MMM yyyy HH:mm:ss Z")
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
            new Live<DateTime>(() =>
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
            new Live<DateTime>(
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
            this.date = new ScalarOf<DateTime>(date);
        }

        /// <summary>
        /// The parsed Date
        /// </summary>
        /// <returns></returns>
        public DateTime Value()
        {
            return this.date.Value();
        }
    }
}
