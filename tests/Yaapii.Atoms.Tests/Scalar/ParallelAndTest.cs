// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class ParallelAndTest
    {

        [Fact]
        void AllTrue()
        {
            var result =
                new ParallelAnd<bool>(
                    new True(),
                    new True(),
                    new True()
                ).Value();
            Assert.True(result);
        }

        [Fact]
        void OneFalse()
        {
            var result =
                new ParallelAnd<bool>
                (
                    new True(),
                    new False(),
                    new True()
                ).Value();
            Assert.False(result);
        }

        [Fact]
        void AllFalse()
        {
            var result =
                new ParallelAnd<bool>
                (
                    new False(),
                    new False(),
                    new False()
                ).Value();
            Assert.False(result);
        }

        [Fact]
        void EmtpyIterator()
        {
            var result =
                new ParallelAnd<bool>
                (
                    new ManyOf<IScalar<bool>>()
                ).Value();
            Assert.True(result);
        }

        [Fact]
        void IteratesList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                new ParallelAnd<bool>(
                    new Enumerable.Mapped<string, IScalar<bool>>(
                        str => { list.AddLast(str); return new True(); },
                        new ManyOf<string>("hello", "world")
                    )
                ).Value() &&
                list.Contains("hello") &&
                list.Contains("world")
            );
        }

        [Fact]
        void IteratesEmptyList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                new ParallelAnd<bool>(
                    new Enumerable.Mapped<string, IScalar<bool>>(
                        str => { list.AddLast(str); return new True(); },
                        new ManyOf<string>()
                    )
                ).Value() &&
                !list.Any()
            );
        }

        [Fact]
        void WorksWithFunc()
        {
            var result =
                new ParallelAnd<int>(
                    new FuncOf<int, bool>(i => i > 0),
                    1,
                    -1,
                    0
                ).Value();
            Assert.False(result);
        }

        [Fact]
        void WorksWithIterableScalarBool()
        {
            var result =
                new ParallelAnd<bool>(
                    new Atoms.List.ListOf<IScalar<bool>>(
                        new True(),
                        new True()
                    )
                ).Value();
            Assert.True(result);
        }

        [Fact]
        void WorksWithEmptyIterableScalarBool()
        {

            var result =
                new ParallelAnd<bool>(
                    new ListOf<IScalar<bool>>( )
                ).Value();

            Assert.True(result);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum