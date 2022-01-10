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

using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class StartsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            var x =
                new StartsWith(
                    new LiveText("Im an text with a really good end!"),
                    new LiveText("Im a")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void MatchesString()
        {
            var x =
                new StartsWith(
                    new LiveText("Im a text with a really good end!"),
                    "Im a"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void DoesntMatch()
        {
            var x =
                new StartsWith(
                    new LiveText("Im a text with a really good end!"),
                    new LiveText("m an")
                );
            Assert.False(x.Value());
        }
    }
}
