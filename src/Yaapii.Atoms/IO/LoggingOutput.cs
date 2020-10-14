using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    public sealed class LoggingOutput : IOutput
    {
        private readonly IOutput origin;
        private readonly string destination;


        public LoggingOutput(IOutput output, string destination)
        {
            this.origin = output;
            this.destination = destination;
        }

        public Stream Stream()
        {
            return
                new LoggingOutputStream(
                    this.origin.Stream(),
                    this.destination
                );
        }
    }
}
