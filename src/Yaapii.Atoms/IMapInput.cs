// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
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
