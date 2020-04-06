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

#pragma warning disable MaxClassLength // Class length max
namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// A <see cref="IText"/> envelope.
    /// The envelope can work in live or in sticky mode.
    /// </summary>
    public abstract class TextEnvelope : IText
    {
        private readonly Func<string> origin;
        private readonly ScalarOf<string> fixedOrigin;
        private readonly bool live;

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="text">Origin text</param>
        /// <param name="live">should the value be created every time the object is used?</param>
        public TextEnvelope(IText text, bool live) : this(() => text.AsString(), live)
        { }

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="origin">How to create the value</param>
        /// <param name="live">should the value be created every time the object is used?</param>
        public TextEnvelope(Func<string> origin, bool live)
        {
            this.origin = origin;
            this.live = live;
            this.fixedOrigin = new ScalarOf<string>(() => origin());
        }

        /// <summary>
        /// Gives the text as a string.
        /// </summary>
        /// <returns></returns>
        public String AsString()
        {
            var result = string.Empty;
            if (this.live)
            {
                result = this.origin();
            }
            else
            {
                result = this.fixedOrigin.Value();
            }
            return result;
        }
    }
}