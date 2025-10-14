// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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
using System.IO.Compression;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A output that compresses.
    /// </summary>
    public sealed class GZipOutput : IOutput
    {
        // The input.
        private readonly IOutput _output;

        // The buffer size.
        private readonly CompressionLevel _level;

        /// <summary>
        /// The output as a gzip output. It compresses with level 'optimal'.
        /// </summary>
        /// <param name="output">the input</param>
        public GZipOutput(IOutput output) : this(output, CompressionLevel.Optimal)
        { }

        /// <summary>
        /// The output as a gzip compressed stream.
        /// </summary>
        /// <param name="output">the output to compress</param>
        /// <param name="level">the compression level</param>
        public GZipOutput(IOutput output, CompressionLevel level)
        {
            this._output = output;
            this._level = level;
        }

        /// <summary>
        /// A stream configured to decompressing.
        /// </summary>
        /// <returns></returns>
        public Stream Stream()
        {
            return new GZipStream(this._output.Stream(), this._level, true);
        }
    }
}
