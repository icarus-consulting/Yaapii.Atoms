using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="char"/> out of other objects.
    /// </summary>
    public sealed class CharOf : IScalar<char>
    {
        private readonly IScalar<char> _chr;

        /// <summary>
        /// Converts the value of the specified 32-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="integer">The 32-bit signed integer to convert.</param>
        public CharOf(int integer) :
            this(new ScalarOf<char>(() => Convert.ToChar(integer)))
        { }

        /// <summary>
        /// Converts the value of the specified 32-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="uInteger">The 32-bit unsigned integer to convert.</param>
        public CharOf(uint uInteger) :
            this(new ScalarOf<char>(() => Convert.ToChar(uInteger)))
        { }

        /// <summary>
        /// Converts the value of the specified 16-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="shrt">The 16-bit signed integer to convert.</param>
        public CharOf(short shrt) :
            this(new ScalarOf<char>(() => Convert.ToChar(shrt)))
        { }

        /// <summary>
        /// Converts the value of the specified 16-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="uShort">The 16-bit unsigned integer to convert.</param>
        public CharOf(ushort uShort) :
            this(new ScalarOf<char>(() => Convert.ToChar(uShort)))
        { }

        /// <summary>
        /// Converts the value of the specified 64-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="lng">The 64-bit signed integer to convert.</param>
        public CharOf(long lng) :
            this(new ScalarOf<char>(() => Convert.ToChar(lng)))
        { }

        /// <summary>
        /// Converts the value of the specified 64-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="ulng">The 64-bit unsigned integer to convert.</param>
        public CharOf(ulong ulng) :
            this(new ScalarOf<char>(() => Convert.ToChar(ulng)))
        { }

        /// <summary>
        /// Converts the value of the specified double-precision floating-point number to an equivalent Unicode character.
        /// </summary>
        /// <param name="dbl">The double-precision floating-point number to convert.</param>
        public CharOf(double dbl) :
            this(new ScalarOf<char>(() => Convert.ToChar(Convert.ToInt64(dbl))))
        { }

        /// <summary>
        /// Converts the value of the specified single-precision floating-point number to an equivalent Unicode character.
        /// </summary>
        /// <param name="flt">The single-precision floating-point number.</param>
        public CharOf(float flt) :
            this(new ScalarOf<char>(() => Convert.ToChar(Convert.ToInt64(flt))))
        { }

        /// <summary>
        /// Converts the first character of a specified string to a Unicode character.
        /// </summary>
        /// <param name="str">A string of length 1.</param>
        public CharOf(string str) :
            this(new ScalarOf<char>(() => Convert.ToChar(str)))
        { }

        /// <summary>
        /// Converts the value of the specified 8-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="byt">The 8-bit unsigned integer to convert.</param>
        public CharOf(byte byt) :
            this(new ScalarOf<char>(() => Convert.ToChar(byt)))
        { }

        /// <summary>
        /// Converts the value of the specified 8-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="sByt">The 8-bit signed integer to convert.</param>
        public CharOf(sbyte sByt) :
            this(new ScalarOf<char>(() => Convert.ToChar(sByt)))
        { }

        /// <summary>
        /// Primary ctor
        /// </summary>
        /// <param name="chr">Converter method who returns the character.</param>
        private CharOf(IScalar<char> chr)
        {
            _chr = chr;
        }

        /// <summary>
        /// Returns the char.
        /// </summary>
        /// <returns></returns>
        public char Value()
        {
            return _chr.Value();
        }
    }
}
