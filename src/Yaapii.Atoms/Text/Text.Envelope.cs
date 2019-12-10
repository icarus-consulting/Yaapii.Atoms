﻿// MIT License
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

#pragma warning disable MaxClassLength // Class length max
namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// A Text
    /// </summary>
    partial class Text
    {
        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        public abstract class Envelope : IText
        {
            private readonly Func<String> origin;
            private readonly Lazy<String> fixedOrigin;
            private readonly bool live;

            /// <summary>
            /// A <see cref="IText"/> envelope.
            /// The envelope can work in live or in sticky mode.
            /// </summary>
            /// <param name="origin">How to create the value</param>
            /// <param name="live">should the value be created every time the object is used?</param>
            public Envelope(Func<IText> origin, bool live = false) : this(() => origin().AsString(), live)
            { }

            /// <summary>
            /// A <see cref="IText"/> envelope.
            /// The envelope can work in live or in sticky mode.
            /// </summary>
            /// <param name="origin">How to create the value</param>
            /// <param name="live">should the value be created every time the object is used?</param>
            public Envelope(Func<string> origin, bool live = false)
            {
                this.origin = origin;
                this.live = live;
                this.fixedOrigin = new Lazy<string>(origin);
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
                    result = this.fixedOrigin.Value;
                }
                else
                {
                    result = this.origin();
                }
                return result;
            }
        }
    }
}