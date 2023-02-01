// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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

using System.Threading.Tasks;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Func that runs in the background.
    /// If you want your piece of code to be executed in the background, use
    /// <see cref="AsyncFunc{In, Out}"/> as following:
    /// int length = new AsyncFunc(
    ///     input => input.length()
    /// ).Apply("Hello, world!").Length;
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class AsyncFunc<In, Out> : IFunc<In, Task<Out>>
        where Out : class
    {
        /// <summary>
        /// func to call
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// Func that runs in the background.
        /// </summary>
        /// <param name="act">procedure to call</param>
        public AsyncFunc(IAction<In> act) : this(new FuncOf<In, Out>(act, null)) //@TODO eliminate null passing
        { }

        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="func">func to call</param>
        public AsyncFunc(System.Func<In, Out> func) : this(new FuncOf<In, Out>((X) => func(X)))
        { }

        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="fnc">func to call</param>
        public AsyncFunc(IFunc<In, Out> fnc)
        {
            this.func = fnc;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">the input argument</param>
        /// <returns>the output</returns>
        public async Task<Out> Invoke(In input)
        {
            return await Task.Run(() => this.func.Invoke(input));
        }
    }

    /// <summary>
    /// Func that runs in the background.
    /// If you want your piece of code to be executed in the background, use
    /// <see cref="AsyncFunc{In, Out}"/> as following:
    /// int length = new AsyncFunc(
    ///     input => input.length()
    /// ).Apply("Hello, world!").Length;
    /// </summary>
    public static class AsyncFunc
    {
        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="func">func to call</param>
        public static IFunc<In, Task<Out>> New<In, Out>(System.Func<In, Out> func)
            where Out : class
            => new AsyncFunc<In, Out>(func);

        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="fnc">func to call</param>
        public static IFunc<In, Task<Out>> New<In, Out>(IFunc<In, Out> fnc)
            where Out : class
            => new AsyncFunc<In, Out>(fnc);
    }
}
