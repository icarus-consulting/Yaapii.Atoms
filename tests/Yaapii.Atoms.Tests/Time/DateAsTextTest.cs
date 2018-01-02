using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Time.Tests
{
    public sealed class DateAsTextTest
    {
        [Fact]
        public void FormatsCurrentTime()
        {
            Assert.True(
                !String.IsNullOrEmpty(
                    new DateAsText().AsString()
                )
            );
        }

        [Fact]
        public void FormatsWithCustomPattern()
        {
            Assert.True(
                new DateAsText(
                    new DateTime(2017, 12, 13, 14, 15, 16, 17, DateTimeKind.Local),
                    "yyyy-MM-dd HH:mm:ss").AsString() == "2017-12-13 14:15:16");
        }
    }
}
