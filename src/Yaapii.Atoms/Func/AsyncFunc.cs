using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms.Misc;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Func that runs in the background.
    /// If you want your piece of code to be executed in the background, use
    /// <see cref="AsyncFunc""/> as following:
    /// int length = new AsyncFunc(
    ///     input => input.length()
    /// ).Apply("Hello, world!").Length;
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class AsyncFunc<In, Out> : IFunc<In, Task<Out>>
        where Out:class
    {
        /// <summary>
        /// func to call
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// Func that runs in the background.
        /// </summary>
        /// <param name="proc">procedure to call</param>
        public AsyncFunc(IProc<In> proc) : this(new FuncOf<In, Out>(proc, null)) //@TODO eliminate null passing
        { }

        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc""/> as following:
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
        /// <see cref="AsyncFunc""/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="fnc">func to call</param>
        public AsyncFunc(IFunc<In, Out> fnc)
        {
            this._func = fnc;
        }

        public async Task<Out> Invoke(In input)
        {
            return await Task.Run(() => this._func.Invoke(input));
        }
    }
}
