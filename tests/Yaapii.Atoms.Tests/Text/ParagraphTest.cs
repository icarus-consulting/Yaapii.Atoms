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

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class ParagraphTest
    {
        [Fact]
        public void BuildsWithParamsString()
        {
            var p = new Paragraph("a", "b", "c");
            Assert.Equal("a\nb\nc".Replace("\n", Environment.NewLine), p.ToString());
        }

        [Fact]
        public void BuildsWithITextEnumerable()
        {
            var p = new Paragraph(
                new LiveMany<IText>(
                    new LiveText("a"),
                    new LiveText("b"),
                    new LiveText("c")
                ));
            Assert.Equal("a\nb\nc".Replace("\n", Environment.NewLine), p.ToString());
        }

        [Fact]
        public void HeadArrayTailStrings()
        {
            var p = new Paragraph(
                "Hello", "World",
                new string[] { "I", "was", "here" },
                "foo", "bar"
            );
            Assert.Equal("Hello\nWorld\nI\nwas\nhere\nfoo\nbar".Replace("\n", Environment.NewLine), p.ToString());
        }

        [Fact]
        public void HeadsAndTailsMixedITextStrings()
        {
            var p = new Paragraph(
                new LiveText("Hello"), new LiveText("World"),
                new string[] { "I", "was", "here" },
                new LiveText("foo"), new LiveText("bar")
            );
            Assert.Equal("Hello\nWorld\nI\nwas\nhere\nfoo\nbar".Replace("\n", Environment.NewLine), p.ToString());
        }
    }
}
