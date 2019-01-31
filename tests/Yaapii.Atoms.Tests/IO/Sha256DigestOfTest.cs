// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class Sha256DigestOfTest
    {
        [Fact]
        public void ChecksumOfEmptyString()
        {
           Assert.Equal(
                "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                new HexOf(
                    new Sha256DigestOf(new InputOf(string.Empty))
                ).AsString()
           );
        }

        [Fact]
        public void ChecksumOfString()
        {
            Assert.Equal(
                "7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d9069",
                new HexOf(
                    new Sha256DigestOf(new InputOf("Hello World!"))
                ).AsString()
            );
        }

        [Fact]
        public void ChecksumFromFile()
        {
            Assert.Equal(
                "c94451bd1476a3728669de11e22c645906d806e63a95c5797de1f3e84f126a3e",
                new HexOf(
                    new Sha256DigestOf(
                        new ResourceOf(
                            "IO/Resources/digest-calculation.txt",
                            this.GetType()
                        )
                    )
                ).AsString()
            );
        }
    }
}