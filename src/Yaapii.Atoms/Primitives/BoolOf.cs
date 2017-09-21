using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A bool out of text objects.
    /// </summary>
    public sealed class BoolOf : IScalar<Boolean>
    {
        private readonly IText _text;

        /// <summary>
        /// <see cref="string"/> as bool
        /// </summary>
        /// <param name="str">source string</param>
        public BoolOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// <see cref="IText"/> as bool
        /// </summary>
        /// <param name="text">source text "true" or "false"</param>
        public BoolOf(IText text)
        {
            this._text = text;
        }

        /// <summary>
        /// Bool value
        /// </summary>
        /// <returns>true or false</returns>
        public Boolean Value()
        {
            try
            {
                return Convert.ToBoolean(this._text.AsString());
            }catch(FormatException ex)
            {
                throw new IOException(ex.Message, ex);
            }
        }
    }
}
