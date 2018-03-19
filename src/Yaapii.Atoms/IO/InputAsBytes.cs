// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Input as bytes. (Self-Disposing)
    /// </summary>
    public sealed class InputAsBytes : IBytes
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

        /// <summary>
        /// Get the content as byte array. (Self-Disposing)
        /// </summary>
        /// <returns>content as byte array</returns>
        public byte[] AsBytes()
        {
            var baos = new MemoryStream();
            byte[] output;
            using (var source = this._source.Stream())
            using (var stream = new TeeInput(new InputOf(source), new OutputTo(baos)).Stream())
            {
                //output = new byte[source.Length];
                byte[] readBuffer = new byte[this._size];
                while ((stream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                { }
                output = baos.ToArray();
            }

            Dispose();
            return output;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        private void Dispose()
        {
            try
            {
                (_source as IDisposable)?.Dispose();
            }
            catch (Exception) { }
        }
    }
}
