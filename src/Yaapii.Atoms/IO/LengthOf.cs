using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Length of <see cref="IInput"/>.
    /// </summary>
    public sealed class LengthOf : IScalar<long>, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IInput _source;

        /// <summary>
        /// buffer size
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// Length of <see cref="IInput"/> by reading all bytes.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="max">maximum buffer size</param>
        public LengthOf(IInput input, int max = 16 << 10)
        {
            this._source = input;
            this._size = max;
        }

        public long Value()
        {
            using (var stream = this._source.Stream())
            {
                byte[] buf = new byte[this._size];
                long length = 0L;

                int bytesRead;
                while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
                {
                    length += (long)bytesRead;
                }
                return length;
            }
        }

        public void Dispose()
        {
            ((IDisposable)this._source.Stream()).Dispose();
        }
    }
}
