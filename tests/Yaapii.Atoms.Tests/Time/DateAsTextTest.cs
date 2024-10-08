// MIT License
//
// Copyright(c) 2024 ICARUS Consulting GmbH
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
using System.Globalization;
using Xunit;

namespace Yaapii.Atoms.Time.Tests
{
    public sealed class DateAsTextTest
    {
        [Fact]
        public void FormatsCurrentTime()
        {
            Assert.True(
                !String.IsNullOrEmpty(
                    new DateAsText().AsString()
                )
            );
        }

        [Fact]
        public void FormatsWithCustomPattern()
        {
            Assert.True(
                new DateAsText(
                    new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Local),
                    "yyyy-MM-dd HH:mm:ss").AsString() == "2017-12-13 14:15:16");
        }

        [Fact]
        public void FormatsWithInvariantCulture()
        {
            Assert.Equal(
                "12/13/2017 14:15:16",
                new DateAsText(
                    new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Unspecified),
                    ""
                ).AsString()
            );
        }

        [Fact]
        public void FormatsWithCustomCulture()
        {
            Assert.Equal(
                "13.12.2017 14:15:16",
                new DateAsText(
                    new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Unspecified),
                    "",
                    CultureInfo.GetCultureInfo("de-de")
                ).AsString()
            );
        }
    }
}
