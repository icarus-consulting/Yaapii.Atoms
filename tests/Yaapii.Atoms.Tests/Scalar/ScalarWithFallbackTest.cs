using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Scalar.Tests
{
    public class ScalarWithFallbackTest
    {
        [Fact]
        public void GivesFallback()
        {
            var fbk = "strong string";

            Assert.True(
                new ScalarWithFallback<string>(
                    new ScalarOf<string>(
                        () => throw new Exception("NO STRINGS ATTACHED HAHAHA")),
                    fbk
                    ).Value() == fbk);
        }

        [Fact]
        public void GivesFallbackByFunc()
        {
            var fbk = "strong string";

            Assert.True(
                new ScalarWithFallback<string>(
                    new ScalarOf<string>(
                        () => throw new Exception("NO STRINGS ATTACHED HAHAHA")),
                    () => fbk
                    ).Value() == fbk);
        }

        [Fact]
        public void InjectsException()
        {
            var notAmused = new Exception("All is broken :(");

            Assert.True(
                new ScalarWithFallback<string>(
                    new ScalarOf<string>(
                        () => throw notAmused),
                    (ex) => ex.Message).Value() == notAmused.Message);
        }
    }
}
