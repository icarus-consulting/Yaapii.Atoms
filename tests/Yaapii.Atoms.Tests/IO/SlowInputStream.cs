using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#pragma warning disable MaxPublicMethodCount
namespace Yaapii.Atoms.Tests.IO
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