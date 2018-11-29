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
        [InlineData("IO/Resources/1/A.2/-A-/-/{id}/{/[a]/[/!B!/!/§C§/§/$D$/$/=E=/=/+F+/+/;G;/;/,H,/,/`I`/`/´J´/´/'K'/'/test.txt")]
        [InlineData("IO/Resources/_1/A._2/_A_/__/_id_/__/_a_/__/_B_/__/_C_/__/_D_/__/_E_/__/_F_/__/_G_/__/_H_/__/_I_/__/_J_/__/_K_/__/test.txt")]
        public void FindsResourceWithSpecialCharactersNew(string name)
        {
            Assert.Equal(
                "Hello from Embedded!",
                new TextOf(
                    new ResourceOf(
                        name,
                        this.GetType())
                ).AsString()
            );
        }
    }
}