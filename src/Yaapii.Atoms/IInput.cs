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

using System;
using System.IO;

namespace Yaapii.Atoms
{
    /// <summary>
    /// Input.
    ///
    /// <para>Here is for example how <see cref="IInput"/>  can be used
    /// in order to read the content of a text file:</para>
    ///
    /// <code>String content = new BytesAsText(
    /// new InputAsBytes(
    ///    new InputOf(new Uri("file:///C:/tmp/names.txt")
    ///   )
    /// ).asString();</code>
    ///
    /// <para>Here <see cref="Atoms.IO.InputOf"/> 
    /// implements <see cref="Atoms.IInput"/> and behaves like
    /// one, providing read-only access to the encapsulated <see cref="Uri"/> pointing to a file.</para>
    /// </summary>
    public interface IInput
    {
        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        Stream Stream();
    }
}
