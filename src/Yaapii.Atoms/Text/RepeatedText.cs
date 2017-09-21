using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> repeated multiple times.
    /// </summary>
    public sealed class RepeatedText : IText
    {
        private readonly IText _origin;
        private readonly int _count;

        /// <summary>
        /// A <see cref="IText"/>  repeated multiple times.
        /// </summary>
        /// <param name="text">text to repeat</param>
        /// <param name="count">how often to repeat</param>
        public RepeatedText(String text, int count) : this(new TextOf(text), count)
        { }

        /// <summary>
        /// A <see cref="IText"/>  repeated multiple times.
        /// </summary>
        /// <param name="text">text to repeat</param>
        /// <param name="count">how often to repeat</param>
        public RepeatedText(IText text, int count)
        {
            this._origin = text;
            this._count = count;
        }

        public String AsString()
        {
            StringBuilder output = new StringBuilder();
            for (int cnt = 0; cnt < this._count; ++cnt)
            {
                output.Append(this._origin.AsString());
            }
            return output.ToString();
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
