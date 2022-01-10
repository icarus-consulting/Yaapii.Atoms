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

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A <see cref="char"/> out of other objects.
    /// </summary>
    public sealed class CharOf : IScalar<char>
    {
        private readonly IScalar<char> _converter;

        /// <summary>
        /// Converts the value of the specified 32-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="integer">The 32-bit signed integer to convert.</param>
        public CharOf(int integer) :
            this(new Live<char>(() => Convert.ToChar(integer)))
        { }

        /// <summary>
        /// Converts the value of the specified 32-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="uInteger">The 32-bit unsigned integer to convert.</param>
        public CharOf(uint uInteger) :
            this(new Live<char>(() => Convert.ToChar(uInteger)))
        { }

        /// <summary>
        /// Converts the value of the specified 16-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="shrt">The 16-bit signed integer to convert.</param>
        public CharOf(short shrt) :
            this(new Live<char>(() => Convert.ToChar(shrt)))
        { }

        /// <summary>
        /// Converts the value of the specified 16-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="uShort">The 16-bit unsigned integer to convert.</param>
        public CharOf(ushort uShort) :
            this(new Live<char>(() => Convert.ToChar(uShort)))
        { }

        /// <summary>
        /// Converts the value of the specified 64-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="lng">The 64-bit signed integer to convert.</param>
        public CharOf(long lng) :
            this(new Live<char>(() => Convert.ToChar(lng)))
        { }

        /// <summary>
        /// Converts the value of the specified 64-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="ulng">The 64-bit unsigned integer to convert.</param>
        public CharOf(ulong ulng) :
            this(new Live<char>(() => Convert.ToChar(ulng)))
        { }

        /// <summary>
        /// Converts the value of the specified double-precision floating-point number to an equivalent Unicode character.
        /// </summary>
        /// <param name="dbl">The double-precision floating-point number to convert.</param>
        public CharOf(double dbl) :
            this(new Live<char>(() => Convert.ToChar(Convert.ToInt64(dbl))))
        { }

        /// <summary>
        /// Converts the value of the specified single-precision floating-point number to an equivalent Unicode character.
        /// </summary>
        /// <param name="flt">The single-precision floating-point number.</param>
        public CharOf(float flt) :
            this(new Live<char>(() => Convert.ToChar(Convert.ToInt64(flt))))
        { }

        /// <summary>
        /// Converts the first character of a specified string to a Unicode character.
        /// </summary>
        /// <param name="str">A string of length 1.</param>
        public CharOf(string str) :
            this(new Live<char>(() => Convert.ToChar(str)))
        { }

        /// <summary>
        /// Converts the value of the specified 8-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="byt">The 8-bit unsigned integer to convert.</param>
        public CharOf(byte byt) :
            this(new Live<char>(() => Convert.ToChar(byt)))
        { }

        /// <summary>
        /// Converts the value of the specified 8-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="sByt">The 8-bit signed integer to convert.</param>
        public CharOf(sbyte sByt) :
            this(new Live<char>(() => Convert.ToChar(sByt)))
        { }

        /// <summary>
        /// Primary ctor
        /// </summary>
        /// <param name="converter">Converter method who returns the character.</param>
        private CharOf(IScalar<char> converter)
        {
            _converter = new ScalarOf<char>(converter);
        }

        /// <summary>
        /// the char
        /// </summary>
        /// <returns></returns>
        public char Value()
        {
            return _converter.Value();
        }
    }
}
