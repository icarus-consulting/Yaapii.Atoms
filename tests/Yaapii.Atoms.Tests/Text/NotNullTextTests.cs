using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public class NotNullTextTests
    {
        [Fact]
        public void TextIsNull()
        {
            IText test = null;
            Assert.Throws<IOException>(() =>
                new NotNullText(
                    test
                ).AsString()
            );
        }
    }
}
