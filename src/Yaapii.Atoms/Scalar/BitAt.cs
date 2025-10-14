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

using System;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// The value of a particular bit.
    /// </summary>
    public sealed class BitAt : ScalarEnvelope<bool>
    {
        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="exception">Exception which is thrown in case of an error</param>
        public BitAt(IBytes bytes, Exception exception) : this(
            bytes,
            0,
            exception
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="exception">Exception which is thrown in case of an error</param>
        public BitAt(IBytes bytes, int position, Exception exception) : this(
            bytes,
            position,
            itr => throw exception
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        public BitAt(IBytes bytes) : this(
            bytes,
            itr => throw new ArgumentException($"Cannot get first bit because there are only {bytes.AsBytes().Length} bytes.")
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, bool fallback) : this(
            bytes,
            itr => fallback
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, bool fallback) : this(
            bytes,
            itr => fallback
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, Func<IBytes, bool> fallback) : this(
            bytes,
            0,
            fallback
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, IFunc<IBytes, bool> fallback) : this(
            bytes,
            0,
            fallback
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        public BitAt(IBytes bytes, int position) : this(
            bytes,
            position,
            itr => throw new ArgumentException($"Cannot get bit at position {position} because there are only {bytes.AsBytes().Length} bytes.")
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, IFunc<IBytes, bool> fallback) : this(
            bytes,
            position,
            (bts) => fallback.Invoke(bts)
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, Func<IBytes, bool> fallback)
            : base(() =>
            {
                if (position < 0)
                {
                    throw new ArgumentException($"The position must be non-negative but is {position}");
                }

                bool result;
                var byteIndex = position / 8;
                var bitInByteIndex = position % 8;
                var bytesArr = bytes.AsBytes();
                if (bytesArr.Length > byteIndex)
                {
                    var relevantByte = bytesArr[byteIndex];
                    result = (relevantByte & (1 << bitInByteIndex)) > 0;
                }
                else
                {
                    result = fallback.Invoke(bytes);
                }

                return result;
            })
        { }
    }
}
