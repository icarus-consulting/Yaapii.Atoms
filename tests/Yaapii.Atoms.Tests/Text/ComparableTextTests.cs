using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class ComparableTextTests
    {
		[Fact]
		public void Compares()
		{
			Assert.True(
				new ComparableText(
					new TextOf("Hallo Welt")
				).CompareTo(
					new TextOf("Tschüss Welt")
				) <= 0 
			);			
		}

		[Fact]
		public void Equal()
		{
			Assert.True(
				new ComparableText(
					new TextOf("Timm")
				).Equals(
					new TextOf("Jan-Peter")
				) == false
			);
		}

		[Fact]
		public void AsString()
		{
			Assert.True(
				new ComparableText(
					new TextOf("Timm")
				).AsString()
				== "Timm"
			);
		}
    }
}
