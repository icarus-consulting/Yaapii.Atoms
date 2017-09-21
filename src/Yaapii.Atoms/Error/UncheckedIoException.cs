using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Exception to occur when a check should be bypassed.
    /// </summary>
    public sealed class UncheckedIOException : IOException
    {
        /// <summary>
        /// Exception to occur when a check should be bypassed.
        /// </summary>
        /// <param name="ex"></param>
        public UncheckedIOException(Exception ex) : base(ex.Message, ex) { }
    }
}
