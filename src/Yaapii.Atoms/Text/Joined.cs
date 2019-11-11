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
using System.Collections.Generic;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// A <see cref="IText"/> of texts joined together.
    /// </summary>
    public sealed class Joined : IText
    {
        private readonly IScalar<IEnumerable<IText>> _texts;
        private readonly IText _delimiter;

        /// <summary>
        /// Joins A <see cref="IText"/>s together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public Joined(String delimit, params String[] strs) :
            this(
                delimit,
                new Many.Of<string>(strs)
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public Joined(String delimit, IEnumerable<String> strs) :
            this(
                new TextOf(delimit),
                    new Enumerable.Mapped<string, IText>(
                        new FuncOf<string, IText>(
                            (text) => new TextOf(text)
                        ),
                        strs
                    )
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(IText delimit, params IText[] txts) : this(delimit, new Many.Of<IText>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(String delimit, params IText[] txts) : this(new TextOf(delimit), txts)
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(String delimit, IEnumerable<IText> txts) : this(new TextOf(delimit), new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(String delimit, System.Func<IEnumerable<IText>> txts) : this(new TextOf(delimit), new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(IText delimit, System.Func<IEnumerable<IText>> txts) : this(delimit, new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(IText delimit, IEnumerable<IText> txts) : this(delimit, new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">scalars of texts to join</param>
        private Joined(IText delimit, IScalar<IEnumerable<IText>> txts)
        {
            this._delimiter = delimit;
            this._texts = txts;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            return String.Join(
                this._delimiter.AsString(), 
                new Enumerable.Mapped<IText, string>(
                    text => text.AsString(),
                    this._texts.Value()
                    ));
        }
    }
}
