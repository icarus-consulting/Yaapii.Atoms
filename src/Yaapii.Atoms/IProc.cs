using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// A function with input, but no output.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public interface IProc<X>
    {
        void Exec(X input);
    }
}
