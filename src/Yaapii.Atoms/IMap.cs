using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{

    /// <summary>
    /// Shorthand interface to make a string to string Dictionary.
    /// </summary>
    public interface IMap : IDictionary<string, string>
    { }

    /// <summary>
    /// Shorthand interface to make a string to value Dictionary.
    /// </summary>
    public interface IMap<Value> : IDictionary<string, Value>
    { }

    /// <summary>
    /// Shorthand interface to make a string to string Dictionary.
    /// </summary>
    public interface IMap<Key, Value> : IDictionary<Key, Value>
    { }
}
