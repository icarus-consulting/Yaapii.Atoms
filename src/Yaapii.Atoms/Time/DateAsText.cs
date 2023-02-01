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
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Time
{
    /// <summary>
    /// A date formatted as a text
    /// </summary>
    public sealed class DateAsText : IText
    {
        private readonly ScalarOf<string> formatted;

        /// <summary>
        /// Current Datetime as ISO
        /// </summary>
        public DateAsText() : this(new Live<DateTime>(() => DateTime.Now))
        { }

        /// <summary>
        /// A date formatted as ISO
        /// </summary>
        /// <param name="date"></param>
        public DateAsText(DateTime date) : this(new Live<DateTime>(date), "o")
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
        public DateAsText(DateTime date, string format) : this(new Live<DateTime>(date), new TextOf(format), CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(IScalar<DateTime> date, string format) : this(date, new LiveText(format), CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(DateTime date, IText format) : this(new Live<DateTime>(date), format, CultureInfo.CurrentCulture)
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
            this.formatted =
                new ScalarOf<string>(
                    () => date.Value().ToString(format.AsString(), provider)
                );
        }

        /// <summary>
        /// The formatted <see cref="DateTime"/>
        /// </summary>
        /// <returns></returns>
        public string AsString()
        {
            return formatted.Value();
        }

        /// <summary>
        /// Equal to another <see cref="IText"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IText other)
        {
            return formatted.Value().Equals(other.AsString());
        }
    }
}
