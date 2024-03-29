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

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Length of <see cref="IInput"/>. (Self-Disposing)
    /// </summary>
    public sealed class LengthOf : IScalar<long>
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

        /// <summary>
        /// Get the length. (Self-Disposing)
        /// </summary>
        /// <returns>the length</returns>
        public long Value()
        {
            long length = 0L;
            using (var stream = _source.Stream())
            {
                byte[] buf = new byte[this._size];

                int bytesRead;
                while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
                {
                    length += (long)bytesRead;
                }
            }
            return length;
        }
    }
}
