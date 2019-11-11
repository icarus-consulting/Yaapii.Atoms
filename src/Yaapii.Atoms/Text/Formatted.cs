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
using System.Globalization;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// Use C# formatting syntax: new FormattedText("{0} is {1}", "OOP", "great").AsString() will be "OOP is great"
    /// </summary>
    public sealed class Formatted : IText
    {
        private readonly IText _pattern;
        private readonly IScalar<object[]> _args;
        private readonly CultureInfo _locale;


        
        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, params object[] arguments) : this(
            new TextOf(ptn), CultureInfo.InvariantCulture, new ScalarOf<object[]>(arguments))
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(IText ptn, params object[] arguments) : this(
            ptn, CultureInfo.InvariantCulture, new ScalarOf<object[]>(arguments)
            )
        { }


        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern</param>
        /// <param name="local">CultureInfo</param>
        /// <param name="arguments">arguments to apply</param>

        public Formatted(IText ptn, CultureInfo local, params object[] arguments) : this(
            ptn, local, new ScalarOf<object[]>(arguments)
            )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, CultureInfo locale, params object[] arguments) : this(
            new TextOf(ptn), locale, new ScalarOf<object[]>(arguments))
        { }
       
        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
        public Formatted(string ptn, params IText[] arguments) : this(new TextOf(ptn), CultureInfo.InvariantCulture, new ScalarOf<object[]>(
           () =>
           {
               object[] strings = new object[new LengthOf(arguments).Value()];
               for (int i = 0; i < arguments.Length; i++)
               {
                   strings[i] = arguments[i].AsString();
               }
               return strings;
           }))
        { }
        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>

        public Formatted(string ptn, CultureInfo locale, params IText[] arguments) : this(new TextOf(ptn), locale, new ScalarOf<object[]>(
            () =>
            {
                object[] strings = new object[new LengthOf(arguments).Value()];
                for (int i = 0; i < arguments.Length; i++)
                {
                    strings[i] = arguments[i].AsString();
                }
                return strings;
            }))
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(
            IText ptn,
            CultureInfo locale,
            IScalar<object[]> arguments
        )
        {
            this._pattern = ptn;
            this._locale = locale;
            this._args = arguments;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            return String.Format(this._locale, _pattern.AsString(), _args.Value());
        }
    }
}
