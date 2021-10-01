// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Enumerator.Tests
{
    public sealed class CycledTest
    {
        [Fact]
        public void RepeatIteratorTest()
        {
            string expected = "two";

            Assert.True(
                new ItemAt<string>(
                    new Cycled<string>(
                        new ManyOf<string>(
                            "one", expected, "three"
                            )
                        ),
                    7).Value() == expected,
                "Can't repeat enumerator");
        }

        [Fact]
        public void EmptyThrowsExceptionTest()
        {
            Assert.False(
                    new Cycled<string>(
                        new ManyOf<string>(
                            new string[0]
                            )
                        ).MoveNext(),
                "Can move but am expected to not move");
        }
    }
}
