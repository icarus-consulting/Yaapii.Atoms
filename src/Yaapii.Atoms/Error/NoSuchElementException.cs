using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Fail
{
    /// <summary>
    /// Exception to occur when a requested element can't be found.
    /// </summary>
    public sealed class NoSuchElementException : Exception
    {
        /// <summary>
        /// Exception to occur when a requested element can't be found.
        /// </summary>
        /// <param name="message">message to display</param>
        public NoSuchElementException(string message) : base(message)
        { }
    }
}
