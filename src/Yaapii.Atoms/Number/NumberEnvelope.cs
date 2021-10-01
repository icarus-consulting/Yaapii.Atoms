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

namespace Yaapii.Atoms.Number
{
    /// <summary>
    /// Wraps up Conversions to <see cref="INumber"/>
    /// </summary>
    public abstract class NumberEnvelope : INumber
    {
        private readonly IScalar<double> _dbl;
        private readonly IScalar<int> _int;
        private readonly IScalar<long> _lng;
        private readonly IScalar<float> _flt;

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl">the double</param>
        public NumberEnvelope(double dbl) : this(new Live<double>(dbl))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(int itg) : this(new Live<int>(itg))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(long lng) : this(new Live<long>(lng))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(float flt) : this(new Live<float>(flt))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(IScalar<float> flt) : this(
            new Live<double>(() => Convert.ToDouble(flt.Value())),
            new Live<int>(() => Convert.ToInt32(flt.Value())),
            new Live<long>(() => Convert.ToInt64(flt.Value())),
            flt)
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(IScalar<int> itg) : this(
            new Live<double>(() => Convert.ToDouble(itg.Value())),
            itg,
            new Live<long>(() => Convert.ToInt64(itg.Value())),
            new Live<float>(() => Convert.ToSingle(itg.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(IScalar<long> lng) : this(
            new Live<double>(() => Convert.ToDouble(lng.Value())),
            new Live<int>(() => Convert.ToInt32(lng.Value())),
            lng,
            new Live<float>(() => Convert.ToSingle(lng.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl"></param>
        public NumberEnvelope(IScalar<double> dbl) : this(
            dbl,
            new Live<int>(() => Convert.ToInt32(dbl.Value())),
            new Live<long>(() => Convert.ToInt64(dbl.Value())),
            new Live<float>(() => Convert.ToSingle(dbl.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from the given inputs
        /// </summary>
        /// <param name="dbl"></param>
        /// <param name="itg"></param>
        /// <param name="lng"></param>
        /// <param name="flt"></param>
        public NumberEnvelope(IScalar<double> dbl, IScalar<int> itg, IScalar<long> lng, IScalar<float> flt)
        {
            _dbl = dbl;
            _flt = flt;
            _lng = lng;
            _int = itg;
        }

        /// <summary>
        /// Number as double representation
        /// Precision: ±5.0e−324 to ±1.7e308	(15-16 digits)
        /// </summary>
        /// <returns></returns>
        public double AsDouble()
        {
            return _dbl.Value();
        }

        /// <summary>
        /// Number as float representation
        /// Precision: 	±1.5e−45 to ±3.4e38	    (7 digits)
        /// </summary>
        /// <returns></returns>
        public float AsFloat()
        {
            return _flt.Value();
        }

        /// <summary>
        /// Number as integer representation
        /// Range -2,147,483,648 to 2,147,483,647	(Signed 32-bit integer)
        /// </summary>
        /// <returns></returns>
        public int AsInt()
        {
            return _int.Value();
        }

        /// <summary>
        /// Number as long representation
        /// Range -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	(Signed 64-bit integer)
        /// </summary>
        /// <returns></returns>
        public long AsLong()
        {
            return _lng.Value();
        }
    }
}
