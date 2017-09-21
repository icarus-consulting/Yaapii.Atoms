using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
    /// </summary>
    public sealed class Base64DecodedText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        public Base64DecodedText(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        public Base64DecodedText(IText text)
        {
            this._origin = text;
        }

        public String AsString()
        {
            return new TextOf(
                Convert.FromBase64String(this._origin.AsString())).AsString();
        }

        public bool Equals(IText text)
        {
            return new UncheckedText(this).Equals(text);
        }
    }
}
