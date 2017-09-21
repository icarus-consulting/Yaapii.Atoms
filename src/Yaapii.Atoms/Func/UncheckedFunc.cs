using System.IO;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Func that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public sealed class UncheckedFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">func to call</param>
        public UncheckedFunc(System.Func<In, Out> func) : this(new FuncOf<In, Out>((X) => func(X)))
        { }

        /// <summary>
        /// Func that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="func">func to call</param>
        public UncheckedFunc(IFunc<In, Out> func)
        {
            this._func = func;
        }

        public Out Invoke(In input)
        {
            try
            {
                return new IoCheckedFunc<In, Out>(_func).Invoke(input);
            }
            catch (IOException ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

    }
}
