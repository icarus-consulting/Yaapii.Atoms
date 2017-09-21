using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Stream with killed methods.
    /// </summary>
    public sealed class DeadStream : Stream
    {
        /// <summary>
        /// Stream with killed methods.
        /// </summary>
        public DeadStream()
        { }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        public override long Length => 0;

        public override long Position { get => 0; set => throw new NotSupportedException("#SetPosition is not supported"); }

        public override void Flush()
        { }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0;
        }

        public override void SetLength(long value)
        {
        }

        public override void Write(byte[] buffer, int offset, int count)
        {

        }
    }
}
