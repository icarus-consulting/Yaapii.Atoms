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

using System;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Primitives.Tests
{
    public sealed class CharOfTest
    {
        [Fact]
        public void CharOfInteger()
        {
            Assert.True(
                new CharOf((int)1337).Value() == (char)((int)1337));
        }

        [Fact]
        public void ChardOfIntegerOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((int)2147483647).Value());
        }

        [Fact]
        public void CharOfUInteger()
        {
            Assert.True(
                new CharOf((uint)1337).Value() == (char)((uint)1337));
        }

        [Fact]
        public void ChardOfUIntegerOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((uint)2147483647).Value());
        }

        [Fact]
        public void CharOfLong()
        {
            Assert.True(
                new CharOf((long)1338).Value() == (char)((long)1338));
        }

        [Fact]
        public void ChardOfLongOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((long)2147483647).Value());
        }

        [Fact]
        public void CharOfULong()
        {
            Assert.True(
                new CharOf((ulong)1338).Value() == (char)((ulong)1338));
        }

        [Fact]
        public void ChardOfULongOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((ulong)2147483647).Value());
        }

        [Fact]
        public void CharOfShort()
        {
            Assert.True(
                new CharOf((short)13).Value() == (char)((short)13));
        }

        [Fact]
        public void CharOfUShort()
        {
            Assert.True(
                new CharOf((ushort)1338).Value() == (char)((ushort)1338));
        }

        [Fact]
        public void CharOfDouble()
        {
            Assert.True(
                new CharOf((double)1338.0).Value() == (char)Convert.ToInt64((double)1338.0));
        }

        [Fact]
        public void ChardOfDoubleOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((double)2147483647.0).Value());
        }

        [Fact]
        public void CharOfFloat()
        {
            Assert.True(
                new CharOf((float)1338.0).Value() == (char)Convert.ToInt64((float)1338.0));
        }

        [Fact]
        public void ChardOfFloatOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((float)2147493647.0).Value());
        }

        [Fact]
        public void CharOfString()
        {
            Assert.True(
                new CharOf("b").Value() == 'b');
        }

        [Fact]
        public void CharOfByte()
        {
            Assert.True(
                new CharOf((byte)13).Value() == (char)((byte)13));
        }

        [Fact]
        public void CharOfSByte()
        {
            Assert.True(
                new CharOf((sbyte)13).Value() == (char)((sbyte)13));
        }
    }
}
