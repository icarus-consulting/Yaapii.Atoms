using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> without whitespaces or control characters on both sides.
    /// </summary>
    public sealed class TrimmedText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/> without whitespaces or control characters on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            return this._origin.AsString().Trim();
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
