using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Checks the equality of contents.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Equals<T> : IScalar<Boolean>
        where T : IComparable<T>
    {
        private readonly IScalar<T> _first;
        private readonly IScalar<T> _second;

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="frst">function to return first value to compare</param>
        /// <param name="scnd">function to return second value to compare</param>
        public Equals(Func<T> frst, Func<T> scnd) : this(new ScalarOf<T>(frst), new ScalarOf<T>(scnd))
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="frst">first value to compare</param>
        /// <param name="scnd">second value to compare</param>
        public Equals(T frst, T scnd) : this(new ScalarOf<T>(frst), new ScalarOf<T>(scnd))
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="frst">scalar of first value to compare</param>
        /// <param name="scnd">scalar of second value to compare</param>
        public Equals(IScalar<T> frst, IScalar<T> scnd)
        {
            this._first = frst;
            this._second = scnd;
        }

        public Boolean Value()
        {
            return this._first.Value().CompareTo(this._second.Value()) == 0;
        }
    }
}
