using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    /**
     * Test case for {@link RetryScalar}.
     *
     * @author Yegor Bugayenko (yegor256@gmail.com)
     * @version $Id$
     * @since 0.9
     * @checkstyle JavadocMethodCheck (500 lines)
     */
    public sealed class RetryScalarTest
    {
        [Fact]
        public void RunsScalarMultipleTimes()
        {
            Assert.True(
                new RetryScalar<int>(
                    () =>
                    {
                        if (new Random().NextDouble() > 0.3d)
                        {
                            throw new ArgumentException("May happen");
                        }
                        return 0;
                    },
                int.MaxValue
            ).Value() == 0);
        }

    }
}
