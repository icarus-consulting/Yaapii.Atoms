// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
                new LiveText(
                    new ResourceOf("IO/Resources/test.txt", Assembly.GetExecutingAssembly())
                ).AsString()
            );
        }

        [Fact]
        public void FindsResourceByType()
        {
            Assert.Equal(
                "Hello from Embedded!",
                new LiveText(
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
                new LiveText(
                    new ResourceOf(
                        name,
                        this.GetType())
                ).AsString()
            );
        }
    }
}