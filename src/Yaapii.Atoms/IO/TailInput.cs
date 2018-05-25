using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IInput"/> which will be read from EOF 
    /// </summary>
    public sealed class TailInput : IInput, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IInput input;

        /// <summary>
        /// <see cref="IInput"/> out of a <see cref="string"/> which will be read from EOF
        /// </summary>
        /// <param name="input">input string</param>
        public TailInput(string input) : 
            this(new InputOf(input))
        { }

        /// <summary>
        /// <see cref="IInput"/> out of a file <see cref="Uri"/> which will be read from EOF
        /// </summary>
        /// <param name="input">input Uri</param>
        public TailInput(Uri input) :
            this(new InputOf(input))
        { }

        /// <summary>
        /// <see cref="IInput"/> out of a <see cref="IInput"/> which will be read from EOF
        /// </summary>
        /// <param name="input">input</param>
        public TailInput(IInput input)
        {
            this.input = input;
        }

        /// <summary>
        /// Retrieves the stream for reading the tail
        /// </summary>
        /// <returns></returns>
        public Stream Stream()
        {
            return new TailInputStream(input.Stream());
        }

        /// <summary>
        /// Clean up
        /// </summary>
        public void Dispose()
        {
            try
            {
                (this.input as IDisposable)?.Dispose();
            }
            catch (Exception) { }
        }
    }
}
