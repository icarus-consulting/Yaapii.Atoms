using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// <see cref="IScalar{bool}"/> that is a logical disjunction.
    /// </summary>
    public sealed class Or : IScalar<Boolean>
    {
        private readonly IEnumerable<IScalar<Boolean>> origin;

        /// <summary>
        /// <see cref="IScalar{bool}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">functions returning scalars to chain with or</param>
        public Or(params Func<Boolean>[] scalars) : this(new Mapped<Func<bool>, IScalar<bool>>(scalars, fnc => new ScalarOf<bool>(fnc)))
        { }

        /// <summary>
        /// <see cref="IScalar{bool}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">functions returning scalars to chain with or</param>
        public Or(params ICallable<Boolean>[] scalars) : this(new Mapped<ICallable<bool>, IScalar<bool>>(scalars, fnc => new ScalarOf<bool>(fnc)))
        { }

        /// <summary>
        /// <see cref="IScalar{bool}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">scalars to chain with or</param>        
        public Or(params IScalar<Boolean>[] scalars) : this(new EnumerableOf<IScalar<bool>>(scalars))
        { }

        /// <summary>
        /// <see cref="IScalar{bool}"/> that is a logical disjunction.
        /// </summary>
        /// <param name="scalars">scalars to chain with or</param>
        public Or(IEnumerable<IScalar<Boolean>> scalars)
        {
            this.origin = scalars;
        }

        public Boolean Value()
        {
            bool result = false;
            foreach (IScalar<Boolean> item in this.origin)
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
