// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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


namespace Yaapii.Atoms.Swap
{
    /// <summary>
    /// Swap that has input and output
    /// </summary>
    /// <typeparam name="In">input</typeparam>
    /// <typeparam name="Out">output</typeparam>
    public sealed class SwapOf<In, Out> : ISwap<In, Out>
    {
        private readonly System.Func<In, Out> func;

        /// <summary>
        /// Swap that has input and output
        /// </summary>
        /// <param name="fnc"></param>
        public SwapOf(System.Func<Out> fnc) : this(input => fnc.Invoke())
        { }

        /// <summary>
        /// Swap that has input and output
        /// </summary>
        /// <param name="func">function to execute</param>
        public SwapOf(System.Func<In, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Swap the input to an output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Out Flip(In input)
        {
            return func.Invoke(input);
        }
    }

    /// <summary>
    /// Swap two inputs to an output
    /// </summary>
    /// <typeparam name="In1">type of first input</typeparam>
    /// <typeparam name="In2">type of second input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class SwapOf<In1, In2, Out> : ISwap<In1, In2, Out>
    {
        private readonly System.Func<In1, In2, Out> func;

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public SwapOf(System.Func<In1, In2, Out> func)
        {
            this.func = func;
        }
       
        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public static SwapOf<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> func) =>
            new SwapOf<In1, In2, Out>(func);

        public Out Flip(In1 input1, In2 input2)
        {
            return func.Invoke(input1, input2);
        }
    }

    /// <summary>
    /// Swap three inputs to an output
    /// </summary>
    /// <typeparam name="In1"></typeparam>
    /// <typeparam name="In2"></typeparam>
    /// <typeparam name="In3"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public sealed class SwapOf<In1, In2, In3, Out> : ISwap<In1, In2, In3, Out>
    {
        private readonly System.Func<In1, In2, In3, Out> func;

        /// <summary>
        /// Swap three inputs to an output
        /// </summary>
        /// <param name="func"></param>
        public SwapOf(System.Func<In1, In2, In3, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        ///  /// Swap three inputs to an output
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <returns></returns>
        public Out Flip(In1 arg1, In2 arg2, In3 arg3)
        {
            return func.Invoke(arg1, arg2, arg3);
        }      

        public Out Invoke(In1 input1, In2 input2, In3 input3)
        {
            throw new System.NotImplementedException();
        }  
    }
}
