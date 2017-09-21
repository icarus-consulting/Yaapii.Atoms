using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// Represents a function that you call without input and that has no output.
    /// </summary>
    public interface IAction
    {
        void Run();
    }
}
