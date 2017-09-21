using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#pragma warning disable MaxPublicMethodCount
namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Readable <see cref="Stream"/> that copies input to <see cref="IOutput"/> while reading.
    /// </summary>
    public sealed class TeeInputStream : Stream, IDisposable
    {
        /// <summary>
        /// input
        /// </summary>
        private readonly Stream _input;

        /// <summary>
        /// destination
        /// </summary>
        private readonly Stream _output;

        /// <summary>
        /// Readable <see cref="Stream"/> that copies input to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="src">the source</param>
        /// <param name="tgt">the destination</param>
        public TeeInputStream(Stream src, Stream tgt) : base()
        {
            this._input = src;
            this._output = tgt;
        }

        public int Read()
        {
            var data = (Byte)this._input.ReadByte();
            if (data >= 0)
            {
                this._output.WriteByte(data);
            }
            return data;
        }

        public int Read(byte[] buf)
        {
            return this.Read(buf, 0, buf.Length);
        }

        public override int Read(byte[] buf, int offset, int len)
        {
            int max = this._input.Read(buf, offset, len);
            if (max > 0)
            {
                this._output.Write(buf, offset, max);
            }
            return max;
        }

        public long Skip(long num)
        {
            return this._input.Seek(num, SeekOrigin.Current);
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

        public new void Dispose()
        {
            try
            {
                this._input.Flush();
                ((IDisposable)this._input).Dispose(); //unelegant but C#...
            }
            catch (Exception) { }
            try
            {
                this._output.Flush();
                ((IDisposable)this._output).Dispose();
            }
            catch (Exception) { }
        }

        public override void Flush()
        {
            _input.Flush();
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
