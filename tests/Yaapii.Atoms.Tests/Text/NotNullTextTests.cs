using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class NotNullTextTests
    {
		[Fact]
		public void NotNull()
		{
			IText s = null;
			Assert.Throws<IOException>(
				()=>new NotNullText(s).AsString()
			);
		}
    }
}
