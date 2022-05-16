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
using System.Globalization;
using System.IO;
using System.Text;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;

#pragma warning disable MaxClassLength // Class length max
namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> out of other objects.
    /// </summary>
    public sealed class LiveText : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> out of a int.
        /// </summary>
        public LiveText(int input) : this(() => input + "")
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a long.
        /// </summary>
        public LiveText(long input) : this(() => input + "")
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        public LiveText(double input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        public LiveText(double input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a float
        /// </summary>
        public LiveText(float input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        public LiveText(float input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        public LiveText(Uri uri) : this(new InputOf(uri))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        public LiveText(Uri uri, Encoding encoding) : this(new InputOf(uri), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        public LiveText(FileInfo file) : this(new InputOf(file))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        public LiveText(FileInfo file, Encoding encoding) : this(new InputOf(file), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        public LiveText(Stream stream) : this(new InputOf(stream))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        public LiveText(IInput input) : this(new BytesOf(input))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        public LiveText(IInput input, int max) : this(input, max, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        public LiveText(IInput input, Encoding encoding) : this(new BytesOf(input), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        public LiveText(IInput input, int max, Encoding encoding) : this(new BytesOf(input, max), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        public LiveText(StringReader reader) : this(new BytesOf(new InputOf(reader)))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        public LiveText(StringReader reader, Encoding encoding) : this(new BytesOf(reader), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        public LiveText(StreamReader reader) : this(new BytesOf(reader))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        public LiveText(StreamReader reader, Encoding encoding) : this(new BytesOf(reader, encoding))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        public LiveText(StreamReader reader, Encoding encoding, int max) : this(new BytesOf(reader, encoding, max))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        public LiveText(StringBuilder builder) : this(new BytesOf(builder))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        public LiveText(StringBuilder builder, Encoding encoding) : this(new BytesOf(builder, encoding))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        public LiveText(params char[] chars) : this(new BytesOf(chars))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        public LiveText(char[] chars, Encoding encoding) : this(new BytesOf(chars, encoding))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Exception"/>.
        /// </summary>
        public LiveText(Exception error) : this(new BytesOf(error))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="byte"/> array.
        /// </summary>
        public LiveText(params byte[] bytes) : this(new BytesOf(bytes))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        public LiveText(IBytes bytes) : this(bytes, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        public LiveText(IBytes bytes, Encoding encoding) : this(
            () =>
            {
                var memoryStream = new MemoryStream(bytes.AsBytes());
                return new StreamReader(memoryStream, encoding).ReadToEnd(); // removes the BOM from the Byte-Array
            })
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        public LiveText(String input) : this(input, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        public LiveText(String input, Encoding encoding) : this(
            () => encoding.GetString(encoding.GetBytes(input)))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
        /// </summary>
        public LiveText(IFunc<string> function) : this(() => function.Invoke())
        { }

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        public LiveText(IScalar<String> scalar) : this(() => scalar.Value())
        { }

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        public LiveText(Func<String> text) : base(text, true)
        { }
    }
}
