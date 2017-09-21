using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Logical truth.
    /// </summary>
    public sealed class True : IScalar<bool>
    {
        /// <summary>
        /// Logical truth.
        /// </summary>
        public True()
        { }

        public Boolean Value()
        {
            return true;
        }
    }
}
