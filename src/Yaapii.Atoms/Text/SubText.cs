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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public sealed class SubText : Text.Envelope
    {
        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        /// <param name="text">text to extreact from</param>
        /// <param name="strt">where to start</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public SubText(String text, int strt, bool live = false) : this(new Text.Live(text), strt, live)
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start</param>
        /// <param name="end">where to end</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public SubText(String text, int strt, int end, bool live = false) : this(
            new Text.Live(text), 
            strt, 
            end, 
            live
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public SubText(IText text, int strt, bool live = false) : this(
            text, 
            new ScalarOf<Int32>(strt), 
            new ScalarOf<Int32>(() => text.AsString().Length - strt),
            live
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start</param>
        /// <param name="end">where to end</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public SubText(IText text, int strt, int end, bool live = false) : this(
            text, 
            new ScalarOf<Int32>(strt), 
            new ScalarOf<Int32>(end),
            live
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start encapsulated in a scalar</param>
        /// <param name="len">where to end encapsulated in a scalar</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public SubText(
            IText text,
            ScalarOf<Int32> strt,
            ScalarOf<Int32> len,
            bool live = false
        ) : this(
            text,
            () => strt.Value(),
            () => len.Value(),
            live
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start encapsulated in a scalar</param>
        /// <param name="len">where to end encapsulated in a scalar</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public SubText(IText text, Func<Int32> strt, Func<Int32> len, bool live = false) : base(() =>
            {
                return 
                    text.AsString().Substring(
                        strt(),
                        len()
                    );
            },
            live
        )
        { }
    }
}
