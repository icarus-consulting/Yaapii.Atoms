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
using System.Collections.Generic;
using Xunit;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class JoinedTest
    {
        [Fact(Skip = "leads to stack overflow")]
        public void WorksWithBigNestings()
        {
            IEnumerable<string> list = new ManyOf<string>();

            for (int i = 0; i < 2000; i++)
            {
                list = new Joined<string>(list, (i*Math.PI).ToString());
            }

            Assert.Equal(
                2000,
                new LengthOf(list).Value()
            );
        }

        [Fact]
        public void TransformsList()
        {
            Assert.True(
                new LengthOf(
                    new Joined<string>(
                        new ManyOf<string>("hello", "world", "друг"),
                        new ManyOf<string>("how", "are", "you"),
                        new ManyOf<string>("what's", "up")
                    )
                ).Value() == 8,
            "Can't concatenate enumerables together");
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.True(
                new LengthOf(
                    new Joined<IEnumerable<string>>(
                        new Mapped<string, IEnumerable<string>>(
                           str => new ManyOf<string>(str),
                           new ManyOf<string>("x")
                        )
                )).Value() == 1,
            "cannot join mapped iterables together");
        }

        [Fact]
        public void JoinsSingleElemtns()
        {
            Assert.True(
                new LengthOf(
                    new Joined<string>(
                        new ManyOf<string>("hello", "world", "друг"),
                        "how",
                        "are",
                        "you",
                        "what's",
                        "up"
                    )
                ).Value() == 8,
            "Can't concatenate enumerable with ingle values");
        }
    }
}
