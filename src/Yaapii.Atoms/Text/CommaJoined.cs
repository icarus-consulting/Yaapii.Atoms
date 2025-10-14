// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Texts joined together seperated by a comma followed by a blank.
    /// </summary>
    public sealed class CommaJoined : TextEnvelope
    {
        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(bool live, params String[] strs) :
            this(
                LiveMany.New(strs),
                live
            )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(IEnumerable<string> strs, bool live = false) :
            this(() => strs,
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(params IText[] txts) : this(false, txts)
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(params String[] strs) : this(false, strs)
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(bool live, params IText[] txts) : this(
            Mapped.New(
                txt => txt.AsString(),
                txts
            ),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(IEnumerable<IText> txts, bool live = false) : this(
            Mapped.New(
                txt => txt.AsString(),
                txts
            ),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(IScalar<IEnumerable<string>> txts, bool live = false) : this(
            () => txts.Value(),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(Func<IEnumerable<string>> txts, bool live = false) : base(() =>
            string.Join(", ", txts()), live
        )
        { }
    }
}
