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
using System.Globalization;
using System.Linq;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// Use C# formatting syntax: new FormattedText("{0} is {1}", "OOP", "great").AsString() will be "OOP is great"
    /// </summary>
    public sealed class Formatted : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, params IText[] arguments) : this(
            new LiveText(ptn),
            CultureInfo.InvariantCulture,
            () =>
            new Mapped<IText, string>(
                txt => txt.ToString(),
                arguments
            ).ToArray()
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, params object[] arguments) : this(
            new LiveText(ptn), CultureInfo.InvariantCulture, new Live<object[]>(arguments))
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(String ptn, bool live, params object[] arguments) : this(
            new LiveText(ptn), live, CultureInfo.InvariantCulture, arguments)
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(IText ptn, params object[] arguments) : this(
            ptn,
            CultureInfo.InvariantCulture,
            false,
            arguments
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(IText ptn, bool live, params object[] arguments) : this(
            ptn, CultureInfo.InvariantCulture, live, arguments
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern</param>
        /// <param name="local">CultureInfo</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(IText ptn, CultureInfo local, params object[] arguments) : this(
            ptn, local, false, arguments
            )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern</param>
        /// <param name="local">CultureInfo</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(IText ptn, CultureInfo local, bool live, params object[] arguments) : this(
            ptn, local, () => arguments, live
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, CultureInfo locale, params object[] arguments) : this(
            new LiveText(ptn), locale, false, arguments)
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(String ptn, CultureInfo locale, bool live, params object[] arguments) : this(
            new LiveText(ptn), locale, live, arguments)
        { }

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(string ptn, bool live, params IText[] arguments) : this(
            new LiveText(ptn),
            CultureInfo.InvariantCulture,
            () =>
            {
                object[] strings = new object[new LengthOf(arguments).Value()];
                for (int i = 0; i < arguments.Length; i++)
                {
                    strings[i] = arguments[i].ToString();
                }
                return strings;
            },
            live
        )
        { }

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>

        public Formatted(string ptn, CultureInfo locale, params IText[] arguments) : this(
            new LiveText(ptn),
            locale,
            false,
            arguments
        )
        { }

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>

        public Formatted(string ptn, CultureInfo locale, bool live, params IText[] arguments) : this(
            new LiveText(ptn),
            locale,
            () =>
            {
                object[] strings = new object[new LengthOf(arguments).Value()];
                for (int i = 0; i < arguments.Length; i++)
                {
                    strings[i] = arguments[i].ToString();
                }
                return strings;
            },
            live
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(
            IText ptn,
            CultureInfo locale,
            IScalar<object[]> arguments,
            bool live = false
        ) : this(
            ptn,
            locale,
            () => arguments.Value(),
            live
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(
            IText ptn,
            CultureInfo locale,
            Func<object[]> arguments,
            bool live = false
        ) : base(
            () => String.Format(locale, ptn.ToString(), arguments()),
            live
        )
        { }
    }
}
