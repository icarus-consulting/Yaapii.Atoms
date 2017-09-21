using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Console error output stream.
    /// </summary>
    public sealed class ConsoleErrorOutput : IOutput
    {
        /// <summary>
        /// Console error output stream.
        /// </summary>
        public ConsoleErrorOutput()
        { }

        public Stream Stream()
        {
            return Console.OpenStandardError();
        }
    }
}
