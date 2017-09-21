using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail if opt is empty.
    /// </summary>
    public sealed class FailEmptyOpt : IFail
    {
        private readonly IOpt _opt;
        private readonly string _hint;

        /// <summary>
        /// Fail if opt is empty.
        /// </summary>
        /// <param name="opt">opt to check</param>
        public FailEmptyOpt(IOpt opt) : this(opt, $"Opt is empty")
        { }

        /// <summary>
        /// Fail if opt is empty.
        /// </summary>
        /// <param name="opt">opt to check</param>
        /// <param name="hint">msg in exception</param>
        public FailEmptyOpt(IOpt opt, string hint)
        {
            _opt = opt;
            _hint = hint;
        }

        public void Go()
        {
            if (!_opt.Has()) throw new Exception(_hint);
        }
    }
}
