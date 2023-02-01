// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> repeated multiple times.
    /// </summary>
    public sealed class Repeated : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/>  repeated multiple times.
        /// </summary>
        /// <param name="text">text to repeat</param>
        /// <param name="count">how often to repeat</param>
        public Repeated(String text, int count) : this(
            new LiveText(text),
            count
        )
        { }

        /// <summary>
        /// A <see cref="IText"/>  repeated multiple times.
        /// </summary>
        /// <param name="text">text to repeat</param>
        /// <param name="count">how often to repeat</param>
        public Repeated(IText text, int count) : base(() =>
            {
                StringBuilder output = new StringBuilder();
                for (int cnt = 0; cnt < count; ++cnt)
                {
                    output.Append(text.AsString());
                }
                return output.ToString();
            },
            false
        )
        { }
    }
}
