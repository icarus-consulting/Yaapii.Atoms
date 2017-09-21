using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> as lowercase.
    /// </summary>
    public sealed class LowerText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/>  as lowercase.
        /// </summary>
        /// <param name="text">text to lower</param>
        public LowerText(IText text)
        {
            _origin = text;
        }

        public String AsString()
        {
            return this._origin.AsString().ToLower();
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
