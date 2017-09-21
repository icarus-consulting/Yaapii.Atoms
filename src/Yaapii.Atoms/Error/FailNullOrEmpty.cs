using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if object is null or empty.
    /// </summary>
    public sealed class FailNullOrEmpty : IFail
    {
        private readonly string _string;
        private readonly string _hint;

        /// <summary>
        /// Fail if object is null or empty.
        /// </summary>
        /// <param name="str">string to check</param>
        public FailNullOrEmpty(string str) : this(str, "Parameter is null") { }

        /// <summary>
        /// Fail if object is null or empty.
        /// </summary>
        /// <param name="str">string to check</param>
        /// <param name="hint">msg to display in exception</param>
        public FailNullOrEmpty(string str, string hint)
        {
            _string = str;
            _hint = hint;
        }

        public void Go()
        {
            if (String.IsNullOrEmpty(_string)) throw new ArgumentNullException(_hint);
        }
    }
}
