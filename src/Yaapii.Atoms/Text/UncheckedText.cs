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

using System;
using System.IO;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    public sealed class UncheckedText : IText
    {
        private readonly Atoms.IText _text;
        private readonly IFunc<IOException, String> _fallback;

        /// <summary>
        /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="text">text to uncheck</param>
        public UncheckedText(String text) : this(new TextOf(text))
        { }

        /// <summary>
        /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="text">text to uncheck</param>
        public UncheckedText(IText text) : this(
                text,
                new FuncOf<IOException, string>(
                    (error) =>
                    {
                        throw new UncheckedIOException(error);
                    }))
        { }

        /// <summary>
        /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="txt">text to uncheck</param>
        /// <param name="fbk">fallback function</param>
        public UncheckedText(IText txt, IFunc<IOException, String> fbk)
        {
            _text = txt;
            _fallback = fbk;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            String txt;
            try
            {
                txt = _text.AsString();
            }
            catch (IOException ex)
            {
                txt = new UncheckedFunc<IOException, String>(_fallback).Invoke(ex);
            }
            return txt;
        }

        /// <summary>
        /// Compare to other text.
        /// </summary>
        /// <param name="text">text to compare to</param>
        /// <returns>-1 if this is lower, 0 if equal, 1 if this is higher</returns>
        public int CompareTo(Atoms.IText text)
        {
            return this.AsString().CompareTo(
                new UncheckedText(text).AsString()
            );
        }

        /// <summary>
        /// Check for equality.
        /// </summary>
        /// <param name="other">other object to compare to</param>
        /// <returns>true if equal.</returns>
        public bool Equals(Atoms.IText other)
        {
            return other.AsString().Equals(this.AsString());
        }
    }
}
