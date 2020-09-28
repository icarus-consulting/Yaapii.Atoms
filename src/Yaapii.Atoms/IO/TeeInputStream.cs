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
using System.IO;

#pragma warning disable MaxPublicMethodCount
namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Readable <see cref="Stream"/> that copies input to <see cref="IOutput"/> while reading.
    /// </summary>
    public sealed class TeeInputStream : Stream
    {
        /// <summary>
        /// input
        /// </summary>
        private readonly Stream input;

        /// <summary>
        /// destination
        /// </summary>
        private readonly Stream output;

        /// <summary>
        /// Readable <see cref="Stream"/> that copies input to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="src">the source</param>
        /// <param name="tgt">the destination</param>
        public TeeInputStream(Stream src, Stream tgt) : base()
        {
            this.input = src;
            this.output = tgt;
        }

#pragma warning disable CS1591
        public int Read()
        {
            var data = (Byte)this.input.ReadByte();
            if (data >= 0)
            {
                this.output.WriteByte(data);
            }
            return data;
        }

        public int Read(byte[] buf)
        {
            return this.Read(buf, 0, buf.Length);
        }

        public override int Read(byte[] buf, int offset, int len)
        {
            int max = this.input.Read(buf, offset, len);
            if (max > 0)
            {
                this.output.Write(buf, offset, max);
            }
            return max;
        }

        public long Skip(long num)
        {
            return this.input.Seek(num, SeekOrigin.Current);
        }

        public override bool CanRead => input.CanRead;

        public override bool CanSeek => input.CanSeek;

        public override bool CanWrite => input.CanWrite;

        public override long Length => input.Length;

        public override long Position
        {
            get
            {
                return input.Position;
            }
            set
            {
                input.Position = value;
            }
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                this.input.Flush();
                input.Dispose();
            }
            catch (Exception) { }
            try
            {
                this.output.Flush();
                this.output.Dispose();
            }
            catch (Exception) { }

            base.Dispose(disposing);
        }

        public override void Flush()
        {
            input.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return input.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            input.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
#pragma warning restore CS1591