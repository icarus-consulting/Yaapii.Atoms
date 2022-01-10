// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.IO;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Bytes
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
        /// <param name="max">maximum buffer size</param>
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
        public ReaderAsBytes(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new Live<StreamReader>(rdr), enc, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> returned by a <see cref="Func{TResult}"/>as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">function to retrieve the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        private ReaderAsBytes(Func<StreamReader> rdr, Encoding enc, int max = 16 << 10) : this(new Live<StreamReader>(rdr), enc, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> returned by a <see cref="IScalar{TResult}"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public ReaderAsBytes(IScalar<StreamReader> rdr, Encoding enc, int max)
        {
            this._reader = new ScalarOf<StreamReader>(rdr, reader => !reader.BaseStream.CanRead);
            this._encoding = enc;
            this._size = max;
        }

        /// <summary>
        /// Get the content as byte array.
        /// </summary>
        /// <returns>content as a byte array.</returns>
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

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            try
            {
                _reader.Value().Dispose();
            }
            catch (Exception) { }
        }

    }
}
