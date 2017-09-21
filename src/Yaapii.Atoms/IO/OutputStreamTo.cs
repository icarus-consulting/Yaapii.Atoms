using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
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
        private readonly UncheckedScalar<Stream> _target;

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
        public OutputStreamTo(IOutput output) : this(new ScalarOf<Stream>(() => output.Stream()))
        { }

        /// <summary>
        /// A writable <see cref="Stream"/> out of a <see cref="IScalar{Stream}"/> objects.
        /// </summary>
        /// <param name="tgt">the target</param>
        private OutputStreamTo(IScalar<Stream> tgt) : base()
        {
            this._target = new UncheckedScalar<Stream>(new StickyScalar<Stream>(tgt));
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

        public new void Dispose()
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