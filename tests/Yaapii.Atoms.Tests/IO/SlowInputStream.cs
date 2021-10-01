// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
namespace Yaapii.Atoms.IO.Tests
{
    internal sealed class SlowInputStream : Stream
    {

        /**
         * Original stream.
         */
        private readonly Stream _input;

        /**
         * Ctor.
         * @param size The size of the array to encapsulate
         */
        internal SlowInputStream(int size) : this(new MemoryStream(new byte[size]))
        { }

        /**
         * Ctor.
         * @param stream Original stream to encapsulate and make slower
         */
        internal SlowInputStream(Stream stream) : base()
        {
            this._input = stream;
        }
        public override bool CanRead => _input.CanRead;

        public override bool CanSeek => _input.CanSeek;

        public override bool CanWrite => _input.CanWrite;

        public override long Length => _input.Length;

        public override long Position
        {
            get
            {
                return _input.Position;
            }
            set
            {
                _input.Position = value;
            }
        }


        public int Read()
        {
            byte[] buf = new byte[1];
            int result;
            if (this.Read(buf) < 0)
            {
                result = -1;
            }
            else
            {
                result = buf[0];
            }
            return result;
        }

        public override int Read(byte[] buf, int offset, int len)
        {
            int result;
            if (len > 1)
            {
                result = this._input.Read(buf, offset, len - 1);
            }
            else
            {
                result = this._input.Read(buf, offset, len);
            }
            return result;
        }

        public int Read(byte[] buf)
        {
            return this.Read(buf, 0, buf.Length);
        }

        public override void Flush()
        {
            this._input.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _input.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _input.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
#pragma warning restore MaxPublicMethodCount
