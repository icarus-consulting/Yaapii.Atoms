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

using System.IO;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Input as bytes. Disposes input.
    /// </summary>
    public sealed class InputAsBytes : IBytes
    {
        private readonly IScalar<byte[]> bytes;

        /// <summary>
        /// Input as bytes.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="max">maximum buffer size</param>
        public InputAsBytes(IInput input, int max = 16 << 10)
        {
            this.bytes = new ScalarOf<byte[]>(() =>
            {
                var baos = new MemoryStream();
                byte[] output;
                using (var source = input.Stream())
                using (var stream = new TeeInput(new InputOf(source), new OutputTo(baos)).Stream())
                {
                    byte[] readBuffer = new byte[max];
                    while ((stream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    { }
                    output = baos.ToArray();
                }
                return output;
            });
        }

        /// <summary>
        /// Get the content as byte array. (Self-Disposing)
        /// </summary>
        /// <returns>content as byte array</returns>
        public byte[] AsBytes()
        {
            return this.bytes.Value();
        }
    }
}
