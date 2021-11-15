// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
    /// Represents a function that you call without input and that has no output.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Execute the action.
        /// </summary>
        void Invoke();
    }

    /// <summary>
    /// A function with input, but no output.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public interface IAction<In>
    {
        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="input">input argument</param>
        void Invoke(In input);
    }

    /// <summary>
    /// A function with two inputs, but no output.
    /// </summary>
    /// <typeparam name="In1"></typeparam>
    /// <typeparam name="In2"></typeparam>
    public interface IAction<In1, In2>
    {
        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        void Invoke(In1 input1, In2 input2);
    }

    /// <summary>
    /// A function with three inputs, but no output.
    /// </summary>
    /// <typeparam name="In1"></typeparam>
    /// <typeparam name="In2"></typeparam>
    /// <typeparam name="In3"></typeparam>
    public interface IAction<In1, In2, In3>   
    {
        /// <summary>
        /// Execute the proc.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="input3"></param>
        void Invoke(In1 input1, In2 input2, In3 input3);
    }
}
