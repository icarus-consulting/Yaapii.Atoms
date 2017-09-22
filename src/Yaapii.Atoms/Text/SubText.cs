﻿/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public sealed class SubText : IText
    {
        private readonly IText _origin;
        private readonly UncheckedScalar<Int32> _start;
        private readonly UncheckedScalar<Int32> _end;

        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        /// <param name="text">text to extreact from</param>
        /// <param name="strt">where to start</param>
        public SubText(String text, int strt) : this(new TextOf(text), strt)
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start</param>
        /// <param name="end">where to end</param>
        public SubText(String text, int strt, int end) : this(new TextOf(text), strt, end)
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start</param>
        public SubText(IText text, int strt) : this(text, new ScalarOf<Int32>(strt), new ScalarOf<Int32>(() => text.AsString().Length - strt))
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start</param>
        /// <param name="end">where to end</param>
        public SubText(IText text, int strt, int end) : this(text, new ScalarOf<Int32>(strt), new ScalarOf<Int32>(end))
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start encapsulated in a scalar</param>
        /// <param name="end">where to end encapsulated in a scalar</param>
        public SubText(IText text, IScalar<Int32> strt, IScalar<Int32> end) : this(text, new UncheckedScalar<Int32>(strt), new UncheckedScalar<Int32>(end))
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        /// <param name="text">text to extract from</param>
        /// <param name="strt">where to start encapsulated in a scalar</param>
        /// <param name="end">where to end encapsulated in a scalar</param>
        public SubText(IText text, UncheckedScalar<Int32> strt,
            UncheckedScalar<Int32> end)
        {
            this._origin = text;
            this._start = strt;
            this._end = end;
        }

        public String AsString()
        {
            return this._origin.AsString().Substring(
                this._start.Value(),
                this._end.Value()
            );
        }

        public int CompareTo(IText text)
        {
            return new UncheckedText(this).CompareTo(text);
        }

        public bool Equals(IText text)
        {
            return this.CompareTo(text) == 0;
        }
    }
}
