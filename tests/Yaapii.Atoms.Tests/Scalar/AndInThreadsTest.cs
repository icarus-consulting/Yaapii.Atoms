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
    public sealed class AndInThreadsTest
    {

        [Fact]
        void AllTrue()
        {
            var result =
                new AndInThreads<bool>(
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
                new AndInThreads<bool>
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
                new AndInThreads<bool>
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
                new AndInThreads<bool>
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
                new AndInThreads<bool>(
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
                new AndInThreads<bool>(
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
                new AndInThreads<int>(
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
                new AndInThreads<bool>(
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
                new AndInThreads<bool>(
                    new ListOf<IScalar<bool>>( )
                ).Value();

            Assert.True(result);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum