﻿// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class TeeInputTest
    {
        [Fact]
        public void CopiesContent()
        {
            var baos = new MemoryStream();
            String content = "Hello, товарищ!";
            Assert.True(
                new TextOf(
                    new TeeInput(
                        new InputOf(content),
                        new OutputTo(baos)
                    )
                ).AsString() == Encoding.UTF8.GetString(baos.ToArray()),
                "Can't copy Input to Output and return Input");
        }

        [Fact]
        public void CopiesToFile()
        {
            var dir = "artifacts/TeeInputTest"; var file = "txt.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);


            var str = "";
                str = new TextOf(
                    new BytesOf(
                        new TeeInput(
                            "Hello, друг!", 
                            new OutputTo(new Uri(path))))
                ).AsString();

            Assert.True(
                str == new TextOf(new InputOf(new Uri(path))).AsString(),
                "Can't copy Input to File and return content");
        }
    }
}
