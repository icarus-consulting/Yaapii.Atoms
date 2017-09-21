using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> trimmed (removed whitespaces) on the left side.
    /// </summary>
    public sealed class TrimmedLeftText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/> trimmed (removed whitespaces) on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedLeftText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            String text = this._origin.AsString();
            return text.TrimStart();
        }

        public bool Equals(IText other)
        {
            return new UncheckedText(this).CompareTo(other as IText) == 0;
        }
    }
}
