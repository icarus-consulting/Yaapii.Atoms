using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Logical negative.
    /// </summary>
    public sealed class Not : IScalar<Boolean>
    {
        private readonly IScalar<Boolean> origin;

        /// <summary>
        /// Logical negative.
        /// </summary>
        /// <param name="scalar">scalar to negate</param>
        public Not(IScalar<Boolean> scalar)
        {
            this.origin = scalar;
        }

        public Boolean Value()
        {
            return !this.origin.Value();
        }
    }
}
