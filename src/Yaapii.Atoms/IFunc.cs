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

namespace Yaapii.Atoms
{
    /// <summary>
    /// Represents a function that you call without an argument and which returns something.
    /// </summary>
    /// <typeparam name="Out"></typeparam>
    public interface IFunc<Out>
    {
        /// <summary>
        /// Call the function and retrieve the output.
        /// </summary>
        /// <returns></returns>
        Out Invoke();
    }

    /// <summary>
    /// A function that has one input and an output.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public interface IFunc<In, Out>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>the output</returns>
        Out Invoke(In input);
    }

    /// <summary>
    /// A function that has one input and an output.
    /// </summary>
    public interface IFunc<In1, In2, Out>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input1">the input 1</param>
        /// <param name="input2">the input 2</param>
        /// <returns>the output</returns>
        Out Invoke(In1 input1, In2 input2);
    }

    /// <summary>
    /// A function that has one input and an output.
    /// </summary>
    public interface IFunc<In1, In2, In3, Out>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input1">the input 1</param>
        /// <param name="input2">the input 2</param>
        /// <param name="input3">the input 3</param>
        /// <returns>the output</returns>
        Out Invoke(In1 input1, In2 input2, In3 input3);
    }
}
