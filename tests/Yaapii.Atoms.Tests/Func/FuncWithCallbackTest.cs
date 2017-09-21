using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class FuncWithCallbackTest
    {
        [Fact]
        public void UsesMainFunc()
        {
            Assert.True(
            new FuncWithFallback<bool, string>(
                input => "It's success",
                ex => "In case of failure..."
            ).Invoke(true).Contains("success"),
            "cannot use main function");
        }

        [Fact]
        public void UsesCallback()
        {
            Assert.True(
                new FuncWithFallback<bool, string>(
                    input =>
                    {
                        throw new Exception("Failure");
                    },
                    ex => "Never mind"
                ).Invoke(true) == "Never mind"
            );
        }

        [Fact]
        public void UsesFollowUp()
        {
            Assert.True(
            new FuncWithFallback<bool, string>(
                input => "works fine",
                ex => "won't happen",
                input => "follow up"
            ).Invoke(true) == "follow up");
    }
}
}
