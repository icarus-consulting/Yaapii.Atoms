using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    public sealed class HeadInputStream : Stream
    {
        private readonly IScalar<Stream> origin;
        private readonly long length;
        private readonly IList<long> processed;


        public HeadInputStream(Stream origin, int length)
        {
            this.origin =
                new ScalarOf<Stream>(origin);
            this.length = length;
            this.processed = new List<long>() { 0 };
        }


        public override bool CanRead => this.origin.Value().CanRead;

        public override bool CanSeek => this.origin.Value().CanSeek;

        public override bool CanWrite => this.origin.Value().CanWrite;

        public override long Length => this.origin.Value().Length;

        public override long Position
        {
            get
            {
                return this.origin.Value().Position;
            }
            set
            {
                throw new NotImplementedException(); //intended
            }
        }
        public override void Flush()
        {
            origin.Value().Flush();
        }


        public override int Read(byte[] buf, int offset, int len)
        {
            if(this.processed[0] < this.length)
            {
                var dif = this.length - this.processed[0];
                this.processed[0] = this.length;
                return this.origin.Value().Read(buf, offset, (int)(dif));
            }
            else
            {
                this.processed[0] = 0;
                return 0;
            }

            
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long adjusted;
            if (this.processed[0] + offset > this.length)
            {
                adjusted = this.length - this.processed[0];
            }
            else
            {
                adjusted = offset;
            }

            long skipped = this.origin.Value().Seek(adjusted, SeekOrigin.Begin);
            this.processed[0] = this.processed[0] + skipped;
            return skipped;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
