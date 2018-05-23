using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public class NotNullTextTest
    {
        [Fact]
        public void RejectsNullText()
        {
            IText nullText = null;
            var text = new NotNullText(nullText);
            Assert.Throws<IOException>(
                () => text.AsString()
            );
        }
    }
}
