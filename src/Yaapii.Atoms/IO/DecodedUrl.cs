using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Decoded url from a string.
    /// </summary>
    public sealed class DecodedUrl : IScalar<String>
    {
        /// <summary>
        /// source text
        /// </summary>
        private readonly IText _source;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="url">url as string</param>
        public DecodedUrl(String url) : this(url, Encoding.UTF8)
        { }

        /// <summary>
        /// Decoded url from a string.
        /// </summary>
        /// <param name="url">url as string</param>
        /// <param name="enc">encoding of the string</param>
        public DecodedUrl(String url, Encoding enc) : this(new TextOf(url, enc))
        { }

        /// <summary>
        /// Decoded url from a string.
        /// </summary>
        /// <param name="url">url as text</param>
        public DecodedUrl(IText url)
        {
            this._source = url;
        }

        public String Value()
        {
            try
            {
                return WebUtility.UrlDecode(
                    this._source.AsString()
                );
            }
            catch (IOException ex) {
                throw new UncheckedIOException(ex);
            }
            }
        }
    }
