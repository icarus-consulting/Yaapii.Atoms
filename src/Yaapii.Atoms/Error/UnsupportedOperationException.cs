using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Fail
{
    /// <summary>
    /// exception to occur when a requested operation is not supported
    /// </summary>
    public sealed class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(string message) : base(message)
        { }
    }
}
