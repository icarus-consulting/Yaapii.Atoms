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

using System;
using System.Globalization;
using System.IO;
using Xunit;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public class SiblingTest
    {
        [Fact]
        public void Next()
        {
            Assert.True(
            new Sibling<int>(
                2,
                new ManyOf<int>(1, 2, 3)
            ).Value() == 3,
            "Can't get the right neighbour from the enumerable"
        );
        }

        [Fact]
        public void ByPos()
        {
            Assert.True(
                new Sibling<int>(
                    2,
                    new ManyOf<int>(1, 2, 3),
                    -1
                ).Value() == 1,
            "Can't take the left neighbour from the enumerable");
        }

        [Fact]
        public void FailForInvalidPosition()
        {
            Assert.True(
                new Sibling<string>(
                    "1",
                    new ManyOf<string>("1", "2", "3"),
                    -1,
                    "15"
                ).Value() == "15");
        }

        [Fact]
        public void FailForEmptyCollection()
        {
            Assert.Throws<IOException>(
                () =>
                    new Sibling<int>(
                        1337,
                        new ManyOf<int>()
                ).Value());
        }

        [Fact]
        public void FallbackTest()
        {
            String fallback = "fallback";
            Assert.True(
                new Sibling<string>(
                    "Not-there",
                    new ManyOf<string>(),
                    12,
                    fallback
                ).Value() == fallback,
            "Can't fallback to default value");
        }

        [Fact]
        public void WithCustomComparable()
        {
            var format = "dd.MM.yyyy";
            var provider = CultureInfo.InvariantCulture;
            var nb1 = new FakeSibling(DateTime.ParseExact("11.10.2017", format, provider));
            var nb2 = new FakeSibling(DateTime.ParseExact("10.10.2017", format, provider));
            var nb3 = new FakeSibling(DateTime.ParseExact("13.10.2017", format, provider));

            Assert.True(
                new Sibling<FakeSibling>(
                    nb1,
                    new ManyOf<FakeSibling>(nb1, nb2),
                    -1,
                    nb2
                ).Value().TimeStamp() == nb2.TimeStamp(),
            "Can't take the item by position from the enumerable");
        }

        internal class FakeSibling : IComparable<FakeSibling>
        {
            private readonly DateTime _stmp;

            public FakeSibling(DateTime stmp)
            {
                _stmp = stmp;
            }

            public DateTime TimeStamp() { return _stmp; }

            public int CompareTo(FakeSibling obj)
            {
                return _stmp.CompareTo(obj.TimeStamp());
            }
        }
    }
}
