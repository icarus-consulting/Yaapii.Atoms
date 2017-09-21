using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Normalized A <see cref="IText"/> (whitespaces replaced with one single space)
    /// </summary>
    public sealed class NormalizedText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// Normalized A <see cref="IText"/>  (whitespaces replaced with one single space)
        /// </summary>
        /// <param name="text">text to normalize</param>
        public NormalizedText(String text) : this(new TextOf(text))
        { }

        /// <summary>
        /// Normalized A <see cref="IText"/>  (whitespaces replaced with one single space)
        /// </summary>
        /// <param name="text">text to normalize</param>
        public NormalizedText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            return Regex.Replace(new TrimmedText(this._origin).AsString(), "\\s+", " ");
        }

        public int CompareTo(IText text)
        {
            return new UncheckedText(this).CompareTo(text);
        }

        public bool Equals(IText other)
        {
            return CompareTo(other) == 0;
        }
    }
}
