using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> whose contents have been replaced by another text.
    /// </summary>
    public sealed class ReplacedText : IText
    {
        private readonly IText _origin;
        private readonly String _needle;
        private readonly String _replacement;

        /// <summary>
        /// A <see cref="IText"/>  whose contents have been replaced by another text.
        /// </summary>
        /// <param name="text">text to replace contents in</param>
        /// <param name="find">part to replace</param>
        /// <param name="replace">replacement to insert</param>
        public ReplacedText(IText text, String find, String replace)
        {
            this._origin = text;
            this._needle = find;
            this._replacement = replace;
        }

        public String AsString()
        {
            return this._origin.AsString().Replace(this._needle, this._replacement);
        }

        public int CompareTo(IText text)
        {
            return new UncheckedText(this).CompareTo(text);
        }

        public bool Equals(IText text)
        {
            return this.CompareTo(text) == 0;
        }

    }
}
