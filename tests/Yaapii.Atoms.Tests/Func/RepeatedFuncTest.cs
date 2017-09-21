using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class RepeatedFuncTest
    {
        [Fact]
        public void RunsFuncMultipleTimes()
        {
            var iter = new StickyEnumerator<int>(1, 2, 5, 6);
            var func = new RepeatedFunc<bool, IScalar<int>>(
                input =>
                {
                    iter.MoveNext();
                    return new ScalarOf<int>(iter.Current);
                },
                3
            );
            Assert.True(
                func.Invoke(true).Value() == 5);
        }

        [Fact]
        public void RepeatsNullsResults()
        {
            var func = new RepeatedFunc<bool, IScalar<int>>(
                input =>
                {
                    return null;
                },
                2
            );

            Assert.Null(func.Invoke(true));
        }

        [Fact]
        public void DoesntRepeatAny()
        {
            Assert.Throws(typeof(ArgumentException),
            () => new RepeatedFunc<bool, IScalar<int>>(
                input =>
                {
                    return new ScalarOf<int>(
                            new Random().Next());
                },
                0
            ).Invoke(true));
        }
    }
}
