using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if enum is empty.
    /// </summary>
    /// <typeparam name="T">type of enum</typeparam>
    public sealed class FailEmpty<T> : IFail
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly string _hint;

        /// <summary>
        /// Fail if enum is empty.
        /// </summary>
        /// <param name="enumerable">enum to check</param>
        public FailEmpty(IEnumerable<T> enumerable) : this(enumerable, "Collection is empty") { }

        /// <summary>
        /// Fail if enum is empty.
        /// </summary>
        /// <param name="enumerable">enum to check</param>
        /// <param name="hint">msg to display in exception</param>
        public FailEmpty(IEnumerable<T> enumerable, string hint)
        {
            _enumerable = enumerable;
            _hint = hint;
        }

        public void Go()
        {
            if (!_enumerable.GetEnumerator().MoveNext()) throw new Exception(_hint + " is empty");
        }
    }
}
