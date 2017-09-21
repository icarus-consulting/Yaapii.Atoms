﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Input which returns an alternate value if it fails.
    /// </summary>
    public sealed class InputWithFallback : IInput, IDisposable
    {
        /// <summary>
        /// main input
        /// </summary>
        private readonly IInput _main;

        /// <summary>
        /// alternative input
        /// </summary>
        private readonly IoCheckedFunc<IOException, IInput> _alternative;

        /// <summary>
        /// Input which returns an alternate value if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        public InputWithFallback(IInput input) : this(input, new DeadInput())
        { }

        /// <summary>
        /// Input which returns an alternate value if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">a fallback input</param>
        public InputWithFallback(IInput input, IInput alt) : this(input, (error) => alt)
        { }

        /// <summary>
        /// Input which returns an alternate value from the given <see cref="IFunc{In, Out}"/> if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">a fallback input</param>
        public InputWithFallback(IInput input, IFunc<IOException, IInput> alt) : this(
            input, new IoCheckedFunc<IOException, IInput>(alt))
        { }

        /// <summary>
        /// Input which returns an alternate value from the given <see cref="Func{IOException, IInput}"/>if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">function to return alternative input</param>
        public InputWithFallback(IInput input, Func<IOException, IInput> alt) : this(input,
            new IoCheckedFunc<IOException, IInput>(alt))
        { }

        /// <summary>
        /// Input which returns an alternate value from the given <see cref="IoCheckedFunc{IOException, IInput}"/>if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">an alternative input</param>
        public InputWithFallback(IInput input, IoCheckedFunc<IOException, IInput> alt)
        {
            this._main = input;
            this._alternative = alt;
        }

        public Stream Stream()
        {
            Stream stream;
            try
            {
                stream = this._main.Stream();
            }
            catch (IOException ex)
            {
                stream = this._alternative.Invoke(ex).Stream();
            }
            return stream;
        }

        public void Dispose()
        {
            ((IDisposable)this.Stream()).Dispose();
        }

    }
}
