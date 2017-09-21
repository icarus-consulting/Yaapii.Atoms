using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IInput"/> that reads once and then returns from cache.
    /// </summary>
    public sealed class StickyInput : IInput
    {
        /// <summary>
        /// the cache
        /// </summary>
        private readonly IScalar<byte[]> _cache;

        /// <summary>
        /// <see cref="IInput"/> that reads once and then returns from cache.
        /// Closes the input stream after first read.
        /// </summary>
        /// <param name="input"></param>
        public StickyInput(IInput input)
        {
            this._cache = new StickyScalar<byte[]>(
                new ScalarOf<byte[]>(() =>
                    {
                        MemoryStream baos = new MemoryStream();
                        new LengthOf(
                            new TeeInput(input, new OutputTo(baos))
                        ).Value();
                        input.Stream().Dispose();
                        return baos.ToArray();
                    }));
        }

        public Stream Stream()
        {
            return new MemoryStream(
                new IoCheckedScalar<byte[]>(this._cache).Value()
            );
        }
    }
}
