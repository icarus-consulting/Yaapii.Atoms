using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Time.Tests
{
    public sealed class DateOfTest
    {
        [Fact]
        public void CanParseIsoString()
        {
            Assert.True(
                new DateOf("2017-12-13T14:15:16.0170000+0:00").Value().ToUniversalTime() ==
                new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Utc).ToUniversalTime());
        }

        [Fact]
        public void CanParseCustomFormat()
        {
            Assert.True(
                new DateOf(
                    "2017-12-13 14:15:16.0170000", 
                    "yyyy-MM-dd HH:mm:ss.fffffff").Value().ToUniversalTime() ==
                new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Utc).ToUniversalTime());
        }

    }
}
