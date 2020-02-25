using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// A key-value pair string to string to add to a map.
    /// </summary>
    public interface IKvp
    {
        string Key();
        string Value();
        bool IsLazy();
    }

    /// <summary>
    /// A key-value pair to add to a map.
    /// </summary>
    public interface IKvp<TValue>
    {
        string Key();
        TValue Value();
        bool IsLazy();
    }

    /// <summary>
    /// A key-value pair to add to a map.
    /// </summary>
    public interface IKvp<TKey, TValue>
    {
        TKey Key();
        TValue Value();
        bool IsLazy();
    }
}
