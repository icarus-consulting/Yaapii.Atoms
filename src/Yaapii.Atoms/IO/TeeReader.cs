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
using System.Threading.Tasks;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A  <see cref="StreamReader"/> which copies to a <see cref="StreamWriter"/> while reading.
    /// </summary>
    public sealed class TeeReader : StreamReader, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly StreamReader _source;

        /// <summary>
        /// the destination
        /// </summary>
        private readonly StreamWriter _destination;

        /// <summary>
        /// A  <see cref="StreamReader"/> which copies to a <see cref="StreamWriter"/> while reading.
        /// </summary>
        /// <param name="reader">the reader</param>
        /// <param name="writer">the copy target</param>
        public TeeReader(StreamReader reader, StreamWriter writer) : base(new DeadStream())
        {
            this._source = reader;
            this._destination = writer;
        }

#pragma warning disable CS1591
        public override int Read()
        {
            int c = this._source.Read();
            if (c > -1)
            {
                this._destination.Write((char)c);
            }
            return c;
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            int done = this._source.ReadBlock(buffer, index, count);
            if (done >= 0)
            {
                this._destination.Write(buffer);
            }
            return done;
        }

        public override async Task<string> ReadLineAsync()
        {
            var str = await this._source.ReadLineAsync();
            this._destination.WriteLine(str);
            return str;
        }

        public override async Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            var done = await this._source.ReadBlockAsync(buffer, index, count);
            if (done > 0)
            {
                await this._destination.WriteAsync(buffer);
            }
            return done;
        }

        public override async Task<string> ReadToEndAsync()
        {
            var s = await this._source.ReadToEndAsync();
            await this._destination.WriteAsync(s);
            return s;
        }

        public override int Peek()
        {
            return this._source.Peek();
        }

        public override string ReadToEnd()
        {
            string s = this._source.ReadToEnd();
            this._destination.Write(s);
            return s;
        }

        public override string ReadLine()
        {
            var s = base.ReadLine();
            this._destination.WriteLine(s);
            return s;
        }

        public override async Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            int done = await this._source.ReadAsync(buffer, index, count);
            if (done >= 0)
            {
                await this._destination.WriteAsync(buffer);
            }
            return done;
        }

        public override int Read(char[] cbuf, int offset, int length)
        {
            int done = this._source.Read(cbuf, 0, length);
            if (done >= 0)
            {
                this._destination.Write(cbuf);
            }
            return done;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                _source.Dispose();
            }
            catch (Exception) { }

            try
            {
                _destination.Flush();
                _destination.Dispose();
            }
            catch (Exception) { }
            base.Dispose(disposing);
        }

        public override Encoding CurrentEncoding => this._source.CurrentEncoding;
        public override Stream BaseStream => this._source.BaseStream;

#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore CS1591
    }
}
