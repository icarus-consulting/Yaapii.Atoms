using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class TeeOutput : IOutput, IDisposable
    {

        /**
         * The target.
         */
        private readonly IOutput _target;

        /**
         * The copy.
         */
        private readonly IOutput _copy;

        /**
         * Ctor.
         * @param tgt The target
         * @param cpy The copy destination
         * @param charset The charset
         */
        public TeeOutput(IOutput tgt, StreamWriter cpy, Encoding enc) : this(tgt, new OutputTo(cpy, enc))
        { }

        /**
         * Ctor.
         * @param tgt The target
         * @param cpy The copy destination
         */
        public TeeOutput(IOutput tgt, StreamWriter cpy) : this(tgt, new OutputTo(cpy))
        { }

        /**
         * Ctor.
         * @param tgt The target
         * @param cpy The copy destination
         */
        public TeeOutput(IOutput tgt, Uri cpy) : this(tgt, new OutputTo(cpy))
        { }

        /**
         * Ctor.
         * @param tgt The target
         * @param cpy The copy destination
         */
        public TeeOutput(IOutput tgt, Stream cpy) : this(tgt, new OutputTo(cpy))
        { }

        /**
         * Ctor.
         * @param tgt The target
         * @param cpy The copy destination
         */
        public TeeOutput(IOutput tgt, IOutput cpy)
        {
            this._target = tgt;
            this._copy = cpy;
        }

        public Stream Stream()
        {
            return new TeeOutputStream(
                this._target.Stream(), this._copy.Stream()
            );
        }

        public void Dispose()
        {
            try
            {
                this._target.Stream().Dispose();
            }
            catch (Exception) { }

            try
            {
                this._copy.Stream().Dispose();
            }
            catch (Exception) { }
        }

    }
}
