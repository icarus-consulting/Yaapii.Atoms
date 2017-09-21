using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IInput"/> which will be copied to output while reading.
    /// </summary>
    public sealed class TeeInput : IInput, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IInput _source;

        /// <summary>
        /// the destination
        /// </summary>
        private readonly IOutput _target;

        /// <summary>
        /// <see cref="IInput"/> out of a file <see cref="Uri"/> which will be copied to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="input">input Uri</param>
        /// <param name="output">output Uri</param>
        public TeeInput(Uri input, Uri output) :
            this(new InputOf(input), new OutputTo(output))
        { }

        /// <summary>
        /// <see cref="IInput"/> out of a <see cref="string"/> which will be copied to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="input">input path</param>
        /// <param name="file">output Uri</param>
        public TeeInput(String input, Uri file) :
            this(new BytesAsInput(input), new OutputTo(file))
        { }

        /// <summary>
        /// <see cref="IInput"/> out of a <see cref="byte"/> array which will be copied to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="input">input byte array</param>
        /// <param name="file">output Uri</param>
        public TeeInput(byte[] input, Uri file) : 
            this(new BytesAsInput(input), new OutputTo(file))
        { }

        /// <summary>
        /// <see cref="IInput"/> out of a <see cref="string"/>  which will be copied to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="output">output</param>
        public TeeInput(String input, IOutput output) :
            this(new BytesAsInput(input), output)
        { }

        /// <summary>
        /// <see cref="IInput"/> out of a <see cref="IInput"/> array which will be copied to <see cref="IOutput"/> while reading.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="output">output</param>
        public TeeInput(IInput input, IOutput output)
        {
            this._source = input;
            this._target = output;
        }

        public Stream Stream()
        {
            return new TeeInputStream(this._source.Stream(), this._target.Stream());
        }

        public void Dispose()
        {
            try
            {
                ((IDisposable)this._source).Dispose();
            }
            catch (Exception) { }

            try
            {
                this._target.Stream().Flush();
                ((IDisposable)this._target).Dispose();
            }
            catch (Exception) { }
        }
    }
}
