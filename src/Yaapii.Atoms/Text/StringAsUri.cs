using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A text as a <see cref="Uri"/>
    /// </summary>
    public sealed class StringAsUri : IScalar<Uri>
    {
        private readonly IText _source;

        /// <summary>
        /// A <see cref="string"/> as a <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">uri as a string</param>
        public StringAsUri(String url) : this(new TextOf(url))
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="Uri"/>
        /// </summary>
        /// <param name="url">uri as text</param>
        public StringAsUri(IText url)
        {
            this._source = url;
        }

        public Uri Value()
        {
            return new UriBuilder(this._source.AsString()).Uri;
        }
    }
}
