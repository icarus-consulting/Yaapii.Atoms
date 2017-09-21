using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail always with the given message.
    /// </summary>
    public sealed class FailAlways : IFail
    {
        private readonly Exception _error;

        /// <summary>
        /// Fail always with the given message.
        /// </summary>
        /// <param name="msg">message to wrap in exception</param>
        public FailAlways(string msg) : this(new TextOf(msg))
        { }

        /// <summary>
        /// Fail always with the given message.
        /// </summary>
        /// <param name="msg">message to wrap in exception</param>
        public FailAlways(IText msg) : this(new Exception(msg.AsString()))
        { }

        /// <summary>
        /// Fail always with the given message.
        /// </summary>
        /// <param name="error">ex to throw</param>
        public FailAlways(Exception error)
        {
            _error = error;
        }

        public void Go()
        {
            throw _error;
        }
    }
}
