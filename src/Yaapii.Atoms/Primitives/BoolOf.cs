// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A bool out of text objects.
    /// </summary>
    public sealed class BoolOf : IScalar<Boolean>
    {
        private readonly ScalarOf<bool> bl;

        /// <summary>
        /// <see cref="string"/> as bool
        /// </summary>
        /// <param name="str">source string</param>
        public BoolOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// <see cref="IText"/> as bool
        /// </summary>
        /// <param name="text">source text "true" or "false"</param>
        public BoolOf(IText text)
        {
            this.bl =
                new ScalarOf<bool>(() =>
                {
                    try
                    {
                        return Convert.ToBoolean(text.AsString());
                    }
                    catch (FormatException ex)
                    {
                        throw new IOException(ex.Message, ex);
                    }
                });
        }

        /// <summary>
        /// Bool value
        /// </summary>
        /// <returns>true or false</returns>
        public Boolean Value()
        {
            return this.bl.Value();
        }
    }
}
