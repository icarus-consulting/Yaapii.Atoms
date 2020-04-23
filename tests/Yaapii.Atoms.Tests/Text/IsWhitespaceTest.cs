// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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

using Xunit;

namespace Yaapii.Atoms.Texts.Tests
{
    public sealed class IsWhitespaceTest
    {
        [Fact]
        public void ConvertsString()
        {
            Assert.True(
                new IsWhitespace(
                    " "
                ).Value(),
                "Can't convert string"
            );
        }

        [Fact]
        public void DoesntMatchEmpty()
        {
            Assert.True(
                new IsWhitespace(
                    new LiveText("")
                ).Value(),
                "Can't determine an empty text");
        }

        [Fact]
        public void MatchesWhitespace()
        {
            Assert.True(
                new IsWhitespace(
                    new LiveText("  ")
                ).Value(),
                "Can't determine an empty text with spaces");
        }

        [Fact]
        public void DoesntMatchNotWhitespace()
        {
            Assert.False(
                new IsWhitespace(
                    new LiveText("not empty")
                ).Value(),
                "Can't detect a nonempty text");
        }
    }
}