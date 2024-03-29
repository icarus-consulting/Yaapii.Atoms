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

using System;
using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class ReplacedTest
    {
        [Fact]
        public void ReplaceText()
        {
            Assert.True(
                new Replaced(
                    new LiveText("Hello!"),
                    "ello", "i"
                ).AsString() == "Hi!",
                "Can't replace a text");
        }

        [Fact]
        public void NotReplaceTextWhenSubstringNotFound()
        {
            String text = "HelloAgain!";
            Assert.True(
                new Replaced(
                    new LiveText(text),
                    "xyz", "i"
                ).AsString() == text,
                "Replace a text abnormally");
        }

        [Fact]
        public void ReplacesAllOccurrences()
        {
            Assert.True(
                new Replaced(
                    new LiveText("one cat, two cats, three cats"),
                    "cat",
                    "dog"
                ).AsString() == "one dog, two dogs, three dogs",
                "Can't replace a text with multiple needle occurrences");
        }
    }
}
