using System.IO;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that does not allow null as input.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class NoNulls<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// The function
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// Function that does not allow null as input.
        /// </summary>
        /// <param name="Func">he function</param>
        public NoNulls(IFunc<In, Out> func)
        {
            _func = func;
        }

        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            new FailNull(input, "got NULL instead of a valid function");

            Out result = _func.Invoke(input);
            if (result == null)
            {
                throw new IOException("got NULL instead of a valid result");
            }
            return result;
        }
    }
}
