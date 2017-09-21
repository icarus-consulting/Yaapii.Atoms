using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Bytes as input.
    /// </summary>
    public sealed class BytesAsInput : IInput
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IBytes _source;

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="text">a text</param>
        public BytesAsInput(IText text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="text">a string</param>
        public BytesAsInput(String text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="bytes">byte array</param>
        public BytesAsInput(byte[] bytes) : this(new BytesOf(bytes))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public BytesAsInput(IBytes bytes)
        {
            this._source = bytes;
        }

        public Stream Stream()
        {
            return new MemoryStream(this._source.AsBytes());
        }
    }
}
