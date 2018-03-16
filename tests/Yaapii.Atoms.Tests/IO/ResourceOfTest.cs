using System.Reflection;
using Xunit;
using Yaapii.Atoms.IO;
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
    }
}