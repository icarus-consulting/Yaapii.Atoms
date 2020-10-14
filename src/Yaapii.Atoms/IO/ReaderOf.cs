// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
#pragma warning disable CS1591
namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A <see cref="StreamReader"/> out of other objects.
    /// </summary>
    public sealed class ReaderOf : StreamReader, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IScalar<StreamReader> _source;

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">some chars</param>
        public ReaderOf(params char[] chars) : this(new InputOf(chars))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">some chars</param>
        /// <param name="enc">encoding of the chars</param>
        public ReaderOf(char[] chars, Encoding enc) : this(new InputOf(chars, enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">some bytes</param>
        public ReaderOf(byte[] bytes) : this(new InputOf(bytes))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">some bytes</param>
        /// <param name="enc">encoding of the bytes</param>
        public ReaderOf(byte[] bytes, Encoding enc) : this(new InputOf(bytes), enc)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Url"/> array.
        /// </summary>
        /// <param name="url">a www url starting with http:// or https://</param>
        public ReaderOf(Url url) : this(new InputOf(url))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="string"/>.
        /// </summary>
        /// <param name="content">a string</param>
        public ReaderOf(string content) : this(new InputOf(content))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a file <see cref="Uri"/> array.
        /// </summary>
        /// <param name="uri">a file Uri, create with Path.GetFullPath(absOrRelativePath) or prefix with file:/// </param>
        public ReaderOf(Uri uri) : this(new InputOf(uri))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">a <see cref="IBytes"/> object</param>
        public ReaderOf(IBytes bytes) : this(new InputOf(bytes))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        public ReaderOf(IText text) : this(new InputOf(text))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IText"/> object.
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        /// <param name="enc">encoding of the text</param>
        public ReaderOf(IText text, Encoding enc) : this(new InputOf(text, enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IInput"/> object.
        /// </summary>
        /// <param name="input">a input</param>
        public ReaderOf(IInput input) : this(input, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="IInput"/> object.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="enc">encoding of the input</param>
        public ReaderOf(IInput input, Encoding enc) : this(
            () => new StreamReader(input.Stream(), enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">a stream</param>
        public ReaderOf(Stream stream) : this(stream, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Stream"/> object.
        /// </summary>
        /// <param name="stream">a stream</param>
        /// <param name="enc">encoding of the stream</param>
        public ReaderOf(Stream stream, Encoding enc) : this(new StreamReader(stream, enc))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a reader</param>
        private ReaderOf(StreamReader rdr) : this(() => rdr)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> out of a <see cref="Func{TResult}"/> that returns a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="src">func retrieving a reader</param>
        private ReaderOf(Func<StreamReader> src) : this(new Live<StreamReader>(src))
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> encapsulated in a <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="src">scalar of a reader</param>
        private ReaderOf(IScalar<StreamReader> src) : base(new DeadInput().Stream())
        {
            this._source = new ScalarOf<StreamReader>(src, stream => !stream.BaseStream.CanRead);
        }

        public override int Read()
        {
            return this._source.Value().Read();
        }

        public override Task<string> ReadToEndAsync()
        {
            return this._source.Value().ReadToEndAsync();
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return this._source.Value().ReadBlock(buffer, index, count);
        }

        public override Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            return this._source.Value().ReadAsync(buffer, index, count);
        }

        public override int Read(char[] cbuf, int off, int len)
        {
            return this._source.Value().Read(cbuf, off, len);
        }

        public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            return this._source.Value().ReadBlockAsync(buffer, index, count);
        }

        public override string ReadLine()
        {
            return this._source.Value().ReadLine();
        }

        public override Task<string> ReadLineAsync()
        {
            return this._source.Value().ReadLineAsync();
        }

        public override string ReadToEnd()
        {
            return this._source.Value().ReadToEnd();
        }

        public override int Peek()
        {
            return this._source.Value().Peek();
        }

        protected override void Dispose(bool disposing)
        {
            _source.Value().Dispose();
            base.Dispose(disposing);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum