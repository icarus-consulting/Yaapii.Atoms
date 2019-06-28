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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Tests;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class DirectoryOfTests
    {
        [Fact]
        public void EnumeratesFiles()
        {
            var file1 = Path.GetFullPath("assets/directoryof/subdir/garbage.txt");
            var file2 = Path.GetFullPath("assets/directoryof/subdir/rubbish.txt");

            new Tidy(() =>
            {
                var dir = Path.GetDirectoryName(file1);
                Directory.CreateDirectory(dir);

                File.Create(file1).Close();
                File.Create(file2).Close();

                Assert.True(
                    new Contains<string>(
                        new DirectoryOf(Path.GetFullPath(dir)),
                        file1
                    ).Value() &&
                    new Contains<string>(
                        new DirectoryOf(Path.GetFullPath(dir)),
                        file2
                    ).Value()
                );

            },
            new Uri(file1), new Uri(file2)).Invoke();
        }

        [Fact]
        public void EnumeratesDirectories()
        {
            var dir = Path.GetFullPath("/assets/directoryof/dir");
            var subdir = Path.GetFullPath("/assets/directoryof/dir/fancy-subdir");

            Directory.CreateDirectory(dir);
            Directory.CreateDirectory(subdir);

            Assert.True(
                new Contains<string>(
                    new DirectoryOf(dir),
                    subdir
                ).Value()
            );
        }

        [Fact]
        public void RejectsNonDirectory()
        {
            var file = Path.GetFullPath("assets/directoryof/subdir/garbage.txt");

            new Tidy(() =>
                {
                    var dir = Path.GetDirectoryName(file);
                    Directory.CreateDirectory(dir);

                    File.Create(file).Close();

                    Assert.Throws<ArgumentException>(() =>
                        new DirectoryOf(file).GetEnumerator()
                    );

                },
                new Uri(Path.GetFullPath(file))
            ).Invoke();
        }

        [Fact]
        public void EnumeratesFilesInSubDirectories()
        {
            var dir = Path.GetFullPath("assets/directoryof/dir");
            var subdir = Path.GetFullPath("assets/directoryof/dir/subdir/subdir2/subdir3/");
            var file = Path.GetFullPath("assets/directoryof/dir/subdir/subdir2/subdir3/test.txt");

            new Tidy(() =>
                {
                    Directory.CreateDirectory(subdir);
                    File.Create(file).Close();

                    var test = new DirectoryOf(dir, true);

                    Assert.True(
                        new Contains<string>(
                            new DirectoryOf(dir, true),
                            file
                        ).Value()
                    );
                },
                new Uri(file)
            ).Invoke();
        }
    }
}
