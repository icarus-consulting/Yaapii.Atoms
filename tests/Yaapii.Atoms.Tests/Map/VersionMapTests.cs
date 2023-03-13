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
using Yaapii.Atoms.Map;

namespace Yaapii.Lookup.Tests
{
    public sealed class VersionMapTests
    {
        [Fact]
        public void MatchesKeyLow()
        {
            Assert.True(
                new VersionMap(true,
                    new KvpOf<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new KvpOf<Version, string>(new Version(2, 0, 0, 0), "zway")
                ).ContainsKey(new Version(1, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesKeyBetween()
        {
            Assert.True(
                new VersionMap(true,
                    new KvpOf<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new KvpOf<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(2, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesOpenEnd()
        {
            Assert.True(
                new VersionMap(true,
                    new KvpOf<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new KvpOf<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(10, 0, 0, 0))
            );
        }

        [Fact]
        public void RejectsOverClosedEnd()
        {
            Assert.False(
                new VersionMap(false,
                    new KvpOf<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new KvpOf<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(10, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesWithinClosedEnd()
        {
            Assert.True(
                new VersionMap(false,
                    new KvpOf<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new KvpOf<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(2, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesEndingZero()
        {
            Assert.True(
                new VersionMap(false,
                    new KvpOf<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new KvpOf<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(1, 0))
            );
        }

        [Fact]
        public void MatchesMajorMinorVersion()
        {
            Assert.Equal(
                "zway",
                new VersionMap(true,
                    new KvpOf<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new KvpOf<Version, string>(new Version(5, 0, 0, 0), "zway")
                )[new Version(5, 0)]
            );
        }
    }
}
