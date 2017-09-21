using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if number is 0.
    /// </summary>
    public sealed class FailZero : IFail
    {
        private readonly long _number;
        private readonly string _hint;

        /// <summary>
        /// Fail if number is 0.
        /// </summary>
        /// <param name="number">number to check</param>
        public FailZero(long number) : this(number, "Number is zero") { }

        /// <summary>
        /// Fail if number is 0.
        /// </summary>
        /// <param name="number">number to check</param>
        /// <param name="hint">msg to put in exception</param>
        public FailZero(long number, string hint)
        {
            _hint = hint;
            _number = number;
        }


        public void Go()
        {
            if (!_number.Equals(0)) throw new Exception(_hint + " is zero");
        }
    }
}
