using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    public sealed class UncheckedBytes : IBytes
    {
        /// <summary>
        /// the bytes
        /// </summary>
        private readonly IBytes _bytes;

        /// <summary>
        /// fallback function
        /// </summary>
        private readonly IFunc<IOException, byte[]> _fallback;

        /// <summary>
        /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="bts">the bytes</param>
        public UncheckedBytes(IBytes bts) : this(
                bts, (error) => throw new UncheckedIOException(error))
        { }

        /// <summary>
        /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but calls given <see cref="Atoms.Func{IOException}"/> instead.
        /// </summary>
        /// <param name="bts"></param>
        /// <param name="fallback"></param>
        public UncheckedBytes(IBytes bts, System.Func<IOException, byte[]> fallback) : this(bts, new FuncOf<IOException, byte[]>(fallback))
        { }

        /// <summary>
        /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but calls given <see cref="Atoms.Func{IOException}"/> instead.
        /// </summary>
        /// <param name="bts"></param>
        /// <param name="fallback"></param>
        public UncheckedBytes(IBytes bts, IFunc<IOException, byte[]> fallback)
        {
            this._bytes = bts;
            this._fallback = fallback;
        }

        public byte[] AsBytes()
        {
            byte[] data;
            try
            {
                data = this._bytes.AsBytes();
            }
            catch (IOException ex)
            {
                data = new UncheckedFunc<IOException, byte[]>(this._fallback).Invoke(ex);
            }
            return data;
        }

    }
}
