using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Logged input.
    /// </summary>
    public sealed class LoggingInput : IInput
    {

        private readonly IInput origin;
        private readonly string source;

        /// <summary>
        /// Logged input.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="source"></param>
        public LoggingInput(IInput input, string source)
        {
            this.origin = input;
            this.source = source;
        }

        public Stream Stream()
        {
            return 
                new LoggingInputStream(
                    this.origin.Stream(),
                    this.source
                );
        }
    }

}
