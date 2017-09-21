using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// A function with two inputs and one output which always throws <see cref="IOException"/> if it fails.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    public sealed class IoCheckedBiFunc<X, Y, Z> : IBiFunc<X, Y, Z>
    {
        private readonly IBiFunc<X, Y, Z> _func;

        /// <summary>
        /// A function with two inputs and one output which always throws <see cref="IOException"/> if it fails.
        /// </summary>
        /// <param name="fnc">function to call</param>
        public IoCheckedBiFunc(System.Func<X, Y, Z> fnc) : this(new BiFuncOf<X, Y, Z>(fnc))
        { }

        /// <summary>
        /// A function with two inputs and one output which always throws <see cref="IOException"/> if it fails.
        /// </summary>
        /// <param name="fnc">function to call</param>
        public IoCheckedBiFunc(IBiFunc<X, Y, Z> fnc)
        {
            this._func = fnc;
        }

        public Z Apply(X first, Y second)
        {
            try
            {
                return this._func.Apply(first, second);
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
