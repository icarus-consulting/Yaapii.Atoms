// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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
using System.Diagnostics;
using System.IO;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Logged input stream.
    /// </summary>
    public sealed class LoggingInputStream : Stream
    {

        private readonly Stream origin;
        private readonly string source;
        private readonly Action<string> log;
        private long bytes;
        private long time;

        /// <summary>
        /// Logged input stream.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="source"></param>
        public LoggingInputStream(Stream input, string source) : this(input, source, (msg) => Debug.WriteLine(msg))
        { }

        /// <summary>
        /// Logged input stream
        /// </summary>
        /// <param name="input"></param>
        /// <param name="source"></param>
        /// <param name="log"></param>
        public LoggingInputStream(Stream input, string source, Action<String> log)
        {
            this.origin = input;
            this.source = source;
            this.log = log;
        }

        public override bool CanRead => this.origin.CanRead;

        public override bool CanSeek => this.origin.CanSeek;

        public override bool CanWrite => this.origin.CanWrite;

        public override long Length => this.origin.Length;

        public override long Position { get => this.origin.Position; set => this.origin.Position = value; }

        public override void Flush()
        {
            this.origin.Flush();
        }

        public override int ReadByte()
        {
            byte[] buf = new byte[1];
            int size;
            if (this.Read(buf, 0, buf.Length) == 0)
            {
                size = 0;
            }
            else
            {
                size = Convert.ToInt32(Convert.ToUInt32(buf[0]));
            }
            return size;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            DateTime start = DateTime.UtcNow;
            int byts = this.origin.Read(buffer, offset, count);
            DateTime end = DateTime.UtcNow;

            long millis = (long)end.Subtract(start).TotalMilliseconds;
            if (byts > 0)
            {
                this.bytes += byts;
                this.time += millis;
            }
            var msg = $"Read {this.bytes} byte(s) from {this.source} in {this.time}.";
            log.Invoke(msg);

            return byts;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long skipped = this.origin.Seek(offset, origin);
            var msg = $"Skipped {skipped} byte(s) from {this.source}.";
            log.Invoke(msg);
            return skipped;
        }

        public override void SetLength(long value)
        {
            this.origin.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.origin.Write(buffer, offset, count);
        }
    }
}
