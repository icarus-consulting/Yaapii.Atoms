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
        [InlineData("IO/Resources/1/A.2/test.txt")]
        [InlineData("IO/Resources/1/A._2/test.txt")]
        [InlineData("IO/Resources/1/test.txt")]
        [InlineData("IO/Resources/_1/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/test.txt")]
        [InlineData("IO/Resources/1/A.2/_A_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/-/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/_id_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/{/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/_a_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/[/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/_B_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/!/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/_C_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/§/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/_D_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/$/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/_E_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/=/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/_F_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/+/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/_G_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/;/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/_H_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/,/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/_I_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/`/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/´J´/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/_J_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/´J´/´/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/´J´/__/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/´J´/'K'/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/´J´/_K_/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/´J´/'K'/'/test.txt")]
        [InlineData("IO/Resources/1/A.2/-A-/{id}/[a]/!B!/§C§/$D$/=E=/+F+/;G;/,H,/`I`/´J´/'K'/__/test.txt")]
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