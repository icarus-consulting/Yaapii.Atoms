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

using Xunit;

namespace Yaapii.Atoms.Map.Tests
{
    public sealed class JoinedMapTests
    {
        [Fact]
        public void JoinsInputs()
        {
            var dict =
                new Joined(
                    new LazyDict(new KvpOf("A", "I am")),
                    new LazyDict(new KvpOf("B", "trapped in")),
                    new LazyDict(new KvpOf("C", "a dictionary"))
                );

            Assert.Equal(
                "I am trapped in a dictionary",
                $"{dict["A"]} {dict["B"]} {dict["C"]}"
            );
        }

        [Fact]
        public void ReplacesExisting()
        {
            var dict =
                new Joined(
                    new LazyDict(new KvpOf("A", "Hakuna")),
                    new LazyDict(new KvpOf("B", "Matata")),
                    new LazyDict(new KvpOf("B", "Banana"))
                );

            Assert.Equal(
                "Banana",
                dict["B"]
            );
        }

        [Fact]
        public void JoinsInputsTypedValue()
        {
            var dict =
                new Joined<int>(
                    new LazyDict<int>(new KvpOf<int>("A", 89)),
                    new LazyDict<int>(new KvpOf<int>("B", 17)),
                    new LazyDict<int>(new KvpOf<int>("C", 8))
                );

            Assert.Equal(
                "89 17 8",
                $"{dict["A"]} {dict["B"]} {dict["C"]}"
            );
        }

        [Fact]
        public void ReplacesExistingTypedValue()
        {
            var dict =
                new Joined<int>(
                    new LazyDict<int>(new KvpOf<int>("A", 1)),
                    new LazyDict<int>(new KvpOf<int>("B", 4)),
                    new LazyDict<int>(new KvpOf<int>("B", 19))
                );

            Assert.Equal(
                19,
                dict["B"]
            );
        }

        [Fact]
        public void JoinsInputsTypedKeyValue()
        {
            var dict =
                new Joined<int, int>(
                    new LazyDict<int, int>(new KvpOf<int, int>(0, 1)),
                    new LazyDict<int, int>(new KvpOf<int, int>(1, 3)),
                    new LazyDict<int, int>(new KvpOf<int, int>(2, 37))
                );

            Assert.Equal(
                "1337",
                $"{dict[0]}{dict[1]}{dict[2]}"
            );
        }

        [Fact]
        public void ReplacesExistingTypedKeyValue()
        {
            var dict =
                new Joined<int, int>(
                    new LazyDict<int, int>(new KvpOf<int, int>(0, 1)),
                    new LazyDict<int, int>(new KvpOf<int, int>(0, 4)),
                    new LazyDict<int, int>(new KvpOf<int, int>(0, 19))
                );

            Assert.Equal(
                19,
                dict[0]
            );
        }

        [Fact]
        public void JoinsLazy()
        {
            var dict =
                new Joined(
                    new LazyDict(new KvpOf("A", () => "I am")),
                    new LazyDict(new KvpOf("B", () => "trapped in")),
                    new LazyDict(new KvpOf("C", "a dictionary"))
                );

            Assert.Equal(
                "I am trapped in a dictionary",
                $"{dict["A"]} {dict["B"]} {dict["C"]}"
            );
        }
    }
}
