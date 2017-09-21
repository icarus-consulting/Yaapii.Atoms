using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Logical false.
    /// </summary>
    public sealed class False : IScalar<Boolean>
    {
        /// <summary>
        /// Logical false.
        /// </summary>
        public False()
        { }

        public Boolean Value()
        {
            return false;
        }
    }
}
