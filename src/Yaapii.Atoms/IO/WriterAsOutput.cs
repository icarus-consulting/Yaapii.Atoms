using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
    /// </summary>
    public sealed class WriterAsOutput : IOutput, IDisposable
    {
        /// <summary>
        /// the writer
        /// </summary>
        private readonly StreamWriter _writer;

        /// <summary>
        /// encoding of the writer
        /// </summary>
        private readonly IScalar<Decoder> _decoder;

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        public WriterAsOutput(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="enc">encoding of the streamwriter</param>
        /// <param name="max"></param>
        public WriterAsOutput(StreamWriter wtr, Encoding enc) : this(wtr,
            () => enc.GetDecoder())
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="ddr">decoder for the writer</param>
        /// <param name="max">maximum buffer size</param>
        public WriterAsOutput(StreamWriter wtr, Func<Decoder> fnc) : this(wtr, new ScalarOf<Decoder>(fnc))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="ddr">decoder for the writer</param>
        /// <param name="max">maximum buffer size</param>
        public WriterAsOutput(StreamWriter wtr, IScalar<Decoder> ddr)
        {
            this._writer = wtr;
            this._decoder = ddr;
        }

        public Stream Stream()
        {
            return new WriterAsOutputStream(this._writer, this._decoder);
        }

        public void Dispose()
        {
            ((IDisposable)this._writer).Dispose();
        }

    }
}
