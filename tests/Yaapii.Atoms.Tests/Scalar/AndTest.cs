// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class AndTest
    {
        [Fact]
        public void AllTrue()
        {
            Assert.True(
            new And<True>(
                new True(),
                new True(),
                new True()
            ).Value() == true);
        }

        [Fact]
        public void OneFalse()
        {
            Assert.True(
                new And<bool>(
                    new True(),
                    new False(),
                    new True()
                ).Value() == false
            );
        }

        [Fact]
        public void AllFalse()
        {
            Assert.True(
                    new And<string>(
                        new EnumerableOf<IScalar<Boolean>>(
                            new False(),
                            new False(),
                            new False()
                        )
                    ).Value() == false);
        }

        [Fact]
        public void EmptyIterator()
        {
            Assert.True(
                    new And<bool>(new EnumerableOf<IScalar<Boolean>>())
                    .Value() == true);
        }

        [Fact]
        public void EnumeratesList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                new And<string>(
                    new Enumerable.Mapped<String, IScalar<Boolean>>(
                        str => { list.AddLast(str); return new True(); },
                        new EnumerableOf<string>("hello", "world")
                    )
                ).Value() == true);

            Assert.True(
                new JoinedText(" ", list).AsString() == "hello world",
            "Can't iterate a list with a procedure");
        }

        [Fact]
        public void EnumeratesEmptyList()
        {
            var list = new LinkedList<string>();

            Assert.True(
                new And<string>(
                    new Enumerable.Mapped<string, IScalar<Boolean>>(
                        str => { list.AddLast(str); return new True(); },
                        new EnumerableOf<string>()
                    )
                ).Value() == true,
                "Can't enumerate a list"
                );

            Assert.True(list.Count == 0);
        }

        [Fact]
        public void TestFunc()
        {
            Assert.True(
                    new And<int>(
                        new FuncOf<int, bool>(input => input > 0),
                        1, -1, 0
                    ).Value() == false);
        }

        [Fact]
        public void TestNonGenFunc()
        {
            Assert.True(
                new And(
                    new FuncOf<bool,bool>((i) => { return true; }),
                    true,
                    true
                ).Value()
            );
        }
        [Fact]
        public void TestNonGenScalar()
        {
            Assert.True(
                    new And(
                        new ScalarOf<bool>(true),
                        new ScalarOf<bool>(true),
                        new ScalarOf<bool>(true)
                    ).Value()
            );
        }


    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum