// MIT License
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxVariablesCount // Four fields maximum
#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
    /// </summary>
    internal class WriterAsOutputStream : Stream, IDisposable
    {
        /// <summary>
        /// the writer
        /// </summary>
        private readonly StreamWriter _writer;

        /// <summary>
        /// encoding of the writer
        /// </summary>
        private readonly IScalar<Decoder> _decoder;

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        internal WriterAsOutputStream(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="encoding">encoding of the writer</param>
        internal WriterAsOutputStream(StreamWriter wtr, string encoding) : this(wtr, Encoding.GetEncoding(encoding))
        { }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="enc">encoding of the writer</param>
        internal WriterAsOutputStream(StreamWriter wtr, Encoding enc) : this(
                wtr,
                new Live<Decoder>(() =>
                {
                    var ddr = enc.GetDecoder();
                    ddr.Fallback = DecoderFallback.ExceptionFallback;
                    return ddr;
                })
            )
        { }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="ddr">charset decoder for the writer</param>
        internal WriterAsOutputStream(StreamWriter wtr, IScalar<Decoder> ddr) : base()
        {
            this._writer = wtr;
            this._decoder = ddr;
        }

        /// <summary>
        /// <see cref="StreamWriter"/> as a writable <see cref="Stream"/>.
        /// </summary>
        /// <param name="data">some data</param>
        public void Write(int data)
        {
            this.Write(new byte[] { (byte)data }, 0, 1);
        }

        public void Write(byte[] buffer)
        {
            this.Write(buffer, 0, buffer.Length);
        }

        public override void Write(byte[] buffer, int offset, int length)
        {
            int left = length;
            int start = offset;
            while (left > 0)
            {
                int taken = this.Next(buffer, start, left);
                start += taken;
                left -= taken;
            }
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int length, CancellationToken cancellationToken)
        {
            int left = length;
            int start = offset;
            while (left > 0)
            {
                int taken = await this.NextAsync(buffer, start, left);
                start += taken;
                left -= taken;
            }
        }

        private int Next(byte[] buffer, int offset, int length)
        {
            var charCount = this._decoder.Value().GetCharCount(buffer, 0, length);
            char[] chars = new char[charCount];
            this._decoder.Value().GetChars(buffer, 0, charCount, chars, 0);

            long max = Math.Min((long)length, charCount);


            this._writer.Write(chars);
            this._writer.Flush();

            return (int)Math.Min((long)length, charCount);
        }

        private async Task<int> NextAsync(byte[] buffer, int offset, int length)
        {
            var charCount = this._decoder.Value().GetCharCount(buffer, 0, length);
            char[] chars = new char[charCount];
            this._decoder.Value().GetChars(buffer, 0, charCount, chars, 0);

            long max = Math.Min((long)length, charCount);


            await this._writer.WriteAsync(chars);
            await this._writer.FlushAsync();

            return (int)Math.Min((long)length, charCount);
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length
        {
            get => throw new NotImplementedException();
        }

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public override void Flush()
        {
            this._writer.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }


        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }


        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                this._writer.Flush();
            }
            catch (Exception) { }

            try
            {
                this._writer.Dispose();
            }
            catch (Exception) { }
            base.Dispose(disposing);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore Immutability // Fields are readonly or constant
#pragma warning restore MaxVariablesCount // Four fields maximum