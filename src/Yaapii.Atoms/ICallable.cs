using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// Represents a function that you call without an argument and which returns something.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICallable<T>
    {
        T Call();
    }
}
