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

using System.Linq;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class SplitTest
    {
        [Fact]
        public void SplitText()
        {
            Assert.True(
        new Split(
            "Hello world!", "\\s+"
        ).Select(s => s == "Hello" || s == "world!").Count() == 2
            );
        }

        [Fact]
        public void SplitEmptyText()
        {
            Assert.True(
                new LengthOf(
                    new Split("", "\n")).Value() == 0,
                    "Can't split an empty text");
        }

        [Fact]
        public void SplitStringWithTextRegex()
        {
            Assert.True(
                new Split(
                    "Atoms OOP!",
                    new LiveText("\\s")
                ).Select(s => s == "Atoms" || s == "OOP!").Count() == 2,
                "Can't split an string with text regex");
        }

        [Fact]
        public void SplitTextWithStringRegex()
        {
            Assert.True(
            new Split(
                new LiveText("Atoms4Primitives!"), "\\d+")
                .Select(s => s == "Atoms" || s == "Primitives!").Count() == 2,
            "Can't split an text with string regex");
        }

        [Fact]
        public void SplitTextWithTextRegex()
        {
            Assert.True(
                new Split(new LiveText("Split#OOP!"), "\\W+")
                .Select(s => s == "Split" || s == "OOP").Count() == 2,
                "Can't split an text with text regex");
        }

        [Fact]
        public void SplitTextRemoveEmptyStrings()
        {
            Assert.True(
                new LengthOf(
                    new Split(
                        new LiveText("Split##OOP!"),
                        "\\W+")).Value() == 2,
                "Can't remove empty strings");
        }

        [Fact]
        public void SplitTextContainsEmptyStrings()
        {
            Assert.True(
                new LengthOf(
                    new Split(
                        new LiveText("Split##OOP!"),
                        "\\W+",
                        false
                    )
                ).Value() == 3
            );
        }
    }
}
