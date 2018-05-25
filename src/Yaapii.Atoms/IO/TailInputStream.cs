using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    public sealed class TailInputStream : Stream, IDisposable
    {
        private readonly Stream input;

        public TailInputStream(Stream input) : base()
        {
            this.input = input;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                this.input.Flush();
                this.input.Dispose();
            }
            catch (Exception) { }

            base.Dispose(disposing);
        }

        public override bool CanRead => input.CanRead;

        public override bool CanSeek => input.CanSeek;

        public override bool CanWrite => input.CanWrite;

        public override long Length => throw new NotImplementedException();

        // cju: need to think about
        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            this.input.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // cju: implement tail reading
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            // cju: implement revert seeking
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            // cju: more information needed to implement
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
