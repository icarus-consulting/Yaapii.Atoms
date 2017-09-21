using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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


        public new void Dispose()
        {
            try
            {
                this._target.Dispose();
            }
            catch(Exception ex)
            {
                
            }
            finally
            {
                try
                {
                    this._copy.Dispose();
                }
                catch (Exception ex) { }
            }
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => this._target.Length;

        public override long Position { get { return this._target.Position; }  set { this._target.Position = value; } }

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
