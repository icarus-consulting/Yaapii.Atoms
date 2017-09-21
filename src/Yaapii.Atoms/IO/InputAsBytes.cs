using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Input as bytes.
    /// </summary>
    public sealed class InputAsBytes : IBytes, IDisposable
    {
        /// <summary>
        /// input
        /// </summary>
        private readonly IInput _source;

        /// <summary>
        /// buffer size
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// Input as bytes.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="max">maximum buffer size</param>
        public InputAsBytes(IInput input, int max = 16 << 10)
        {
            this._source = input;
            this._size = max;
        }

        public byte[] AsBytes()
        {
            var baos = new MemoryStream();
            byte[] output;
            using (var source = this._source.Stream())
            using (var stream = new TeeInput(new InputOf(source), new OutputTo(baos)).Stream())
            {
                output = new byte[source.Length];
                byte[] readBuffer = new byte[this._size];
                while ((stream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                { }
                output = baos.ToArray();
            }
            return output;
        }

        public void Dispose()
        {
            try
            {
                ((IDisposable)this._source.Stream()).Dispose();
            }
            catch (Exception) { }
        }
    }
}
