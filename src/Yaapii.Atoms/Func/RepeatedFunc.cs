using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that repeats its calculation a few times before returning the result.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class RepeatedFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// function to call
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// how often to run
        /// </summary>
        private readonly int _times;

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public RepeatedFunc(System.Func<In, Out> fnc, int max) : this(new FuncOf<In, Out>((X) => fnc(X)), max)
        { }

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public RepeatedFunc(IFunc<In, Out> fnc, int max)
        {
            this._func = fnc;
            this._times = max;
        }

        public Out Invoke(In input)
        {
            if (this._times <= 0)
            {
                throw new ArgumentException("The number of repetitions must be at least 1");
            }

            Out result;
            result = this._func.Invoke(input);
            for (int idx = 0; idx < this._times - 1; ++idx)
            {
                result = this._func.Invoke(input);
            }
            return result;
        }
    }
}
