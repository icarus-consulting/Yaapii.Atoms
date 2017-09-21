using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if is not a number.
    /// </summary>
    public sealed class FailNoNumber : IFail
    {
        private readonly string _string;
        private readonly string _hint;

        /// <summary>
        /// Fail if is not a number.
        /// </summary>
        /// <param name="value">string to check</param>
        public FailNoNumber(string value) : this(value, "Parameter is not a number")
        { }

        /// <summary>
        /// Fail if is not a number.
        /// </summary>
        /// <param name="value">string to check</param>
        /// <param name="hint">msg to throw in exception</param>
        public FailNoNumber(string value, string hint)
        {
            _string = value;
            _hint = hint;
        }

        public void Go()
        {
            try
            {
                Convert.ToDouble(_string);
            }
            catch (Exception)
            {
                throw new ArgumentNullException(_hint);
            }

        }
    }
}
