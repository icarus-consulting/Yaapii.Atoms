using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    public interface IFunc<In, Out>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>the output</returns>
        Out Invoke(In input);
    }
}
