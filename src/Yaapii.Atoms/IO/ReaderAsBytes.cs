using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A <see cref="StringReader"/> as <see cref="IBytes"/>
    /// </summary>
    public sealed class ReaderAsBytes : IBytes, IDisposable
    {
        /// <summary>
        /// a reader
        /// </summary>
        private readonly IScalar<StreamReader> _reader;

        /// <summary>
        /// encoding of the reader
        /// </summary>
        private readonly Encoding _encoding;

        /// <summary>
        /// maximum buffer size
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// A <see cref="StringReader"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        public ReaderAsBytes(StringReader rdr, int max = 16 << 10) : this(() =>
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(rdr.ReadToEnd());
                writer.Flush();
                stream.Position = 0;
                return new StreamReader(stream);
            }, 
            Encoding.UTF8, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        public ReaderAsBytes(StreamReader rdr) : this(rdr, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public ReaderAsBytes(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new ScalarOf<StreamReader>(rdr), enc, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> returned by a <see cref="Func{TResult}"/>as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">function to retrieve the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        private ReaderAsBytes(Func<StreamReader> rdr, Encoding enc, int max = 16 << 10) : this(new ScalarOf<StreamReader>(rdr), enc, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> returned by a <see cref="IScalar{TResult}"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public ReaderAsBytes(IScalar<StreamReader> rdr, Encoding enc, int max)
        {
            this._reader = rdr;
            this._encoding = enc;
            this._size = max;
        }

        public byte[] AsBytes()
        {
            var rdr = this._reader.Value();
            var buffer = new char[this._size];
            var builder = new StringBuilder(this._size);
            int pos = 0;
            while (rdr.Peek() > -1)
            {
                pos = rdr.Read(buffer, 0, buffer.Length);
                builder.Append(buffer, 0, pos);
            }
            rdr.BaseStream.Position = 0;
            rdr.DiscardBufferedData();
            return this._encoding.GetBytes(builder.ToString());
        }

        public void Dispose()
        {
            try
            {
                ((IDisposable)this._reader).Dispose();
            }
            catch (Exception) { }
        }

    }
}
