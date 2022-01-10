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

using System;
using Xunit;

namespace Yaapii.Atoms.Time.Tests
{
    public sealed class DateOfTest
    {
        [Fact]
        public void CanParseIsoString()
        {
            Assert.True(
                new DateOf("2017-12-13T14:15:16.0170000+0:00").Value().ToUniversalTime() ==
                new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Utc).ToUniversalTime());
        }

        [Fact]
        public void CanParseUtcDate()
        {
            Assert.True(
                new DateOf("Fri, 29 Mar 2019 12:50:36 GMT").Value().ToUniversalTime() ==
                new DateTime(2019, 03, 29, 12, 50, 36, DateTimeKind.Utc).ToUniversalTime()
            );
        }

        [Fact]
        public void CanParseCustomFormat()
        {
            Assert.True(
                new DateOf(
                    "2017-12-13 14:15:16.0170000",
                    "yyyy-MM-dd HH:mm:ss.fffffff").Value().ToUniversalTime() ==
                new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Utc).ToUniversalTime());
        }

    }
}
