using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class OrTests
    {
        [Fact]
        public void TrueOrGenEnumerable()
        {
            Assert.True(
                new Or<bool>(
                    new EnumerableOf<IScalar<bool>>(
                        new ScalarOf<bool>(true),
                        new ScalarOf<bool>(false)
                    )
                ).Value()
            );
        }
        [Fact]
        public void TrueOrGenFuncs()
        {
            Assert.True(
                new Or<bool>(
                    func => func,
                    new True().Value(),
                    new False().Value()
                    ).Value() == true
            );
        }
        [Fact]
        public void TrueOrGenScalar()
        {
            Assert.True(
                new Or<bool>(
                    new True(),
                    new False(),
                    new True()
                    ).Value() == true
            );
        }
        [Fact]
        public void TrueOrGenAction()
        {
            Assert.True(
                new Or<IAction<bool>>(
                    act => act.Invoke(true),
                    new ActionOf<bool>(() => new True()),
                    new ActionOf<bool>(() => new True())
                ).Value() == true
            );
        }
    }
}

