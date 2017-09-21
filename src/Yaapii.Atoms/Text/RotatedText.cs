using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> whose characters have been rotated.
    /// </summary>
    public sealed class RotatedText : IText
    {
        private readonly IText _origin;
        private readonly int _move;

        /// <summary>
        /// A <see cref="IText"/> whose characters have been rotated.
        /// </summary>
        /// <param name="text">text to rotate</param>
        /// <param name="shift">direction and amount of chars to rotate (minus means rotate left, plus means rotate right)</param>
        public RotatedText(IText text, int shift)
        {
            this._origin = text;
            this._move = shift;
        }

        public String AsString()
        {
            var text = this._origin.AsString();
            int length = text.Length;
            if (length != 0 && this._move != 0 && this._move % length != 0)
            {
                var builder = new StringBuilder(length);
                int offset = -(this._move % length);
                if (offset < 0)
                {
                    offset = text.Length + offset;
                }
                text = builder.Append(
                    text.Substring(offset)
                ).Append(
                    text.Substring(0, offset)
                ).ToString();
            }
            return text;
        }

        public bool Equals(IText text)
        {
            return new UncheckedText(this).CompareTo(text) == 0;
        }

    }
}
