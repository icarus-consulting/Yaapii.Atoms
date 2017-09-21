using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Console input stream.
    /// </summary>
    public sealed class ConsoleInput : IInput
    {
        /// <summary>
        /// Console input stream.
        /// </summary>
        public ConsoleInput()
        {

        }

        public Stream Stream()
        {
            return Console.OpenStandardInput();
        }
    }
}
