using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Tests.Scalar
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
                    new Mapped<String, IScalar<Boolean>>(
                        new EnumerableOf<string>("hello", "world"),
                        str => { list.AddLast(str); return new True(); }
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
                    new Mapped<string, IScalar<Boolean>>(
                        new EnumerableOf<string>(),
                        str => { list.AddLast(str); return new True(); }
                    )
                ).Value() == true,
                "Can't enumerate a list"
                );

            Assert.True(list.Count == 0);
        }

        [Fact]
        public void TestProc()
        {
            var list = new LinkedList<int>();
            new And<int>(
                new ProcOf<int>(i => list.AddLast(i)),
                1, 1
            ).Value();


            Assert.True(
                list.Count == 2);
        }

        [Fact]
        public void TestFunc()
        {
            Assert.True(
                    new And<int>(
                        input => input > 0,
                        1, -1, 0
                    ).Value() == false);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum