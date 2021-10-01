// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Text
{
    /// <summary> Check if a text contains a pattern </summary>
    public sealed class Contains : IScalar<bool>
    {
        private readonly ScalarOf<bool> result;

        /// <summary> Checks if a text contains a pattern using strings </summary>
        /// <param name="inputStr"> text as string </param>
        /// <param name="patternStr"> pattern as string </param>
        /// <param name="ignoreCase"> Enables case sensitivity </param>
        public Contains(string inputStr, string patternStr, bool ignoreCase = false) :
            this(new Live<string>(inputStr), new Live<string>(patternStr), new Live<StringComparison>(() => ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture))
        { }

        /// <summary> Checks if a text contains a pattern using IText </summary>
        /// <param name="inputText"> text as IText </param>
        /// <param name="patternText"> pattern as IText </param>
        /// <param name="ignoreCase"> Enables case sensitivity </param>
        public Contains(IText inputText, IText patternText, bool ignoreCase = false) :
            this(new Live<string>(() => inputText.AsString()), new Live<string>(() => patternText.AsString()), new Live<StringComparison>(() => ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture))
        { }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        public Contains(IScalar<string> inputValue, IScalar<string> pattern)
            : this(inputValue, pattern, new Live<StringComparison>(StringComparison.CurrentCulture))
        {
        }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
        public Contains(IScalar<string> inputValue, IScalar<string> pattern, IScalar<StringComparison> stringComparison) : this(
            () => inputValue.Value(),
            () => pattern.Value(),
            () => stringComparison.Value()
        )
        { }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
        public Contains(Func<string> inputValue, Func<string> pattern, Func<StringComparison> stringComparison)
        {
            this.result =
                new ScalarOf<bool>(() => inputValue().IndexOf(pattern(), stringComparison()) >= 0);
        }

        /// <summary> Returns if the inputValue contains the pattern </summary>
        /// <returns> bool </returns>
        public bool Value()
        {
            return this.result.Value();
        }
    }
}
