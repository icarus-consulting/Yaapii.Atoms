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
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
#pragma warning disable CS1591
#pragma warning disable CS0108

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A writable <see cref="Stream"/> out of other objects.
    /// </summary>
    public sealed class OutputStreamTo : Stream
    {
        /// <summary>
        /// the target
        /// </summary>
        private readonly IScalar<Stream> _target;

        /// <summary>
        /// A writable <see cref="Stream"/> of a file path.
        /// </summary>
        /// <param name="path"></param>
        public OutputStreamTo(string path) : this(new OutputTo(path))
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a StreamWriter.
        /// </summary>
        /// <param name="wtr">a writer</param>
        public OutputStreamTo(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a StreamWriter.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="enc">encoding of the writer</param>        
        public OutputStreamTo(StreamWriter wtr, Encoding enc) : this(new OutputTo(wtr, enc))
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a <see cref="IOutput"/>.
        /// </summary>
        /// <param name="output">an output</param>
        public OutputStreamTo(IOutput output) : this(new Live<Stream>(() => output.Stream()))
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a <see cref="IScalar{Stream}"/> objects.
        /// </summary>
        /// <param name="tgt">the target</param>
        private OutputStreamTo(IScalar<Stream> tgt) : base()
        {
            this._target = new ScalarOf<Stream>(tgt);
        }

        public async new void WriteAsync(byte[] buffer, int offset, int length)
        {
            await this._target.Value().WriteAsync(buffer, offset, length);
        }

        public override void WriteByte(byte b)
        {
            this._target.Value().WriteByte(b);
        }

        public override void Write(byte[] buffer, int offset, int length)
        {
            this._target.Value().Write(buffer, offset, length);
        }

        public void Dispose()
        {
            ((IDisposable)this._target.Value()).Dispose();
        }

        public override void Flush()
        {
            this._target.Value().Flush();
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => _target.Value().Length;

        public override long Position { get { return _target.Value().Position; } set { _target.Value().Position = value; } }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this._target.Value().Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this._target.Value().SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException(); //intended
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
