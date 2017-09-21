using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Output to dev/null.
    /// </summary>
    public sealed class DeadOutput : IOutput
    {
        /// <summary>
        /// Output to dev/null.
        /// </summary>
        public DeadOutput()
        { }

        public Stream Stream()
        {
            return new DeadStream();
        }
    }
}