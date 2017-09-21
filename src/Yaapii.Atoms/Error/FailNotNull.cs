using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if object is not null.
    /// </summary>
    public sealed class FailNotNull : IFail
    {
        private readonly object _obj;
        private readonly string _hint;

        /// <summary>
        /// Fail if object is not null.
        /// </summary>
        /// <param name="obj">object to check</param>
        public FailNotNull(object obj) : this(obj, "Parameter is not null") { }

        /// <summary>
        /// Fail if object is not null.
        /// </summary>
        /// <param name="obj">object to check</param>
        /// <param name="hint">msg to display</param>
        public FailNotNull(object obj, string hint)
        {
            _obj = obj;
            _hint = hint;
        }

        public void Go()
        {
            if (_obj != null) throw new ArgumentException(_hint);
        }
    }
}
