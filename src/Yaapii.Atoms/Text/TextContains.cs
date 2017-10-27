// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
// modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary> Check if a text contains a pattern </summary>
    public sealed class TextContains : IScalar<bool>
    {
        #region Fields

        private readonly IScalar<string> _inputValue;
        private readonly IScalar<string> _pattern;
        private readonly IScalar<StringComparison> _stringComparison;

        #endregion Fields

        #region Constructors

        /// <summary> Checks if a text contains a pattern using strings </summary>
        /// <param name="inputStr"> text as string </param>
        /// <param name="patternStr"> pattern as string </param>
        /// <param name="ignoreCase"> Enables case sensitivity </param>
        public TextContains(string inputStr, string patternStr, bool ignoreCase = false) :
            this(new ScalarOf<string>(inputStr), new ScalarOf<string>(patternStr), new ScalarOf<StringComparison>(() => ignoreCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
        { }

        /// <summary> Checks if a text contains a pattern using IText </summary>
        /// <param name="inputText"> text as IText </param>
        /// <param name="patternText"> pattern as IText </param>
        /// <param name="ignoreCase"> Enables case sensitivity </param>
        public TextContains(IText inputText, IText patternText, bool ignoreCase = false) :
            this(new ScalarOf<string>(() => inputText.AsString()), new ScalarOf<string>(() => patternText.AsString()), new ScalarOf<StringComparison>(() => ignoreCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
        { }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        public TextContains(IScalar<string> inputValue, IScalar<string> pattern) : this(inputValue, pattern, new ScalarOf<StringComparison>(StringComparison.CurrentCulture))
        {
        }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
        public TextContains(IScalar<string> inputValue, IScalar<string> pattern, IScalar<StringComparison> stringComparison)
        {
            _inputValue = inputValue;
            _pattern = pattern;
            _stringComparison = stringComparison;
        }

        #endregion Constructors

        #region Methods

        /// <summary> Returns if the inputValue contains the pattern </summary>
        /// <returns> bool </returns>
        public bool Value()
        {
            return _inputValue.Value().IndexOf(_pattern.Value(), _stringComparison.Value()) >= 0;
        }

        #endregion Methods
    }
}