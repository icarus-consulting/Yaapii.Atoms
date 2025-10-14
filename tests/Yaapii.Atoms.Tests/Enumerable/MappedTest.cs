// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class MappedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.Equal(
                "HELLO",
                new ItemAt<IText>(
                    new Enumerable.Mapped<String, IText>(
                        input => new Upper(new LiveText(input)),
                        new ManyOf<string>("hello", "world", "damn")),
                    0
                ).Value().AsString()
            );
        }

        [Fact]
        public void MappedResultIsSticky()
        {
            var mappings = 0;
            var mapping =
                new Enumerable.Mapped<String, IText>(
                    input =>
                    {
                        mappings++;
                        return new Upper(new LiveText(input));
                    },
                    new ManyOf<string>("hello", "world", "damn")
                );

            var enm1 = mapping.GetEnumerator();
            enm1.MoveNext(); var current = enm1.Current;
            var enm2 = mapping.GetEnumerator();
            enm2.MoveNext(); var current2 = enm2.Current;

            Assert.Equal(1, mappings);
        }

        [Fact]
        public void MappedResultCanBeLive()
        {
            var mappings = 0;
            var mapping =
                new Enumerable.Mapped<string, string>(
                    input =>
                    {
                        mappings++;
                        return input;
                    },
                    new ManyOf<string>("hello", "world", "damn"),
                    live: true
                );

            var enm1 = mapping.GetEnumerator();
            enm1.MoveNext(); var current = enm1.Current;
            var enm2 = mapping.GetEnumerator();
            enm2.MoveNext(); var current2 = enm2.Current;

            Assert.Equal(2, mappings);
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.True(
                new LengthOf(
                    new Enumerable.Mapped<String, IText>(
                        input => new Upper(new LiveText(input)),
                        new ManyOf<string>()
                    )
                ).Value() == 0
            );
        }

        [Fact]
        public void TransformsListUsingIndex()
        {
            Assert.Equal(
                "WORLD1",
                new ItemAt<IText>(
                    new Enumerable.Mapped<String, IText>(
                        (input, index) => new Upper(new LiveText(input + index)),
                        new ManyOf<string>("hello", "world", "damn")
                        ),
                    1
                ).Value().AsString()
            );
        }


        [Fact]
        public void AdvancesOnlyNecessary()
        {
            var advances = 0;
            var origin = new ListOf<string>("item1", "item2", "item3");

            var list =
                new Mapped<string, string>(
                    item => item,
                    new ManyOf<string>(
                        new Logging<string>(
                            origin,
                            idx => advances++
                        )
                    )
                );

            list.GetEnumerator().MoveNext();

            Assert.Equal(1, advances);

        }

        [Fact]
        public void CopyCtorDoesNotAdvance()
        {
            var advances = 0;
            var origin = new ListOf<string>("item1", "item2", "item3");

            var list =
                new Mapped<string, string>(
                    item => item,
                    new ManyOf<string>(
                        new Logging<string>(
                            origin,
                            idx => advances++
                        )
                    )
                );
            list.GetEnumerator();

            Assert.Equal(0, advances);

        }
    }
}
