using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// Function that accepts two arguments
    /// </summary>
    /// <typeparam name="X">Type of Input</typeparam>
    /// <typeparam name="Y">Type of Input</typeparam>
    /// <typeparam name="Z">Type of Output</typeparam>
    public interface IBiFunc<X, Y, Z>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Output</returns>
        Z Apply(X first, Y second);
    }
}
