// MIT License
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
using Yaapii.Atoms.Texts;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class IsNumberTests
    {
        [Fact]
        public void DetectsNumber()
        {
            Assert.True(
                new IsNumber(
                    "1,234.56"
                ).Value(),
                "Can't read number from string"
            );
        }

        [Fact]
        public void DetectsCustomCultureNumber()
        {
            Assert.True(
                new IsNumber(
                    "1234,56",
                    new System.Globalization.NumberFormatInfo
                    {
                        NumberDecimalSeparator = ","
                    }
                ).Value(),
                "Can't read number from string using custom format provider"
            );
        }

        [Fact]
        public void DetectsNumberFromText()
        {
            Assert.True(
                new IsNumber(
                    new Text.Live(
                        "1,234.56"
                    )
                ).Value(),
                "Can't read number from text"
            );
        }

        [Fact]
        public void DetectsCustomCultureNumberFromText()
        {
            Assert.True(
                new IsNumber(
                    new Text.Live(
                        "1234,56"
                    ),
                    new System.Globalization.NumberFormatInfo
                    {
                        NumberDecimalSeparator = ","
                    }
                ).Value(),
                "Can't read number from text using custom format provider"
            );
        }

        [Fact]
        public void DetectsNoNumber()
        {
            Assert.False(
                new IsNumber(
                    "not a number"
                ).Value(),
                "Falsely recognized a string as a number"
            );
        }

        [Fact]
        public void DetectsNoNumberFromText()
        {
            Assert.False(
                new IsNumber(
                    new Text.Live(
                        "not a number"
                    )
                ).Value(),
                "Falsely recognized a text as a number"
            );
        }
    }
}
