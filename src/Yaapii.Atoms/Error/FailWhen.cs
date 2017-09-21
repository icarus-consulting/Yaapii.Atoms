using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if condition is matched.
    /// </summary>
    public sealed class FailWhen : IFail
    {
        private readonly Func<bool> _condition;
        private readonly string _hint;

        /// <summary>
        /// Fail if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        public FailWhen(Func<bool> condition) : this(condition, "Failed because the given function failed.")
        { }

        /// <summary>
        /// Fail if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        /// <param name="hint">msg to put in exception</param>
        public FailWhen(Func<bool> condition, string hint)
        {
            _condition = condition;
            _hint = hint;
        }

        public void Go()
        {
            if (_condition.Invoke()) throw new ArgumentException(_hint);
        }
    }
}
