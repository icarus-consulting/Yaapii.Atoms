using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> which has been reversed.
    /// </summary>
    public sealed class ReversedText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/> which has been reversed.
        /// </summary>
        /// <param name="text">text to reverse</param>
        public ReversedText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            char[] chararray = _origin.AsString().ToCharArray();
            Array.Reverse(chararray);
            string reverseTxt = "";
            for (int i = 0; i <= chararray.Length - 1; i++)
            {
                reverseTxt += chararray.GetValue(i);
            }
            return reverseTxt;
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
