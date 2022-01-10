// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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

using System.Security.Cryptography;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Digest Envelope
    /// </summary>
    public abstract class DigestEnvelope : IBytes
    {
        private readonly IInput source;
        private readonly IScalar<HashAlgorithm> algorithmFactory;

        /// <summary>
        /// Digest Envelope of Input
        /// </summary>
        /// <param name="source">Input</param>
        /// <param name="algorithmFactory">Factory to create Hash Algorithm</param>
        public DigestEnvelope(IInput source, IScalar<HashAlgorithm> algorithmFactory)
        {
            this.source = source;
            this.algorithmFactory = algorithmFactory;
        }

        /// <summary>
        /// Digest
        /// </summary>
        public byte[] AsBytes()
        {
            using (var sha = algorithmFactory.Value())
            {
                return sha.ComputeHash(source.Stream());
            }
        }
    }
}
