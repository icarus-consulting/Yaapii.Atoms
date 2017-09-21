using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IOutput"/> that doesn't throw <see cref="IOException"/> but throws <see cref="Atoms.Error.UncheckedIOException"/> instead.
    /// </summary>
    public sealed class UncheckedOutput : IOutput, IDisposable
    {
        /// <summary>
        /// the output
        /// </summary>
        private readonly IOutput _output;

        /// <summary>
        /// <see cref="IOutput"/> that doesn't throw <see cref="IOException"/> but throws <see cref="Atoms.Error.UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="opt">output to uncheck</param>
        public UncheckedOutput(IOutput opt)
        {
            this._output = opt;
        }

        public Stream Stream()
        {
            return new UncheckedScalar<Stream>(
                    new ScalarOf<Stream>(
                        this._output.Stream())
                   ).Value();
        }

        public void Dispose()
        {
            ((IDisposable)this._output.Stream()).Dispose();
        }
    }
}
