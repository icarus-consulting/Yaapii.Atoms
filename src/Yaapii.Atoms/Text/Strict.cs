// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    public sealed class Strict : TextEnvelope
    {
        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, params string[] valid) : this(
            candidate,
            new LiveMany<string>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, IEnumerable<string> valid) : this(
            candidate,
            true,
            valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, bool ignoreCase, params string[] valid) : this(
            candidate, ignoreCase, new ManyOf<string>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, bool ignoreCase, IEnumerable<string> valid) : this(
            new LiveText(candidate), ignoreCase, new Mapped<string, IText>((str) => new LiveText(str), valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, params string[] valid) : this(
            candidate, true, valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, bool ignoreCase, params string[] valid) : this(
            candidate,
            ignoreCase,
            new Mapped<string, IText>((str) => new LiveText(str), valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, params IText[] valid) : this(candidate, true, valid)
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, bool ignoreCase, params IText[] valid) : this(
            candidate,
            ignoreCase,
            new LiveMany<IText>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, IEnumerable<IText> valid) : this(
            candidate,
            true,
            valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, bool ignoreCase, IEnumerable<IText> valid) : this(
            candidate,
            valid,
            new ScalarOf<StringComparison>(() => ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        /// <param name="stringComparer">Ignore case in the canidate and valid texts</param>
        private Strict(IText candidate, IEnumerable<IText> valid, IScalar<StringComparison> stringComparer) : base(() =>
        {
            var result = false;
            var str = candidate.AsString();
            foreach (var txt in valid)
            {
                if (txt.AsString().Equals(str, stringComparer.Value()))
                {
                    result = true;
                    break;
                }
            }
            if (!result)
            {
                throw new ArgumentException($"'{str}' is not valid here - expected: {new Joined(", ", valid).AsString()}");
            }
            return str;
        }, false)
        { }
    }
}
