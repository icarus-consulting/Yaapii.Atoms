using System;
using Xunit;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailOnExceptionTests
    {
        [Fact]
        public void ThrowsExpectedException()
        {
            Assert.Throws<InvalidOperationException>(
              () => new FailOnException<ArgumentException>(
                    () => throw new InvalidOperationException(),
                    new ArgumentException()
                ).Go()
            );
        }

        [Fact]
        public void ThrowsOriginalException()
        {
            Assert.Throws<ArgumentException>(
              () => new FailOnException<InvalidOperationException>(
                    () => throw new ArgumentException(),
                    new InvalidOperationException()
                ).Go()
            );
        }
    }
}
