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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public sealed class SubText : TextEnvelope
    {
        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        public SubText(String text, int start) : this(new LiveText(text), start)
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        public SubText(String text, int start, int length) : this(
            new LiveText(text),
            start,
            length
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, int start) : this(
            text,
            new Live<Int32>(start),
            new Live<Int32>(() => text.AsString().Length - start)
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, int start, int length) : this(
            text,
            new Live<Int32>(start),
            new Live<Int32>(length)
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(
            IText text,
            Live<Int32> start,
            Live<Int32> length,
            bool live = false
        ) : this(
            text,
            () => start.Value(),
            () => length.Value()
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, Func<Int32> start, Func<Int32> length) : base(() =>
            {
                return
                    text.AsString().Substring(
                        start(),
                        length()
                    );
            },
            false
        )
        { }
    }
}
