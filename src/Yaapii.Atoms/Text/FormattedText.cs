using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// Use C# formatting syntax: new FormattedText("{0} is {1}", "OOP", "great").AsString() will be "OOP is great"
    /// </summary>
    public sealed class FormattedText : IText
    {
        private readonly IText _pattern;
        private readonly object[] _args;
        private readonly CultureInfo _locale;

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public FormattedText(String ptn, params object[] arguments) : this(
            new TextOf(ptn), CultureInfo.InvariantCulture, arguments)
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public FormattedText(IText ptn, params object[] arguments) : this(
            ptn, CultureInfo.InvariantCulture, arguments)
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public FormattedText(String ptn, CultureInfo locale, params object[] arguments) : this(
            new TextOf(ptn), locale, arguments)
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public FormattedText(
            IText ptn,
            CultureInfo locale,
            object[] arguments
        )
        {
            this._pattern = ptn;
            this._locale = locale;
            this._args = arguments;
        }

        public String AsString()
        {
            return String.Format(this._locale, _pattern.AsString(), _args);
        }

        public int CompareTo(IText text)
        {
            return new UncheckedText(this).CompareTo(text);
        }

        public bool Equals(IText other)
        {
            return new UncheckedText(this).Equals(other);
        }
    }
}
