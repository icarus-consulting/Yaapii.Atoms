using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Logical or.
    /// </summary>
    public sealed class Or : IScalar<Boolean>
    {
        private readonly IEnumerable<IScalar<Boolean>> _enumerable;

        /// <summary>
        /// Logical or.
        /// </summary>
        /// <param name="src">truths to combine</param>
        public Or(params IScalar<Boolean>[] src)
        {
            this._enumerable = src;
        }

        public Boolean Value()
        {
            var result = false;
            foreach (IScalar<Boolean> item in this._enumerable)
            {
                if (item.Value())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

    }
}
