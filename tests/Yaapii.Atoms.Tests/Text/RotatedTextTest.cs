﻿// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class RotatedTextTest
    {
        [Fact]
        public void RotateRightText()
        {
            Assert.True(
                new RotatedText(
                    new TextOf("Hello!"), 2
                ).AsString() == "o!Hell",
                "Can't rotate text to right");
        }

        [Fact]
        public void RotateLeftText()
        {
            Assert.True(
                new RotatedText(
                    new TextOf("Hi!"), -1
                ).AsString() == "i!H",
                "Can't rotate text to left");
        }

        [Fact]
        public void NoRotateWhenShiftZero()
        {
            var nonrotate = "Atoms!";
            Assert.True(
                new RotatedText(
                    new TextOf(nonrotate), 0
                ).AsString() == nonrotate,
                "Can't rotate text shift zero"
            );
        }

        [Fact]
        public void NoRotateWhenShiftModZero()
        {
            var nonrotate = "Rotate";
            Assert.True(
                new RotatedText(
                    new TextOf(nonrotate), nonrotate.Length
                ).AsString() == nonrotate,
                "Can't rotate text shift mod zero");
        }

        [Fact]
        public void NoRotateWhenEmpty()
        {
            Assert.True(
                new RotatedText(
                    new TextOf(""), 2
                ).AsString() == "",
            "Can't rotate text when empty");
        }
    }
}
