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
using System.Text.RegularExpressions;

namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// Normalized A <see cref="IText"/> (whitespaces replaced with one single space)
    /// </summary>
    public sealed class Normalized : Text.Envelope
    {
        /// <summary>
        /// Normalized A <see cref="IText"/>  (whitespaces replaced with one single space)
        /// </summary>
        /// <param name="text">text to normalize</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Normalized(String text, bool live = false) : this(new Text.Live(text), live)
        { }

        /// <summary>
        /// Normalized A <see cref="IText"/>  (whitespaces replaced with one single space)
        /// </summary>
        /// <param name="text">text to normalize</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Normalized(IText text, bool live = false) : base(() =>
            Regex.Replace(new Trimmed(text).AsString(), "\\s+", " "),
            live
        )
        { }
    }
}
