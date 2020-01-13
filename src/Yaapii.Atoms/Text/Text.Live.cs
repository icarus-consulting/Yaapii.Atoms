﻿// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// A Text
    /// </summary>
    public partial class Text
    {
        /// <summary>
        /// A <see cref="IText"/> out of other objects.
        /// </summary>
        public sealed class Live : Text.Envelope
        {
            /// <summary>
            /// A <see cref="IText"/> out of a int.
            /// </summary>
            /// <param name="input">number</param>
            public Live(int input) : this(() => input + "")
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a long.
            /// </summary>
            /// <param name="input">number</param>
            public Live(long input) : this(() => input + "")
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a double
            /// </summary>
            /// <param name="input">a <see cref="double"/></param>
            public Live(double input) : this(
                () => input.ToString(CultureInfo.InvariantCulture)
            )
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a double
            /// </summary>
            /// <param name="input">a <see cref="double"/></param>
            /// <param name="cultureInfo">The </param>
            public Live(double input, CultureInfo cultureInfo) : this(
                () => input.ToString(cultureInfo)
            )
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a float
            /// </summary>
            /// <param name="input">a <see cref="float"/></param>
            public Live(float input) : this(
                () => input.ToString(CultureInfo.InvariantCulture)
            )
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a double
            /// </summary>
            /// <param name="input">a <see cref="float"/></param>
            /// <param name="cultureInfo">The </param>
            public Live(float input, CultureInfo cultureInfo) : this(
                () => input.ToString(cultureInfo)
            )
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="Uri"/>.
            /// </summary>
            /// <param name="uri">a file <see cref="Uri"/></param>
            public Live(Uri uri) : this(new InputOf(uri))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="Uri"/>.
            /// </summary>
            /// <param name="uri">a file <see cref="Uri"/></param>
            /// <param name="encoding">encoding of the data at the uri</param>
            public Live(Uri uri, Encoding encoding) : this(new InputOf(uri), encoding)
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
            /// </summary>
            /// <param name="file"></param>
            public Live(FileInfo file) : this(new InputOf(file))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
            /// </summary>
            /// <param name="file"></param>
            /// <param name="encoding"></param>
            public Live(FileInfo file, Encoding encoding) : this(new InputOf(file), encoding)
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="IInput"/>.
            /// </summary>
            /// <param name="stream">a <see cref="Stream"/></param>
            public Live(Stream stream) : this(new InputOf(stream))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="IInput"/>.
            /// </summary>
            /// <param name="input">a <see cref="IInput"/></param>
            public Live(IInput input) : this(new BytesOf(input))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="IInput"/>.
            /// </summary>
            /// <param name="input">a input</param>
            /// <param name="max">maximum buffer size</param>
            public Live(IInput input, int max) : this(input, max, Encoding.GetEncoding(0))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="IInput"/>.
            /// </summary>
            /// <param name="input">a input</param>
            /// <param name="encoding"><see cref="Encoding"/> of the input</param>
            public Live(IInput input, Encoding encoding) : this(new BytesOf(input), encoding)
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="IInput"/>.
            /// </summary>
            /// <param name="input">a <see cref="IInput"/></param>
            /// <param name="encoding">encoding of the <see cref="IInput"/></param>
            /// <param name="max">maximum buffer size</param>
            public Live(IInput input, int max, Encoding encoding) : this(new BytesOf(input, max), encoding)
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
            /// </summary>
            /// <param name="rdr">a <see cref="StreamReader"/></param>
            public Live(StringReader rdr) : this(new BytesOf(new InputOf(rdr)))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
            /// </summary>
            /// <param name="rdr">a <see cref="StreamReader"/></param>
            /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
            public Live(StringReader rdr, Encoding enc) : this(new BytesOf(rdr), enc)
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
            /// </summary>
            /// <param name="rdr">a <see cref="StreamReader"/></param>
            public Live(StreamReader rdr) : this(new BytesOf(rdr))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
            /// </summary>
            /// <param name="rdr">a <see cref="StreamReader"/></param>
            /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
            public Live(StreamReader rdr, Encoding cset) : this(new BytesOf(rdr, cset))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
            /// </summary>
            /// <param name="rdr">a <see cref="StreamReader"/></param>
            /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
            /// <param name="max">maximum buffer size</param>
            public Live(StreamReader rdr, Encoding cset, int max) : this(new BytesOf(rdr, cset, max))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
            /// </summary>
            /// <param name="builder">a <see cref="StringBuilder"/></param>
            public Live(StringBuilder builder) : this(new BytesOf(builder))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
            /// </summary>
            /// <param name="builder">a <see cref="StringBuilder"/></param>
            /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
            public Live(StringBuilder builder, Encoding enc) : this(new BytesOf(builder, enc))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="char"/> array.
            /// </summary>
            /// <param name="chars">a char array</param>
            public Live(params char[] chars) : this(new BytesOf(chars))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="char"/> array.
            /// </summary>
            /// <param name="chars">a char array</param>
            /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
            public Live(char[] chars, Encoding encoding) : this(new BytesOf(chars, encoding))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="Exception"/>.
            /// </summary>
            /// <param name="error"><see cref="Exception"/> to serialize</param>
            public Live(Exception error) : this(new BytesOf(error))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of a <see cref="byte"/> array.
            /// </summary>
            /// <param name="bytes">a byte array</param>
            public Live(params byte[] bytes) : this(new BytesOf(bytes))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
            /// </summary>
            /// <param name="bytes">A <see cref="IBytes"/> object</param>
            public Live(IBytes bytes) : this(bytes, Encoding.GetEncoding(0))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
            /// </summary>
            /// <param name="bytes">A <see cref="IBytes"/> object</param>
            /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
            public Live(IBytes bytes, Encoding encoding) : this(
                () =>
                {
                    var memoryStream = new MemoryStream(bytes.AsBytes());
                    return new StreamReader(memoryStream, encoding).ReadToEnd(); // removes the BOM from the Byte-Array
            })
            { }

            /// <summary>
            /// A <see cref="IText"/> out of <see cref="string"/>.
            /// </summary>
            /// <param name="input">a string</param>
            public Live(String input) : this(input, Encoding.GetEncoding(0))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of <see cref="string"/>.
            /// </summary>
            /// <param name="input">a string</param>
            /// <param name="encoding"><see cref="Encoding"/> of the string</param>
            public Live(String input, Encoding encoding) : this(
                () => encoding.GetString(encoding.GetBytes(input)))
            { }

            /// <summary>
            /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
            /// </summary>
            /// <param name="fnc">func returning a string</param>
            public Live(IFunc<string> fnc) : this(() => fnc.Invoke())
            { }

            /// <summary>
            /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
            /// </summary>
            /// <param name="scalar">scalar of a string</param>
            public Live(IScalar<String> scalar) : this(() => scalar.Value())
            { }

            /// <summary>
            /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
            /// </summary>
            /// <param name="txt">func building a of a string</param>
            public Live(Func<String> txt) : base(txt, true)
            { }
        }
    }
}