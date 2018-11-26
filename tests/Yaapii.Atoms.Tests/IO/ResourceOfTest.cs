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
        [InlineData("IO/Resources/-A-/test.txt")]
        [InlineData("IO/Resources/_A_/test.txt")]
        [InlineData("IO/Resources/-A-/-/test.txt")]
        [InlineData("IO/Resources/-A-/__/test.txt")]
        [InlineData("IO/Resources/A-B/test.txt")]
        [InlineData("IO/Resources/A_B/test.txt")]
        [InlineData("IO/Resources/{id}/test.txt")]
        [InlineData("IO/Resources/_id_/test.txt")]
        [InlineData("IO/Resources/{id}/{/test.txt")]
        [InlineData("IO/Resources/{id}/__/test.txt")]
        [InlineData("IO/Resources/[a]/test.txt")]
        [InlineData("IO/Resources/[a]/[/test.txt")]
        [InlineData("IO/Resources/[a]/__/test.txt")]
        [InlineData("IO/Resources/!B!/test.txt")]
        [InlineData("IO/Resources/_B_/test.txt")]
        [InlineData("IO/Resources/!B!/!/test.txt")]
        [InlineData("IO/Resources/!B!/__/test.txt")]
        [InlineData("IO/Resources/§C§/test.txt")]
        [InlineData("IO/Resources/_C_/test.txt")]
        [InlineData("IO/Resources/§C§/§/test.txt")]
        [InlineData("IO/Resources/§C§/__/test.txt")]
        [InlineData("IO/Resources/$D$/test.txt")]
        [InlineData("IO/Resources/_D_/test.txt")]
        [InlineData("IO/Resources/$D$/$/test.txt")]
        [InlineData("IO/Resources/$D$/__/test.txt")]
        [InlineData("IO/Resources/=E=/test.txt")]
        [InlineData("IO/Resources/_E_/test.txt")]
        [InlineData("IO/Resources/=E=/=/test.txt")]
        [InlineData("IO/Resources/=E=/__/test.txt")]
        [InlineData("IO/Resources/+F+/test.txt")]
        [InlineData("IO/Resources/_F_/test.txt")]
        [InlineData("IO/Resources/+F+/+/test.txt")]
        [InlineData("IO/Resources/+F+/__/test.txt")]
        [InlineData("IO/Resources/;G;/test.txt")]
        [InlineData("IO/Resources/_G_/test.txt")]
        [InlineData("IO/Resources/;G;/;/test.txt")]
        [InlineData("IO/Resources/;G;/__/test.txt")]
        [InlineData("IO/Resources/,H,/test.txt")]
        [InlineData("IO/Resources/_H_/test.txt")]
        [InlineData("IO/Resources/,H,/,/test.txt")]
        [InlineData("IO/Resources/,H,/__/test.txt")]
        [InlineData("IO/Resources/`I`/test.txt")]
        [InlineData("IO/Resources/_I_/test.txt")]
        [InlineData("IO/Resources/`I`/`/test.txt")]
        [InlineData("IO/Resources/`I`/__/test.txt")]
        [InlineData("IO/Resources/´J´/test.txt")]
        [InlineData("IO/Resources/_J_/test.txt")]
        [InlineData("IO/Resources/´J´/´/test.txt")]
        [InlineData("IO/Resources/´J´/__/test.txt")]
        [InlineData("IO/Resources/'K'/test.txt")]
        [InlineData("IO/Resources/_K_/test.txt")]
        [InlineData("IO/Resources/'K'/'/test.txt")]
        [InlineData("IO/Resources/'K'/__/test.txt")]
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