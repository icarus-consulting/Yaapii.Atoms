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
using System.IO;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="Stream"/> which copies to another <see cref="Stream"/> while writing.
    /// </summary>
    public sealed class TeeOutputStream : Stream, IDisposable
    {
        /// <summary>
        /// the target
        /// </summary>
        private readonly Stream _target;

        /// <summary>
        /// the copy
        /// </summary>
        private readonly Stream _copy;

        /// <summary>
        /// <see cref="Stream"/> which copies to another <see cref="Stream"/> while writing.
        /// </summary>
        /// <param name="tgt">the target</param>
        /// <param name="cpy">the copy target</param>
        public TeeOutputStream(Stream tgt, Stream cpy) : base()
        {
            this._target = tgt;
            this._copy = cpy;
        }

#pragma warning disable CS1591
        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                this._target.Write(buffer, offset, count);
            }
            finally
            {
                this._copy.Write(buffer, offset, count);
            }
        }


        public override void Flush()
        {
            try
            {
                this._target.Flush();
            }
            finally
            {
                this._copy.Flush();
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                this._target.Dispose();
            }
            catch (Exception) { }
            finally
            {
                try
                {
                    this._copy.Dispose();
                }
                catch (Exception) { }
            }
            base.Dispose(disposing);
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => this._target.Length;

        public override long Position { get { return this._target.Position; } set { this._target.Position = value; } }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            try
            {
                this._target.SetLength(value);
            }
            finally
            {
                this._copy.SetLength(value);
            }
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
#pragma warning restore CS1591
