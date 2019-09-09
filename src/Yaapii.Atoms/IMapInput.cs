using System.Collections.Generic;
using Yaapii.Atoms;
/// <summary>
/// Input to be attached to a dictionary.
/// </summary>
public interface IMapInput
{
    /// <summary>
    /// Apply the input to a dictionary.
    /// </summary>
    /// <param name="map">Map to update with this input</param>
    /// <returns>Updated dictionary</returns>
    IDictionary<string, string> Apply(IDictionary<string, string> map);

    /// <summary>
    /// Access the raw IDictInput.
    /// </summary>
    /// <returns>Itself as IDictInput.</returns>
    IMapInput Self();
}

/// <summary>
/// Input to be attached to a dictionary.
/// </summary>
public interface IMapInput<Value>
{
    /// <summary>
    /// Apply the input to a dictionary.
    /// </summary>
    /// <param name="dict"></param>
    /// <returns>Updated dictionary</returns>
    IDictionary<string, Value> Apply(IDictionary<string, Value> dict);

    /// <summary>
    /// Access the raw IDictInput.
    /// </summary>
    /// <returns>Itself as IDictInput.</returns>
    IMapInput<Value> Self();
}

/// <summary>
/// Input to be attached to a dictionary.
/// </summary>
public interface IMapInput<Key, Value>
{
    /// <summary>
    /// Apply the input to a dictionary.
    /// </summary>
    /// <param name="dict"></param>
    /// <returns>Updated dictionary</returns>
    IDictionary<Key, Value> Apply(IDictionary<Key, Value> dict);

    /// <summary>
    /// Access the raw IDictInput.
    /// </summary>
    /// <returns>Itself as IDictInput.</returns>
    IMapInput<Key, Value> Self();
}