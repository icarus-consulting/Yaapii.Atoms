// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxClassLength // Class length max
namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Bytes out of other objects.
    /// </summary>
    public sealed class BytesOf : IBytes
    {
        /// <summary>
        /// original
        /// </summary>
        private readonly IScalar<byte[]> _origin;

        /// <summary>
        /// Bytes out of a input.
        /// </summary>
        /// <param name="input">the input</param>
        public BytesOf(IInput input) : this(new InputAsBytes(input))
        { }

        /// <summary>
        /// Bytes out of a input.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="max">max buffer size</param>
        public BytesOf(IInput input, int max) : this(new InputAsBytes(input, max))
        { }

        /// <summary>
        /// Bytes out of a StringBuilder.
        /// </summary>
        /// <param name="builder">stringbuilder</param>
        public BytesOf(StringBuilder builder) : this(builder, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a StringBuilder.
        /// </summary>
        /// <param name="builder">stringbuilder</param>
        /// <param name="enc">encoding of stringbuilder</param>
        public BytesOf(StringBuilder builder, Encoding enc) : this(
            () => enc.GetBytes(builder.ToString()))
        { }

        /// <summary>
        /// Bytes out of a StringReader.
        /// </summary>
        /// <param name="rdr">stringreader</param>
        public BytesOf(StringReader rdr) : this(new ReaderAsBytes(rdr))
        { }

        /// <summary>
        /// Bytes out of a StreamReader.
        /// </summary>
        /// <param name="rdr">streamreader</param>
        public BytesOf(StreamReader rdr) : this(new ReaderAsBytes(rdr))
        { }

        /// <summary>
        /// Bytes out of a StreamReader.
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">max buffer size of the reader</param>
        public BytesOf(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new ReaderAsBytes(rdr, enc, max))
        { }

        /// <summary>
        /// Bytes out of a list of chars.
        /// </summary>
        /// <param name="chars">enumerable of bytes</param>
        public BytesOf(IEnumerable<char> chars) : this(chars, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a list of chars.
        /// </summary>
        /// <param name="chars">enumerable of chars</param>
        /// <param name="enc">encoding of chars</param>
        public BytesOf(IEnumerable<char> chars, Encoding enc) : this(
                () => chars.Select(c => (Byte)c).ToArray())
        { }

        /// <summary>
        /// Bytes out of a char array.
        /// </summary>
        /// <param name="chars">array of chars</param>
        public BytesOf(params char[] chars) : this(chars, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a char array.
        /// </summary>
        /// <param name="chars">an array of chars</param>
        /// <param name="encoding">encoding of chars</param>
        public BytesOf(char[] chars, Encoding encoding) : this(new String(chars), encoding)
        { }

        /// <summary>
        /// Bytes out of a string.
        /// </summary>
        /// <param name="source">a string</param>
        public BytesOf(String source) : this(source, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a string.
        /// </summary>
        /// <param name="source">a string</param>
        /// <param name="encoding">encoding of the string</param>
        public BytesOf(String source, Encoding encoding) : this(
            () => encoding.GetBytes(source))
        { }

        /// <summary>
        /// Bytes out of Text.
        /// </summary>
        /// <param name="text">a text</param>
        public BytesOf(IText text) : this(text, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of Text.
        /// </summary>
        /// <param name="text">a text</param>
        /// <param name="encoding">encoding of the string</param>
        public BytesOf(IText text, Encoding encoding) : this(
            () => encoding.GetBytes(text.AsString()))
        { }

        /// <summary>
        /// Bytes out of Exception object.
        /// </summary>
        /// <param name="error">exception to serialize</param>
        public BytesOf(Exception error) : this(
                () => Encoding.UTF8.GetBytes(error.ToString()))
        { }

        /// <summary>
        /// Bytes out of IBytes object.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public BytesOf(IBytes bytes) : this(
                () => bytes.AsBytes())
        { }

        /// <summary>
        /// Bytes out of function which returns a byte array.
        /// </summary>
        /// <param name="bytes">byte aray</param>
        public BytesOf(Func<Byte[]> bytes) : this(new ScalarOf<Byte[]>(bytes))
        { }

        /// <summary>
        /// Bytes out of byte array.
        /// </summary>
        /// <param name="bytes">byte aray</param>
        public BytesOf(params Byte[] bytes) : this(new ScalarOf<Byte[]>(bytes))
        { }

        /// <summary>
        /// Bytes out of other objects.
        /// </summary>
        /// <param name="bytes">scalar of bytes</param>
        private BytesOf(IScalar<Byte[]> bytes)
        {
            this._origin = bytes;
        }

        /// <summary>
        /// Get the content as byte array.
        /// </summary>
        /// <returns>content as byte array</returns>
        public byte[] AsBytes()
        {
            return this._origin.Value();
        }
    }
}
