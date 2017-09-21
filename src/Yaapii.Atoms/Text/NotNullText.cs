using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> that can't accept null.
    /// </summary>
    public sealed class NotNullText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/>  that can't accept null.
        /// </summary>
        /// <param name="text"></param>
        public NotNullText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            if (this._origin == null)
            {
                throw new IOException("invalid text (null)");
            }
            return this._origin.AsString();
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
