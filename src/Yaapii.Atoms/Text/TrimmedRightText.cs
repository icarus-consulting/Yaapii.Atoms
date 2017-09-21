using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> trimmed (removed whitespaces) on the right side.
    /// </summary>
    public sealed class TrimmedRightText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/> trimmed (removed whitespaces) on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedRightText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            String text = this._origin.AsString();
            return text.TrimEnd();
        }

        public bool Equals(IText other)
        {
            return new UncheckedText(this).CompareTo(other as IText) == 0;
        }
    }
}
