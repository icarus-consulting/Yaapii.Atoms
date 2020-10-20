using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    public sealed class LoggingOutputStream : Stream
    {
        private readonly Stream origin;
        private readonly string destination;
        private readonly IScalar<IList<long>> bytes;
        private readonly IScalar<IList<long>> time;

        public LoggingOutputStream(Stream output, string destination)
        {
            this.origin = output;
            this.destination = destination;
            this.bytes = 
                new ScalarOf<IList<long>>(
                    new List<long>(1) 
                    { 
                        0 
                    });
            this.time = 
                new ScalarOf<IList<long>>(
                    new List<long>(1) 
                    { 
                        0 
                    });
        }

        public override bool CanRead => this.origin.CanRead;

        public override bool CanSeek => this.origin.CanSeek;

        public override bool CanWrite => this.origin.CanWrite;

        public override long Length => this.origin.Length;

        public override long Position { get => this.origin.Position; set => this.origin.Position = value; }

        public override void Flush()
        {
            this.origin.Flush();

            Debug.WriteLine($"Written {this.bytes.Value()[0]} byte(s) to {this.destination} in {this.time.Value()[0]}ms.");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.origin.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.origin.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.origin.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            DateTime start = DateTime.UtcNow;
            this.origin.Write(buffer, offset, count);
            DateTime end = DateTime.UtcNow;
            long millis = (long)end.Subtract(start).TotalMilliseconds;
            this.bytes.Value()[0] += count;
            this.time.Value()[0] += millis;

            Debug.WriteLine($"Written {this.bytes.Value()[0]} byte(s) to {this.destination} in {this.time.Value()[0]}.");
        }
    }
}
