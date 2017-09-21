using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// A capsule for anything.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IScalar<T>
    {
        T Value();
    }
}