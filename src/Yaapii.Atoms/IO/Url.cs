using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// www <see cref="Uri"/> from a <see cref="string"/> which checks for format.
    /// </summary>
    public sealed class Url : IScalar<Uri>
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly string _source;

        /// <summary>
        /// Url from a <see cref="string"/> which checks for format.
        /// </summary>
        /// <param name="src">url prefixed with http:// or https://</param>
        public Url(string src)
        {
            _source = src;
        }

        public Uri Value()
        {
            if(!_source.StartsWith("http://") && !_source.StartsWith("https://"))
            {
                throw new ArgumentException("url must start with http or https");
            }
            return new Uri(_source);
        }
    }
}
