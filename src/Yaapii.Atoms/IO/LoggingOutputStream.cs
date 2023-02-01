// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.Diagnostics;
using System.IO;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Logged output stream.
    /// </summary>
    public sealed class LoggingOutputStream : Stream
    {
        private readonly Stream origin;
        private readonly string destination;
        private readonly Action<string> log;
        private readonly IList<long> bytes;
        private readonly IList<long> time;

        /// <summary>
        /// Logged output stream.
        /// </summary>
        /// <param name="output">Destination of data</param>
        /// <param name="destination">The name of source data</param>
        public LoggingOutputStream(Stream output, string destination) : this(output, destination, (msg) => Debug.WriteLine(msg))
        { }

        /// <summary>
        /// Logged output stream.
        /// </summary>
        /// <param name="output">Destination of data</param>
        /// <param name="destination">The name of the source data</param>
        /// <param name="log">The log action</param>
        public LoggingOutputStream(Stream output, string destination, Action<string> log)
        {
            this.origin = output;
            this.destination = destination;
            this.log = log;
            this.bytes =
                new List<long>(1)
                {
                    0
                };
            this.time =
                new List<long>(1)
                {
                    0
                };
        }

        public override bool CanRead => this.origin.CanRead;

        public override bool CanSeek => this.origin.CanSeek;

        public override bool CanWrite => this.origin.CanWrite;

        public override long Length => this.origin.Length;

        public override long Position { get => this.origin.Position; set => this.origin.Position = value; }

        public override void Flush()
        {
            this.origin.Flush();

            log.Invoke($"Written {this.bytes[0]} byte(s) to {this.destination} in {this.time[0]}ms.");
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
            this.bytes[0] += count;
            this.time[0] += millis;

            log.Invoke($"Written {this.bytes[0]} byte(s) to {this.destination} in {this.time[0]}.");
        }
    }
}
