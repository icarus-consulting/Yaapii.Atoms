using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 encoded <see cref="IText"/>
    /// </summary>
    public sealed class TextBase64 : IText
    {
        private readonly IText origin;

        /// <summary>
        /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to encode</param>
        public TextBase64(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to encode</param>
        public TextBase64(IText text)
        {
            this.origin =
                new TextOf(
                    new BytesBase64(
                        new BytesOf(text)
                    )
                );
        }

        public string AsString()
        {
            return origin.AsString();
                
        }

        public bool Equals(IText other)
        {
            return origin.Equals(other);
        }
    }
}
