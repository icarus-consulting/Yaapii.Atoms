using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// Representation of a number.
    /// </summary>
    public interface INumber
    {
        /// <summary>
        /// The number represented as LONG
        /// </summary>
        /// <returns>the long</returns>
        long AsLong();

        /// <summary>
        /// The number represented as INTEGER
        /// </summary>
        /// <returns>the integer</returns>
        int AsInt();

        /// <summary>
        /// The number represented as DOUBLE
        /// </summary>
        /// <returns>the double</returns>
        double AsDouble();

        /// <summary>
        /// The number represented as FLOAT
        /// </summary>
        /// <returns>the float</returns>
        float AsFloat();

    }
}
