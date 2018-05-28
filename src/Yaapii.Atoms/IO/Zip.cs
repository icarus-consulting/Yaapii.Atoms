// MIT License
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

using System.IO;
using System.IO.Compression;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;

// <summary>
/// Zip all Files in an Directory
/// 
/// </summary>
public sealed class Zip : IInput
{

    private readonly System.IO.DirectoryInfo origin;

    /// <summary>
    /// Zip all Files in an Directory
    /// not recursive
    /// </summary>
    /// <param name="origin"> the directory with the files to zip</param>
    public Zip(System.IO.DirectoryInfo origin)
    {
        this.origin = origin;
    }

    /// <summary>
    /// The Zipped Files as a Stream
    /// </summary>
    /// <returns></returns>
    public Stream Stream()
    {
        MemoryStream memory = new MemoryStream();
        using (var zip = new ZipArchive(memory, System.IO.Compression.ZipArchiveMode.Create, true))
        {
            foreach (var file in origin.GetFiles())
            {
                var entry = zip.CreateEntry(file.FullName);

                new LengthOf(
                    new TeeInput(
                        new InputOf(file),
                        new OutputTo(entry.Open())
                    )
                ).Value();
            }
        }
        return memory;
    }
}