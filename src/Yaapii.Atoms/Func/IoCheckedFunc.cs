using System;
using System.IO;
using System.Threading;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// func that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    public sealed class IoCheckedFunc<In, Out> : IFunc<In, Out>
    {
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// func that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="func">func to call</param>
        public IoCheckedFunc(System.Func<In, Out> func) : this(new FuncOf<In, Out>((X) => func(X)))
        { }

        /// <summary>
        /// func that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="func">func to call</param>
        public IoCheckedFunc(IFunc<In, Out> func)
        {
            _func = func;
        }

        public Out Invoke(In input)
        {
            try
            {
                return this._func.Invoke(input);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (System.Threading.ThreadStateException ex)
            {
                Thread.CurrentThread.Join();
                throw new IOException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message, ex);
            }
        }

    }
}
