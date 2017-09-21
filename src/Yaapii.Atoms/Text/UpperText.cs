using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> as uppercase.
    /// </summary>
    public sealed class UpperText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/> as uppercase.
        /// </summary>
        /// <param name="text">text to uppercase</param>
        public UpperText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            return this._origin.AsString().ToUpperInvariant();
        }

        public int CompareTo(IText text)
        {
            return new UncheckedText(this).CompareTo(text);
        }

        public bool Equals(Atoms.IText other)
        {
            return other.AsString().Equals(this.AsString());
        }
    }
}
