using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public class NoNullsCallableTest
    {
        [Fact]
        public void RaisesError()
        {
            Assert.Throws<IOException>(
                () =>
                new NoNullsCallable<string>(
                    () => null
                ).Call());
        }

        [Fact]
        public void RaisesGivenError()
        {
            Assert.Throws<IOException>(() =>
           new NoNullsCallable<string>(
               () => null,
               new IOException("got NULL")
           ).Call());
        }

        [Fact]
        public void ReturnsFallback()
        {
            var fbk = "wtf! its null!";

            Assert.True(
                new NoNullsCallable<string>(
                    () => null,
                    fbk
                ).Call() == fbk);
        }
    }
}
