using System.Reflection;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public class ResourceOfTest
    {
        [Fact]
        public void FindsResourceInAssembly()
        {
            Assert.Equal(
                "Hello from Embedded!",
                new TextOf(
                    new ResourceOf("IO/Resources/test.txt", Assembly.GetExecutingAssembly())
                ).AsString()
            );
        }

        [Fact]
        public void FindsResourceByType()
        {
            Assert.Equal(
                "Hello from Embedded!",
                new TextOf(
                    new ResourceOf("IO/Resources/test.txt", this.GetType())
                ).AsString()
            );
        }

        [Theory]
        [InlineData("IO/Resources/A.2/test.txt")]
        [InlineData("IO/Resources/A._2/test.txt")]
        [InlineData("IO/Resources/1/test.txt")]
        [InlineData("IO/Resources/_1/test.txt")]
        [InlineData("IO/Resources/-/test.txt")]
        [InlineData("IO/Resources/__/test.txt")]
        [InlineData("IO/Resources/-A/test.txt")]
        [InlineData("IO/Resources/_A/test.txt")]
        [InlineData("IO/Resources/A-B/test.txt")]
        [InlineData("IO/Resources/A_B/test.txt")]
        public void FindsResourceWithSpecialCharacters(string name)
        {
            Assert.Equal(
                "Hello from Embedded!",
                new TextOf(
                    new ResourceOf(name, this.GetType())
                ).AsString()
            );
        }
    }
}