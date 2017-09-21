using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IInput"/> that doesn't throw <see cref="IOException"/> but throws <see cref="Atoms.Error.UncheckedIOException"/> instead.
    /// </summary>
    public sealed class UncheckedInput : IInput, IDisposable
    {
        /// <summary>
        /// <see cref="IInput"/> that doesn't throw <see cref="IOException"/> but throws <see cref="Atoms.Error.UncheckedIOException"/> instead.
        /// </summary>
        private readonly IInput _input;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ipt">the input</param>
        public UncheckedInput(IInput ipt)
        {
            this._input = ipt;
        }

        public Stream Stream()
        {
            return new UncheckedScalar<Stream>(new ScalarOf<Stream>(this._input.Stream())).Value();
        }

        public void Dispose()
        {
            ((IDisposable)this._input.Stream()).Dispose();
        }
    }
}
