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

using System.Threading;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List.Tests
{
    public sealed class ListOfTest
    {
        [Fact]
        public void IgnoresChangesInList()
        {
            int size = 2;
            var list =
                new ListOf<int>(
                    new Yaapii.Atoms.Enumerable.HeadOf<int>(
                        new Yaapii.Atoms.Enumerable.Endless<int>(1),
                        new Live<int>(() => Interlocked.Increment(ref size))
                ));

            Assert.Equal(
                new Enumerable.LengthOf(list).Value(),
                new Enumerable.LengthOf(list).Value()
            );
        }

        [Fact]
        public void ContainsWorksWithFirstItem()
        {
            var list = new ListOf<string>("item");
            Assert.Contains("item", list);
        }

        [Fact]
        public void ContainsWorksWithHigherItem()
        {
            var list = new ListOf<string>("item1", "item2", "item3");
            Assert.Contains("item2", list);
        }

        [Fact]
        public void CountingAdvancesAll()
        {
            var advances = 0;
            var origin = new ListOf<string>("item1", "item2", "item3");

            var list =
                new ListOf<string>(
                    new Enumerator.Sticky<string>(
                        new Enumerator.Sticky<string>.Cache<string>(() =>
                            new LoggingEnumerator<string>(
                                origin.GetEnumerator(),
                                idx => advances++
                            )
                        )
                    )
                );

            var count = list.Count;

            Assert.Equal(3, advances);

        }

        [Fact]
        public void FindsIndexOf()
        {
            var lst = new ListOf<string>("item1", "item2", "item3");

            Assert.Equal(
                2,
                lst.IndexOf("item3")
            );
        }

        [Fact]
        public void DeliversIndexWhenNoFinding()
        {
            var lst = new ListOf<string>("item1", "item2", "item3");

            Assert.Equal(
                -1,
                lst.IndexOf("item100")
            );
        }

        [Fact]
        public void CanCopyTo()
        {
            var array = new string[5];
            var origin = new ListOf<string>("item1", "item2", "item3");
            origin.CopyTo(array, 2);

            Assert.Equal(
                new string[] { null, null, "item1", "item2", "item3" },
                array
            );
        }

        [Fact]
        public void ContainsWorksWithEmptyList()
        {
            var list = new ListOf<string>();
            Assert.DoesNotContain("item", list);
        }
    }
}
