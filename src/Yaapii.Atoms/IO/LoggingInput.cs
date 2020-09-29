using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    public sealed class LoggingInput : IInput
    {

        private readonly IInput origin;
        private readonly string source;
        private readonly Logger logger;

        public LoggingInput(IInput input, string source) : this(input,source,Logger.GetLogger(source))
        {}

        public LoggingInput(IInput input, string source, Logger logger)
        {
            this.origin = input;
            this.source = source;
            this.logger = logger;
        }

        public Stream Stream()
        {
            return 
                new LoggingInputStream(
                    this.origin.Stream(),
                    this.source,
                    this.logger
                );
        }
    }

    internal class Logger
    {
        internal static Logger GetLogger(string source)
        {
            throw new NotImplementedException();
        }
    }
}
