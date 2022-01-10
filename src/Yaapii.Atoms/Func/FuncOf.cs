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

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that has only output.
    /// </summary>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class FuncOf<Out> : IFunc<Out>
    {
        /// <summary>
        /// func that will be called
        /// </summary>
        private readonly IFunc<bool, Out> func;

        /// <summary>
        /// Function that has only output.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public FuncOf(System.Func<Out> fnc)
        {
            this.func = new FuncOf<bool, Out>(() => fnc.Invoke());
        }

        /// <summary>
        /// Call function and retrieve output.
        /// </summary>
        /// <returns>the output</returns>
        public Out Invoke()
        {
            return this.func.Invoke(true);
        }
    }

    /// <summary>
    /// Function that has input and output
    /// </summary>
    /// <typeparam name="In">input</typeparam>
    /// <typeparam name="Out">output</typeparam>
    public sealed class FuncOf<In, Out> : IFunc<In, Out>
    {
        private readonly System.Func<In, Out> func;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc"></param>
        public FuncOf(System.Func<Out> fnc) : this(input => fnc.Invoke())
        { }

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="proc">procedure to execute</param>
        /// <param name="result"></param>
        public FuncOf(IAction<In> proc, Out result) : this(
                input =>
                {
                    proc.Invoke(input);
                    return result;
                }
        )
        { }

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="func">function to execute</param>
        public FuncOf(System.Func<In, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Generate the Output from the input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Out Invoke(In input)
        {
            return func.Invoke(input);
        }
    }

    /// <summary>
    /// Function that has two inputs and an output
    /// </summary>
    /// <typeparam name="In1">type of first input</typeparam>
    /// <typeparam name="In2">type of second input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class FuncOf<In1, In2, Out> : IBiFunc<In1, In2, Out>
    {
        private readonly System.Func<In1, In2, Out> func;

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public FuncOf(System.Func<In1, In2, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invoke the function with arguments and retrieve th output.
        /// </summary>
        /// <param name="arg1">first argument</param>
        /// <param name="arg2">second argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In1 arg1, In2 arg2)
        {
            return func.Invoke(arg1, arg2);
        }

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public static FuncOf<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> func) =>
            new FuncOf<In1, In2, Out>(func);
    }

    /// <summary>
    /// Function that has two inputs and an output
    /// </summary>
    /// <typeparam name="In1">type of first input</typeparam>
    /// <typeparam name="In2">type of second input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class FuncOf<In1, In2, In3, Out> : IFunc<In1, In2, In3, Out>
    {
        private readonly System.Func<In1, In2, In3, Out> func;

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public FuncOf(System.Func<In1, In2, In3, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invoke the function with arguments and retrieve th output.
        /// </summary>
        /// <param name="arg1">first argument</param>
        /// <param name="arg2">second argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In1 arg1, In2 arg2, In3 arg3)
        {
            return func.Invoke(arg1, arg2, arg3);
        }

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public static FuncOf<In1, In2, In3, Out> New<In1, In2, In3, Out>(System.Func<In1, In2, In3, Out> func) =>
            new FuncOf<In1, In2, In3, Out>(func);
    }

    public static class FuncOf
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc"></param>
        public static FuncOf<In> New<In>(System.Func<In> fnc) => new FuncOf<In>(fnc);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc"></param>
        public static FuncOf<In, Out> New<In, Out>(System.Func<In, Out> fnc) =>
            new FuncOf<In, Out>(fnc);

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="proc">procedure to execute</param>
        /// <param name="result"></param>
        public static FuncOf<In, Out> New<In, Out>(IAction<In> proc, Out result) =>
            new FuncOf<In, Out>(proc, result);

        public static FuncOf<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> func) =>
            new FuncOf<In1, In2, Out>(func);
    }
}
