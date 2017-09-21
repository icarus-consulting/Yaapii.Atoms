using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Console output stream.
    /// </summary>
    public sealed class ConsoleOutput : IOutput
    {
        /// <summary>
        /// Console output stream.
        /// </summary>
        public ConsoleOutput()
        { }

        public Stream Stream()
        {
            return Console.OpenStandardOutput();
        }
    }
}
