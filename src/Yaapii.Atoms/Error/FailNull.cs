using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if object is null.
    /// </summary>
    public sealed class FailNull : IFail
    {
        private readonly object _obj;
        private readonly string _hint;

        /// <summary>
        /// Fail if object is null.
        /// </summary>
        /// <param name="obj">object to check</param>
        public FailNull(object obj) : this(obj, "Parameter is null") { }

        /// <summary>
        /// Fail if object is null.
        /// </summary>
        /// <param name="obj">object to check</param>
        /// <param name="hint">msg to put in exception</param>
        public FailNull(object obj, string hint)
        {
            _obj = obj;
            _hint = hint;
        }

        public void Go()
        {
            if (_obj == null) throw new ArgumentNullException(_hint);
        }
    }
}
