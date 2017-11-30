using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class BiFuncWithFallbackTest
    {
        [Fact]
        public void UsesMainFunc()
        {
            Assert.True(
                new BiFuncWithFallback<bool, bool, string>(
                    (in1, in2) => "It's success",
                    ex => "In case of failure..."
                ).Apply(true, true).Contains("success"),
                "cannot use main function");
        }

        [Fact]
        public void UsesFallback()
        {
            Assert.True(
                new BiFuncWithFallback<bool, bool, string>(
                    (in1, in2) =>
                    {
                        throw new Exception("Failure");
                    },
                    ex => "Never mind"
                ).Apply(true, true) == "Never mind"
            );
        }

        [Fact]
        public void UsesFollowUp()
        {
            Assert.True(
            new BiFuncWithFallback<bool, bool, string>(
                (in1, in2) => "works fine",
                ex => "won't happen",
                input => "follow up"
            ).Apply(true, true) == "follow up");
        }
    }
}
