﻿// MIT License
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
using System.IO;
using System.IO.Compression;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.IO.Tests
{
    public class UnZippedFileTest
    {
        [Theory]
        [InlineData("File1")]
        [InlineData("File2")]
        [InlineData("File3")]
        public void FindsFile(string fileName)
        {
            Assert.True(
                    new UnzippedFile(
                            new ResourceOf(
                                "Assets/Zip/ZipWithThreeFiles.zip",
                                this.GetType()
                            ),
                            fileName
                    ).Stream() != null
            );
        }

        [Theory]
        [InlineData("File1")]
        [InlineData("File2")]
        [InlineData("File3")]
        public void ReturnsSelectedFile(string fileName)
        {
            Assert.Contains(
                fileName,
                new Text.TextOf(
                    new UnzippedFile(
                       new ResourceOf(
                           "Assets/Zip/ZipWithThreeFiles.zip",
                           this.GetType()
                       ),
                       fileName
                   )
                ).AsString()
            ); 
        }

        [Fact]
        public void ThrowsExcWhenNotAZip()
        {            
            Assert.Throws<InvalidOperationException>(() =>
            {
                new UnzippedFile(
                             new ResourceOf(
                                 "Assets/Zip/NotAZip",
                                 this.GetType()
                             ),
                             "irrelevant"
                     ).Stream();
            });

            
        }
    }
}